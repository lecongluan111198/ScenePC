using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public PhotonView PV;
    public Animator anim;
    public TextMesh username;

    private void Awake()
    {
    }
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
        Debug.Log("mine " + PV.IsMine);
        username.text = AccountInfo.Instance.Username;
        //if (PV.IsMine)
        //{
        //    Camera.main.transform.localPosition = new Vector3(0, 0.64f, -0.071f);
        //    transform.SetParent(Camera.main.transform);
        //    transform.localPosition = new Vector3(0, 0, 0);
        //}
        //else
        //{

        //}
    }

    private void FixedUpdate()
    {
        //if (PV.IsMine)
        //{
        //    if (Input.GetKeyDown(KeyCode.O))
        //    {
        //        anim.Play("Dying");
        //    }
        //    else if (Input.GetKeyDown(KeyCode.P))
        //    {
        //        anim.Play("Stand up");
        //    }
        //    else if (Input.GetKeyDown(KeyCode.E))
        //    {
        //        anim.Play("Kick");
        //    }
        //    else if (Input.GetKeyDown(KeyCode.R))
        //    {
        //        anim.Play("Lead Jab");
        //    }
        //    else if (Input.GetKeyDown(KeyCode.W))
        //    {
        //        anim.Play("Running");
        //    }else if (Input.GetKeyDown(KeyCode.A))
        //    {
        //        anim.Play("Turn left");
        //    }else if (Input.GetKeyDown(KeyCode.D))
        //    {
        //        anim.Play("Turn right");
        //    }

        //}
    }
}
