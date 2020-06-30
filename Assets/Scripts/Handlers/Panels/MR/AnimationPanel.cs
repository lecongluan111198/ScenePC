using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AnimationPanel : MonoBehaviour
{
    private GameObject currentObject;
    private string nameAnim;
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
        //currentObject = MRContextManager.Instance.CurrentObject;
        //Vector3 cameraPos = Camera.main.transform.position;
        //Vector3 distance = MRDataHolder.Instance.Distance;
        //transform.position = new Vector3(cameraPos.x + distance.x, cameraPos.y + distance.y, cameraPos.z + distance.z - 0.15f);
    }

    public void Play(string name)
    {
        Debug.Log(name);
        nameAnim = name;
        CustAnimation anim = currentObject.GetComponent<CustAnimation>();
        if (anim == null)
        {
            anim = currentObject.AddComponent<CustAnimation>();
        }
        anim.ClipName = nameAnim;
        anim.Mode = playMode;
        anim.ControllerName = "truck";
        anim.IsLoop = false;
        anim.refreshAnimation();
        anim.Play();
    }

    public void ChangePlayMode(int mode)
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

    public void Apply()
    {
        //add CustAnimation
        CustAnimation anim = currentObject.GetComponent<CustAnimation>();
        if (anim == null)
        {
            anim = currentObject.AddComponent<CustAnimation>();
        }
        anim.ClipName = nameAnim;
        anim.Mode = playMode;
        anim.ControllerName = "truck";
        gameObject.SetActive(false);
        TagAlongManager.Instance.ControllerOn();
    }

    public void Cancel()
    {
        CustAnimation anim = currentObject.GetComponent<CustAnimation>();
        if(anim != null)
        {
            Debug.Log("destroy");
            Destroy(anim);
        }
        gameObject.SetActive(false);
        TagAlongManager.Instance.ControllerOn();
    }
}
