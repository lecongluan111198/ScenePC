using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecordOption : MonoBehaviour
{
    private GameObject currentObject;
    private EAnimMode playMode = EAnimMode.CLICK;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnEnable()
    {
        currentObject = MRDataHolder.Instance.CurrentClickObject;
    }

    public void Play()
    {
        RecordAnimation recorAnim = currentObject.GetComponent<RecordAnimation>();
        if(recorAnim != null)
        {
            recorAnim.PlayAnimation();
        }
    }

    public void Save()
    {
        //playMode.get
        RecordAnimation recorAnim = currentObject.GetComponent<RecordAnimation>();
        if (recorAnim != null)
        {
            recorAnim.Mode = playMode;
        }
        gameObject.SetActive(false);
        TagAlongManager.Instance.ControllerOn();
    }

    public void ChangPlayMode(int mode)
    {
        switch ((EAnimMode)mode)
        {
            case EAnimMode.CLICK:
                playMode = EAnimMode.CLICK;
                break;
            case EAnimMode.START:
                playMode = EAnimMode.START;
                break;
            case EAnimMode.START_LOOP:
                playMode = EAnimMode.START_LOOP;
                break;
            case EAnimMode.CLICK_LOOP:
                playMode = EAnimMode.CLICK_LOOP;
                break;
        }
    }

    public void Cancel()
    {
        RecordAnimation recorAnim = currentObject.GetComponent<RecordAnimation>();
        if (recorAnim != null)
        {
            Destroy(recorAnim);
        }
        gameObject.SetActive(false);
        TagAlongManager.Instance.ControllerOn();
    }
}
