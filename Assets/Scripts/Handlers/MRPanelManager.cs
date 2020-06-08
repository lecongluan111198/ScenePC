using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MRPanelManager : MonoBehaviour
{
    public Animator anim;

    private bool isIn = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeState()
    {
        if (isIn)
        {
            anim.Play("MR Panel Out");
        }
        else
        {
            anim.Play("MR Panel In");
        }
        isIn = !isIn;
    }
}
