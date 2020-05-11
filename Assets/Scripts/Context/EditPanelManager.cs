using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EditPanelManager : MonoBehaviour
{

    private GameObject currentObject;
    public static EditPanelManager instance = null;
    public ActionManager action;
    public GameObject panel;
    public GameObject arrow;
    public GameObject scale;
    public GameObject rotate;
    public UserManual userManual;

    private void Update()
    {
        if (panel.gameObject.activeSelf && currentObject != null)
        {
            if (action.IsMove)
            {
                scale.SetActive(false);
                rotate.SetActive(false);
                arrow.SetActive(true);
                userManual.txtMove(currentObject);
            }
            else if (action.IsScale)
            {
                arrow.SetActive(false);
                rotate.SetActive(false);
                scale.SetActive(true);
                userManual.txtScale(currentObject);
            }
            else if (action.IsRotate)
            {
                arrow.SetActive(false);
                scale.SetActive(false);
                rotate.SetActive(true);
                userManual.txtRotation(currentObject);
            }
            else userManual.txtDefault(currentObject);
        }
    }
    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            if(instance != this)
            {
                Destroy(instance.gameObject);
                instance = this;
            }
        }
    }

    public void updateCurrObject(GameObject go)
    {
        currentObject = go;
    }

    public void setActive(bool active)
    {
        panel.gameObject.SetActive(active);
    }
}
