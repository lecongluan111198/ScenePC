using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class AnsInput : MonoBehaviour
{
    [Header("INPUT")]
    public TMP_InputField input;
    [Header("BUTTON")]
    public GameObject recordButton;
    public GameObject stopRecordButton;

    private bool isTranslating = false;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    AudioSource audioSource;
    string deviceName;
    public void StartRecord()
    {
        if (isTranslating)
        {
            return;
        }
        Debug.Log("Start recording");
        isTranslating = true;
        if (Microphone.devices.Length > 0)
        {
            audioSource = ConvertContextUtils.AddComponent<AudioSource>(gameObject);
            deviceName = Microphone.devices[0].ToString();
            if (Microphone.IsRecording(deviceName))
            {
                Debug.Log("Cannot record because of that " + deviceName + " is recording!");
                return;
            }
            audioSource.clip = Microphone.Start(deviceName, false, 60, 44100);
            recordButton.SetActive(false);
            stopRecordButton.SetActive(true);
        }
        else
        {
            Debug.Log("Cannot find any devices!");
        }
    }

    public void StopRecord()
    {
        Debug.Log("Stop recording");
        recordButton.SetActive(true);
        stopRecordButton.SetActive(false);
        int position = Microphone.GetPosition(deviceName);
        Microphone.End(deviceName);
        //get only valid data
        AudioClip newClip = CloneToNewClip(audioSource.clip, position);
        audioSource.clip = newClip;
        string filePath;
        byte[] data = WavUtility.FromAudioClip(newClip);
        Debug.Log(data.Length);
        SpeechToTextModel.Instance.SpeechToTextWatson(data, (result) =>
        {
            if (result != null)
            {
                input.text = result;
            }
            else
            {
                input.text = "error";
            }
            isTranslating = false;
        });
    }

    private AudioClip CloneToNewClip(AudioClip clip, int position)
    {
        float[] soundData = new float[clip.samples * clip.channels];
        clip.GetData(soundData, 0);

        float[] newData = new float[position * clip.channels];
        for (int i = 0; i < newData.Length; i++)
        {
            newData[i] = soundData[i];
        }
        var newClip = AudioClip.Create(clip.name, position, clip.channels, clip.frequency, false);
        newClip.SetData(newData, 0);
        AudioClip.Destroy(clip);
        return newClip;
    }
}
