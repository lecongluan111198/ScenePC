using Microsoft.MixedReality.Toolkit.Input;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEditor;
using UnityEngine;

public class CustAnimation : MonoBehaviour, IMixedRealityPointerHandler
{
    private Animator anim;
    private string controllerName = "truck";
    private string clipName = "Transform";
    private EAnimMode mode = EAnimMode.START_LOOP;
    private bool isLoop = false;
    private object _LOCK = new object();

    public string ClipName { get => clipName; set => clipName = value; }
    public EAnimMode Mode { get => mode; set => mode = value; }
    public string ControllerName { get => controllerName; set => controllerName = value; }

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        if(anim == null)
        {
            anim = gameObject.AddComponent<Animator>();
        }
        anim.runtimeAnimatorController = Resources.Load<RuntimeAnimatorController>(ResourceManager.AnimController + controllerName);

        switch (mode)
        {
            case EAnimMode.CLICK:
            case EAnimMode.START:
                isLoop = false;
                break;
            case EAnimMode.CLICK_LOOP:
            case EAnimMode.START_LOOP:
                isLoop = true;
                break;
        }

        if (mode == EAnimMode.START || mode == EAnimMode.START_LOOP)
        {
            foreach (AnimationClip ac in anim.runtimeAnimatorController.animationClips)
            {
                AnimationClipSettings setting = AnimationUtility.GetAnimationClipSettings(ac);
                setting.loopTime = isLoop;
                Debug.Log(ac.name);
                Debug.Log(setting.loopTime);
                AnimationUtility.SetAnimationClipSettings(ac, setting);
            }
            anim.SetTrigger(clipName);
        }
    }

    // Update is called once per frame
    void Update()
    {
        switch (mode)
        {
            case EAnimMode.START:
                break;
            case EAnimMode.CLICK:
                break;
            case EAnimMode.START_LOOP:
                break;
            case EAnimMode.CLICK_LOOP:
                break;
        }
    }

    public void OnPointerDown(MixedRealityPointerEventData eventData)
    {
        Debug.Log("OnPointerDown");
    }

    public void OnPointerDragged(MixedRealityPointerEventData eventData)
    {
        Debug.Log("OnPointerDragged");
    }

    public void OnPointerUp(MixedRealityPointerEventData eventData)
    {
        Debug.Log("OnPointerUp");
    }

    public void OnPointerClicked(MixedRealityPointerEventData eventData)
    {
        Debug.Log("OnPointerClicked");
        if (Monitor.TryEnter(_LOCK))
        {
            try
            {
                if (mode == EAnimMode.CLICK || mode == EAnimMode.CLICK_LOOP)
                {
                    foreach (AnimationClip ac in anim.runtimeAnimatorController.animationClips)
                    {
                        AnimationClipSettings setting = AnimationUtility.GetAnimationClipSettings(ac);
                        setting.loopTime = isLoop;
                        Debug.Log(ac.name);
                        Debug.Log(setting.loopTime);
                        AnimationUtility.SetAnimationClipSettings(ac, setting);
                    }
                    anim.SetTrigger(clipName);
                }
            }
            finally
            {
                Monitor.Exit(_LOCK);
            }
        }
    }
}
