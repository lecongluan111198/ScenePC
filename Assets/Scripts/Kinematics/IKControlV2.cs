using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class IKControlV2 : MonoBehaviour
{
    private Animator animator;

    public bool ikActive = false;
    //public Transform rightHandObj = null;
    //public Transform lookObj = null;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    //a callback for calculating IK
    void OnAnimatorIK()
    {
        if (animator)
        {

            //if the IK is active, set the position and rotation directly to the goal. 
            if (ikActive)
            {

                // Set the look target position, if one has been assigned
                if (Camera.main.transform != null)
                {
                    animator.SetLookAtWeight(1);
                    animator.SetLookAtPosition(Camera.main.transform.position);
                }

                // Set the right hand target position and rotation, if one has been assigned
                if (RightController.Instance != null && RightController.Instance.enabled)
                {
                    animator.SetIKPositionWeight(AvatarIKGoal.RightHand, 1);
                    animator.SetIKRotationWeight(AvatarIKGoal.RightHand, 1);
                    animator.SetIKPosition(AvatarIKGoal.RightHand, RightController.Instance.transform.position);
                    animator.SetIKRotation(AvatarIKGoal.RightHand, RightController.Instance.transform.rotation);
                }

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
