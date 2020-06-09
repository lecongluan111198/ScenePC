﻿using Microsoft.MixedReality.Toolkit.Input;
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
    public bool IsLoop { get => isLoop; set => isLoop = value; }

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        if (anim == null)
        {
            anim = gameObject.AddComponent<Animator>();
        }
        anim.runtimeAnimatorController = Resources.Load<RuntimeAnimatorController>(ResourceManager.AnimController + controllerName);

        if (anim.runtimeAnimatorController == null)
        {
            Destroy(this);
        }
        switch (mode)
        {
            case EAnimMode.CLICK:
            case EAnimMode.START:
                IsLoop = false;
                break;
            case EAnimMode.CLICK_LOOP:
            case EAnimMode.START_LOOP:
                IsLoop = true;
                break;
        }

        if ((mode == EAnimMode.START || mode == EAnimMode.START_LOOP) && !MRDataHolder.Instance.IsEdit)
        {
            foreach (AnimationClip ac in anim.runtimeAnimatorController.animationClips)
            {
                AnimationClipSettings setting = AnimationUtility.GetAnimationClipSettings(ac);
                if (setting.loopTime != isLoop)
                {
                    setting.loopTime = IsLoop;
                    Debug.Log(ac.name);
                    Debug.Log(setting.loopTime);
                    AnimationUtility.SetAnimationClipSettings(ac, setting);
                }
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

    public void refreshAnimation()
    {
        anim = GetComponent<Animator>();
        if (anim == null)
        {
            anim = gameObject.AddComponent<Animator>();
        }
        anim.runtimeAnimatorController = Resources.Load<RuntimeAnimatorController>(ResourceManager.AnimController + controllerName);
        if (anim.runtimeAnimatorController == null)
        {
            Destroy(this);
            return;
        }
        foreach (AnimationClip ac in anim.runtimeAnimatorController.animationClips)
        {
            AnimationClipSettings setting = AnimationUtility.GetAnimationClipSettings(ac);
            setting.loopTime = IsLoop;
            Debug.Log(ac.name);
            Debug.Log(setting.loopTime);
            AnimationUtility.SetAnimationClipSettings(ac, setting);
        }
    }

    public void Play()
    {
        anim.SetTrigger(clipName);
    }

    public void OnPointerDown(MixedRealityPointerEventData eventData)
    {
    }

    public void OnPointerDragged(MixedRealityPointerEventData eventData)
    {
    }

    public void OnPointerUp(MixedRealityPointerEventData eventData)
    {
    }

    public void OnPointerClicked(MixedRealityPointerEventData eventData)
    {
        if (Monitor.TryEnter(_LOCK))
        {
            try
            {
                if ((mode == EAnimMode.CLICK || mode == EAnimMode.CLICK_LOOP) && !MRDataHolder.Instance.IsEdit)
                {
                    foreach (AnimationClip ac in anim.runtimeAnimatorController.animationClips)
                    {
                        AnimationClipSettings setting = AnimationUtility.GetAnimationClipSettings(ac);
                        setting.loopTime = IsLoop;
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