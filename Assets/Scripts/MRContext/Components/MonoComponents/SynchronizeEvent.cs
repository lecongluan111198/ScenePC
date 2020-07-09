using Microsoft.MixedReality.Toolkit.Input;
using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SynchronizeEvent : MonoBehaviour, IMixedRealityPointerHandler
{
    private PhotonView PV;
    private BoxCollider boxCollider;

    private bool isEnableCollider = true;

    // Start is called before the first frame update
    void Start()
    {
        PV = GetComponent<PhotonView>();
        boxCollider = gameObject.GetComponent<BoxCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log("enabled");
        if (!isEnableCollider ||
            (RightController.Instance != null && RightController.Instance.enabled && RightController.Instance.CurrController.Type == AbstractController.ControllerType.BRUSH))
        {
            EnableBoxCollider(false);
        }
        else
        {
            EnableBoxCollider(true);
        }
    }

    private void EnableBoxCollider(bool isEnable)
    {
        if (boxCollider != null && boxCollider.enabled != isEnable)
        {
            boxCollider.enabled = isEnable;
        }
    }

    public void OnPointerClicked(MixedRealityPointerEventData eventData)
    {
    }

    public void OnPointerDown(MixedRealityPointerEventData eventData)
    {
        PV.RPC("SynchorizeDrag", RpcTarget.Others, false);
    }

    public void OnPointerDragged(MixedRealityPointerEventData eventData)
    {
    }

    public void OnPointerUp(MixedRealityPointerEventData eventData)
    {
        PV.RPC("SynchorizeDrag", RpcTarget.Others, true);
    }

    [PunRPC]
    private void SynchorizeDrag(bool isEnableCollider)
    {
        this.isEnableCollider = isEnableCollider;
    }
}
