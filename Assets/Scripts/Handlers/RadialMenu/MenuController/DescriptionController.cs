using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DescriptionController : MonoBehaviour
{
    [Header("PANELS")]
    public GameObject tooltipSettingPanel;
    [Header("ANIMATION")]
    public Animator anim;
    [Header("PREFABS")]
    public GameObject descriptionPanel;

    private Vector3 distance = new Vector3(0.5f, 0.5f, 0.5f);

    public void OffController()
    {
        anim.SetBool("open", false);
    }

    public void OnController()
    {
        anim.SetBool("open", true);
    }

    public void ShowTooltipMenu()
    {
        tooltipSettingPanel.SetActive(true);
        OffController();
    }

    public void CreateDescriptionPanel()
    {
        //descriptionPanel.SetActive(true);
        //create new description panel
        GameObject go = Instantiate(descriptionPanel);
        MREditContextManager.Instance.UpdateModel(go);
        Vector3 camForward = Camera.main.transform.forward;
        Vector3 dis = new Vector3(distance.x * camForward.x, distance.y * camForward.y, distance.z * camForward.z);
        go.transform.position = Camera.main.transform.position + dis;
        ObjBasicInfo bInfo = ConvertContextUtils.AddComponent<ObjBasicInfo>(go);
        bInfo.DownloadName = "Description";
        bInfo.FromServer = false;
        bInfo.Id = 1;
        OffController();
    }
}
