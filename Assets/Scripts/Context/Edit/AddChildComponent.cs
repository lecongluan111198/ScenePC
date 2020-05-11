using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddChildComponent : MonoBehaviour
{
    public GameObject panel;
    public GameObject arrow;
    public GameObject scale;
    public GameObject rotate;
    public GameObject go;
    public ActionManager action;
    public UserManual userManual;
    List<GameObject> childrenList = new List<GameObject>();
    
    // Start is called before the first frame update
    void Start()
    {
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
            sc.arrow = arrow;
            sc.scale = scale;
            sc.rotate = rotate;
            sc.userManual = userManual;
            sc.myObj = childrenList[i];
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnMouseDown()
    {
      
        RaycastHit hit;
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit))
        {
            foreach(GameObject obj in childrenList)
            {
                if (hit.transform.name == obj.name)
                {
                    //obj.AddComponent<ActionMenu>().panel = panel;
                    panel.SetActive(!panel.active);
                }
               // obj.AddComponent<ActionMenu>().panel = null;
            }
        }
    }
}
