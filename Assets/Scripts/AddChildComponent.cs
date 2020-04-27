using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddChildComponent : MonoBehaviour
{
    public GameObject panel;
    public ActionManager action;
    // Start is called before the first frame update
    void Start()
    {
        List<GameObject> childrenList = new List<GameObject>();
        Transform[] children = GetComponentsInChildren<Transform>(true);
        for (int i = 0; i < children.Length; i++)
        {
            Transform child = children[i];
            if (child != transform)
            {
                childrenList.Add(child.gameObject);
            }
        }
        for (int i = 0; i < childrenList.Count; i++)
        {
            ActionMenu sc = childrenList[i].AddComponent<ActionMenu>();
            sc.panel = panel;
            sc.action = action;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
