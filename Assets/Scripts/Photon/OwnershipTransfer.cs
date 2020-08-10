using Microsoft.MixedReality.Toolkit.Input;
using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OwnershipTransfer : MonoBehaviourPun, IMixedRealityPointerHandler
{
    private void OnMouseDown()
    {
        if (!MRDataHolder.Instance.IsEdit)
            base.photonView.RequestOwnership();
    }

    public void OnPointerDown(MixedRealityPointerEventData eventData)
    {
        if (!MRDataHolder.Instance.IsEdit)
            base.photonView.RequestOwnership();
    }

    public void OnPointerClicked(MixedRealityPointerEventData eventData)
    {

    }


    public void OnPointerDragged(MixedRealityPointerEventData eventData)
    {

    }

    public void OnPointerUp(MixedRealityPointerEventData eventData)
    {

    }

}
