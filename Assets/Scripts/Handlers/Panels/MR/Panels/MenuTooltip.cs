using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuTooltip : MonoBehaviour
{
    public GameObject objList;
    public GameObject tooltipItem;

    private GameObject currentObject;
    private Dictionary<string, GameObject> nestedMap = new Dictionary<string, GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnEnable()
    {
        currentObject = MRDataHolder.Instance.CurrentClickObject;
        DestroyAllChild();
        BFS();
        AddObjItem();
    }

    private void BFS()
    {
        Queue<Transform> queue = new Queue<Transform>();
        queue.Enqueue(currentObject.transform);
        nestedMap[currentObject.name] = currentObject;
        while (queue.Count != 0)
        {
            Transform exTransform = queue.Dequeue();
            foreach (Transform child in exTransform)
            {
                if(child.name != "rigRoot")
                {
                    string path = exTransform.name + "." + child.name;
                    nestedMap[path] = child.gameObject;
                    queue.Enqueue(child);
                }
            }
        }
    }

    private void DestroyAllChild()
    {
        foreach(Transform tran in objList.transform)
        {
            Debug.Log(tran.name);
            Destroy(tran.gameObject);
        }
    }

    private void AddObjItem()
    {
        MRTooltip mrTooltip = currentObject.GetComponent<MRTooltip>();
        foreach(KeyValuePair<string, GameObject> entry in nestedMap)
        {
            GameObject item = Instantiate(tooltipItem, objList.transform, false);
            item.transform.SetParent(objList.transform);
            TooltipItem tooltip = item.GetComponent<TooltipItem>();
            //if(mrTooltip != null && mrTooltip.TooltipObject.Contains(entry.Key))
            //{
            //    tooltip.UpdateInfo(entry.Key, entry.Value, true);
            //}
            //else
            //{
            //    tooltip.UpdateInfo(entry.Key, entry.Value, false);
            //}
        }
    }

    public void Cancel()
    {
        gameObject.SetActive(false);
        //TagAlongManager.Instance.ControllerOn();
        SettingMenuPanel.Instance.OnController();
    }
}
