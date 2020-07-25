using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationControllerPanel : MonoBehaviour
{
    [Header("PANELS")]
    public GameObject animationPanel;
    public GameObject notifyRecord;
    [Header("ANIMATION")]
    public Animator anim;

    public void OffController()
    {
        anim.SetBool("open", false);
    }

    public void OnController()
    {
        anim.SetBool("open", true);
    }

    public void ShowAvailableAnimation()
    {
        animationPanel.SetActive(true);
        OffController();
    }

    public void RecordAnimation()
    {
        notifyRecord.SetActive(true);
        OffController();
    }

    public void Close()
    {
        OffController();
        SettingMenuPanel.Instance.OnController();
    }
}
