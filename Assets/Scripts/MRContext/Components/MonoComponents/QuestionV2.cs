using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestionV2 : MonoBehaviour
{
    private Content question;
    private List<Content> choose;
    private int answer;
    private AudioSource audioSource;
    private float t = 0.5f;

    public int Answer { get => answer; set => answer = value; }
    public List<Content> Choose { get => choose; set => choose = value; }
    public Content Question { get => question; set => question = value; }


    // Start is called before the first frame update
    void Start()
    {
        audioSource = ConvertContextUtils.addComponent<AudioSource>(gameObject);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void showQuestion()
    {
        
        //get position
        Vector3 src = gameObject.transform.position;
        Vector3 dest = Camera.main.transform.position;
        Vector3 newPos = new Vector3();
        newPos.x = src.x + (dest.x - src.x) * t;
        newPos.y = src.y + (dest.y - src.y) * t;
        newPos.z = src.z + (dest.z - src.z) * t;
        newPos.y += 0.8f;

        GameObject go = GameObject.Find("MenuQuestion(Clone)");
        if (go != null && go.transform.position == newPos)
        {
            Destroy(go);
        }

        GameObject menuQuestion = Instantiate(Resources.Load(ResourceManager.MRCanvasPrefab + "MenuQuestion") as GameObject);
        menuQuestion.transform.position = newPos;
        MenuQuestionPanelV2 panel = menuQuestion.GetComponent<MenuQuestionPanelV2>();
        if (panel != null)
        {
            panel.updateInfomation(this);
        }
    }

    //public void UpdateInformation(AudioClip clip)
    //{
    //    audioSource.clip = clip;
    //    Data = new float[clip.samples * clip.channels];
    //    clip.GetData(Data, 0);
    //}

    //public void UpdateInformation(float[] data)
    //{
    //    if(data == null || data.Length == 0)
    //    {
    //        Debug.Log("Data cannot be empty");
    //        return;
    //    }
    //    this.data = data;
    //    audioSource.clip.SetData(data, 0);
    //}

    //public void Play()
    //{
    //    if (audioSource.isPlaying)
    //        return;
    //    audioSource.Play();
    //}
}
