using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class CustAnimation : MonoBehaviour
{
    private Animator anim;
    private string controllerName = "truck";
    private string clipName = "Transform";
    private AnimationComponent.AnimMode mode = AnimationComponent.AnimMode.START_LOOP;

    public string ClipName { get => clipName; set => clipName = value; }
    public AnimationComponent.AnimMode Mode { get => mode; set => mode = value; }
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

        bool isLoop = false;
        switch (mode)
        {
            case AnimationComponent.AnimMode.START:
                isLoop = false;
                break;
            case AnimationComponent.AnimMode.START_LOOP:
                isLoop = true;
                break;
        }

        if (mode == AnimationComponent.AnimMode.START || mode == AnimationComponent.AnimMode.START_LOOP)
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
            case AnimationComponent.AnimMode.START:
                break;
            case AnimationComponent.AnimMode.CLICK:
                break;
            case AnimationComponent.AnimMode.START_LOOP:
                break;
            case AnimationComponent.AnimMode.CLICK_LOOP:
                break;
        }
    }
}
