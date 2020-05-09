using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class RightPanel : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private Animator panelAnimator;
    [Header("SETTINGS")]
    public bool isConsole;

    bool isOn;

    void Start()
    {
        panelAnimator = this.GetComponent<Animator>();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        panelAnimator.Play("Panel In");
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        panelAnimator.Play("Panel Out");
    }
}
