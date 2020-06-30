using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SelectLesson : MonoBehaviour
{
    public GameObject list;
    
    [Header("Prefab")]
    public GameObject contextItem;

    [Header("Container")]
    public GameObject List;

    [Header("RESOURCE")]
    public Animator loadingAnim;
    private List<GameObject> contexts = new List<GameObject>();
    private int length = 12;
    private int currentOwnOffset = -1;
    public void SelectAll()
    {
        foreach(Transform child in list.transform)
        {
            child.GetComponentInChildren<Toggle>().isOn = true;
        }
    }
    public void UnselectAll()
    {
        foreach(Transform child in list.transform)
        {
            child.GetComponentInChildren<Toggle>().isOn = false;
        }
    }
    public void GetCheckOn()
    {
        foreach(Transform child in list.transform)
        {
            if (child.GetComponentInChildren<Toggle>().isOn == true)
            {
                Debug.Log(child.GetComponent<CheckListItem>().ID);
                if (currentOwnOffset < 0)
                {
                    //currentOwnOffset = 0;
                   // loadingAnim.Play("Modal Window In");
                    ContextModel.Instance.loadOwnContext(length, currentOwnOffset, (data) =>
                    {
                        if (data != null)
                        {
                            foreach (Context context in data)
                            {
                                if (child.GetComponent<CheckListItem>().ID == context.Id)
                                {
                                    GameObject item = Instantiate(contextItem, List.transform, false);
                                    item.GetComponent<CheckListItem>().UpdateInformation(context);
                                    item.transform.SetParent(List.transform);
                                    contexts.Add(item);
                                }
                            }
                        }
                        //loadingAnim.Play("Modal Window Out");
                    });
                }
            }
        }
        currentOwnOffset = 0;
    }
}
