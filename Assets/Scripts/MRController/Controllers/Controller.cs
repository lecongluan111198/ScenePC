using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.WSA.Input;

public abstract class AbstractController : MonoBehaviour
{
    public enum ControllerType
    {
        HAND,
        BRUSH
    }

    protected ControllerType type;

    public ControllerType Type { get => type; set => type = value; }

    protected void Awake()
    {
        
    }
    // Start is called before the first frame update
    protected void Start()
    {
        doOnStart();
    }

    // Update is called once per frame
    protected void Update()
    {
        //do something
        doOnUpdate();
    }

    protected void OnEnable()
    {
        InteractionManager.InteractionSourcePressed += InteractionSourcePressed;
        doOnEnable();
    }

    private void OnDisable()
    {
        doOnDisable();
    }
    protected abstract void doOnStart();

    protected abstract void doOnEnable();

    protected abstract void doOnDisable();

    protected abstract void doOnUpdate();

    private void InteractionSourcePressed(InteractionSourcePressedEventArgs obj)
    {
        if (obj.state.source.handedness == InteractionSourceHandedness.Right && obj.pressType == InteractionSourcePressType.Grasp)
        {
            //if (currController == null)
            //{
            //    currController = controllers[currentIndex];
            //}
            //currController.gameObject.SetActive(false);

            //currentIndex++;
            //if (currentIndex >= controllers.Count)
            //{
            //    currentIndex = 0;
            //}
            //currController = controllers[currentIndex];
            //currController.gameObject.SetActive(true);

        }
    }
}
