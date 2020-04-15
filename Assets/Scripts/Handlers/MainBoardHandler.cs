using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainBoardHandler : MonoBehaviour
{
    [Header("RESOURCES")]
    public GameObject mainPanels;
    public Animator backgroundAnimator;
    public Animator startFadeIn;

    private Animator mainPanelsAnimator;
    // Start is called before the first frame update
    void Start()
    {
        mainPanelsAnimator = mainPanels.GetComponent<Animator>();
        mainPanels.SetActive(true);
        mainPanelsAnimator.Play("Panel Start");
        backgroundAnimator.Play("Switch");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
