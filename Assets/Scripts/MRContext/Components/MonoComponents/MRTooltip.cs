using Microsoft.MixedReality.Toolkit.Input;
using Microsoft.MixedReality.Toolkit.UI;
using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MRTooltip : MonoBehaviour
{
    private Dictionary<int, TooltipComponent.ToolTipDetail> mapTooltipDetails = new Dictionary<int, TooltipComponent.ToolTipDetail>();
    private int currentIndex = 0;

    public Dictionary<int, TooltipComponent.ToolTipDetail> MapTooltipDetails { get => mapTooltipDetails; set => mapTooltipDetails = value; }

    private Vector3 distance = new Vector3(0.25f, 0.25f, 0.25f);
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
        //BFS();
    }

    public void UpdateTooltip(HashSet<TooltipComponent.ToolTipDetail> tooltipDetails)
    {
        foreach (TooltipComponent.ToolTipDetail detail in tooltipDetails)
        {
            Debug.Log(detail.Title);
            CreateTooltip(detail.Title, ConvertTypeUtils.listToVector3(detail.AnchorPos), ConvertTypeUtils.listToVector3(detail.PivotPos));
        }
    }

    public void AddTooltip(string title)
    {
        CreateTooltip(title, new Vector3(0, 0, 0.5f), new Vector3(0, 0, 0));
        //Debug.Log(name + " " + title);
        //if (nestedMap.Count == 0)
        //{
        //    Debug.Log("nested map size is 0");
        //    BFS();
        //}
        //if (nestedMap.ContainsKey(name))
        //{
        //    tooltipObject.Add(name);
        //    //CreateTooltip(title, nestedMap[name]);
        //    //tooltipDetails.Add(new TooltipComponent.ToolTipDetail(name, title));
        //}
    }

    private void CreateTooltip(string title, Vector3 anchorPos, Vector3 pivotPos)
    {
        GameObject ttGo = Instantiate(Resources.Load(ResourceManager.MRTooltip) as GameObject);
        ttGo.transform.SetParent(transform);
        TooltipObject obj = ttGo.GetComponent<TooltipObject>();
        obj.TargetObject = this;
        obj.CurrentIndex = currentIndex;
        obj.SetPosition(anchorPos, pivotPos);
        MapTooltipDetails[currentIndex++] = new TooltipComponent.ToolTipDetail(title,
            ConvertTypeUtils.vector3ToList(anchorPos), ConvertTypeUtils.vector3ToList(pivotPos));
        ToolTip tooltip = ttGo.GetComponent<ToolTip>();
        tooltip.ToolTipText = title;
        ttGo.transform.position = Camera.main.transform.position;

    }

    public void UpdatePos(int index, Vector3 anchor, Vector3 pivot)
    {
        MapTooltipDetails[index].AnchorPos = ConvertTypeUtils.vector3ToList(anchor);
        MapTooltipDetails[index].PivotPos = ConvertTypeUtils.vector3ToList(pivot);
    }

    private void BFS()
    {
        //Queue<Transform> queue = new Queue<Transform>();
        //queue.Enqueue(transform);
        //nestedMap[transform.name] = gameObject;
        //while (queue.Count != 0)
        //{
        //    Transform exTransform = queue.Dequeue();
        //    foreach (Transform child in exTransform)
        //    {
        //        if (child.name != "rigRoot")
        //        {
        //            string path = exTransform.name + "." + child.name;
        //            nestedMap[path] = child.gameObject;
        //            queue.Enqueue(child);
        //        }
        //    }
        //}
    }
}
