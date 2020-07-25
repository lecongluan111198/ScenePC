using Microsoft.MixedReality.Toolkit.Input;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class RadialMenuItem : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IMixedRealityFocusHandler
{
    private Animator anim;
    public float angle = 0;
    public GameObject arrow;
    public string title;
    public Text text;
    public bool isActive = true;
    public Image img;

    private Color activeColor = new Color(86f / 255f, 1, 241f / 255f, 1);
    private Color inactiveColor = new Color(121f / 255f, 134f / 255f, 133f / 255f, 1);
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        if (isActive)
        {
            img.color = activeColor;
        }
        else
        {
            img.color = inactiveColor;
            //img.color = Color.gray;
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        anim.SetBool("hover", true);
        arrow.transform.rotation = Quaternion.Euler(0, 0, angle);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        anim.SetBool("hover", false);
    }

    public void OnFocusEnter(FocusEventData eventData)
    {
        anim.SetBool("hover", true);
        arrow.transform.rotation = Quaternion.Euler(0, 0, angle);
        text.text = title;
    }

    public void OnFocusExit(FocusEventData eventData)
    {
        anim.SetBool("hover", false);
        arrow.transform.rotation = Quaternion.Euler(0, 0, 0);
        text.text = "";
    }
}
