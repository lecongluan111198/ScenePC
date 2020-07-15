using UnityEngine;
using System;
using System.Collections;
using Photon.Pun;

[RequireComponent(typeof(Animator))]

public class IKControl : MonoBehaviour
{
    protected Animator animator;
    public bool ikActive = false;
    private Vector3 distance = new Vector3(10, 10, 10);
    private Transform rightArm;
    private PhotonView PV;
    void Start()
    {
        animator = GetComponent<Animator>();
        rightArm = animator.GetBoneTransform(HumanBodyBones.RightUpperArm);
        PV = GetComponent<PhotonView>();
        if (PV.IsMine)
        {
            //on ikActive
            ikActive = true;
        }
    }

    private void Update()
    {
        
    }
    //a callback for calculating IK
    //private void LateUpdate()
    //{
    //    rightArm.LookAt(RightController.Instance.transform.position*100);
    //   // rightShoulder.position = rightShoulder.position + RightController.Instance.transform.position;
    //}
    void OnAnimatorIK()
    {
        if (RightController.Instance != null && RightController.Instance.enabled)
        {

            if (animator)
            {
                //if the IK is active, set the position and rotation directly to the goal. 
                if (ikActive)
                { 
                    animator.SetIKPositionWeight(AvatarIKGoal.RightHand, 1);
                    animator.SetIKRotationWeight(AvatarIKGoal.RightHand, 1);

                    animator.SetIKPosition(AvatarIKGoal.RightHand, RightController.Instance.transform.localPosition);
                    animator.SetIKRotation(AvatarIKGoal.RightHand, RightController.Instance.transform.localRotation);
                    //Debug.Log(rightArm.position);
                    rightArm.LookAt(RightController.Instance.transform.position);
                    //rightArm.position = rightArm.position + 100 * RightController.Instance.transform.position;
                    ////rightShoulder.position = RightController.Instance.transform.localPosition+distance;
                    //Debug.Log(rightArm.position);
                   
                }

                //if the IK is not active, set the position and rotation of the hand and head back to the original position
                else
                {
                    animator.SetIKPositionWeight(AvatarIKGoal.RightHand, 0);
                    animator.SetIKRotationWeight(AvatarIKGoal.RightHand, 0);
                    animator.SetLookAtWeight(0);
                }
            }
        }
    }
}