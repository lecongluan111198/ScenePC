using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TagAlongManager : MonoBehaviour
{
    public static TagAlongManager Instance = null;
    public Animator anim;
    public TMP_Text titlePanel;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ControllerIn()
    {
        anim.Play("Controller in");
    }

    public void ControllerOut()
    {
        anim.Play("Controller out");
        if(MRDataHolder.Instance.CurrentClickObject != null)
        {
            titlePanel.text = MRDataHolder.Instance.CurrentClickObject.name;
        }
        else
        {
            titlePanel.text = "Unkown object";
        }
    }
    public void ControllerOn()
    {
        anim.Play("Controller On");
    }
    public void ControllerOff()
    {
        anim.Play("Controller Off");
    }
}
