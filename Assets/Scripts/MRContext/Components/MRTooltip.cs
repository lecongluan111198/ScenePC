using Microsoft.MixedReality.Toolkit.Input;
using Microsoft.MixedReality.Toolkit.UI;
using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MRTooltip : MonoBehaviour
{
    private HashSet<TooltipComponent.ToolTipDetail> tooltipDetails = new HashSet<TooltipComponent.ToolTipDetail>();
    private Dictionary<string, GameObject> nestedMap = new Dictionary<string, GameObject>();
    private HashSet<string> tooltipObject = new HashSet<string>();

    public HashSet<TooltipComponent.ToolTipDetail> TooltipDetails { get => tooltipDetails; set => tooltipDetails = value; }
    public HashSet<string> TooltipObject { get => tooltipObject; set => tooltipObject = value; }

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
        BFS();
    }

    public void UpdateTooltip(HashSet<TooltipComponent.ToolTipDetail> tooltipDetails)
    {
        this.TooltipDetails = tooltipDetails;
        if (nestedMap.Count == 0)
        {
            Debug.Log("nested map size is 0");
            BFS();
        }
        foreach (TooltipComponent.ToolTipDetail detail in tooltipDetails)
        {
            if (nestedMap.ContainsKey(name))
            {
                tooltipObject.Add(detail.Name);
                CreateTooltip(detail.Title, nestedMap[name]);
            }
        }
    }

    public void AddTooltip(string name, string title)
    {
        Debug.Log(name + " " + title);
        if (nestedMap.Count == 0)
        {
            Debug.Log("nested map size is 0");
            BFS();
        }
        if (nestedMap.ContainsKey(name))
        {
            tooltipObject.Add(name);
            CreateTooltip(title, nestedMap[name]);
            //tooltipDetails.Add(new TooltipComponent.ToolTipDetail(name, title));
        }
    }

    private void CreateTooltip(string title, GameObject go)
    {
        Debug.Log("create tooltip");
        GameObject ttGo;
        if (MRDataHolder.Instance.IsEdit)
        {
            ttGo = Instantiate(Resources.Load(ResourceManager.MRTooltip) as GameObject);
            ttGo.transform.SetParent(MREditContextManager.Instance.container.transform);
        }
        else
        {
            ttGo = PhotonNetwork.Instantiate(ResourceManager.MRTooltip, Vector3.zero, Quaternion.identity);
        }
        //ttGo.transform.localPosition = go.transform.localPosition;
        //ttGo.transform.parent = go.transform;
        ToolTipConnector connector = ttGo.GetComponent<ToolTipConnector>();
        //connector.Target = go;
        //connector.PivotDirection = ConnectorPivotDirection.North;
        //connector.PivotDistance = 0.25f;
        //connector.PivotDirectionOrient = ConnectorOrientType.OrientToObject;
        //connector.ManualPivotLocalPosition = Vector3.up;
        //connector.ManualPivotDirection = Vector3.up;
        //connector.ConnectorFollowingType = ConnectorFollowType.AnchorOnly;
        //connector.PivotMode = ConnectorPivotMode.Manual;
        ToolTip tooltip = ttGo.GetComponent<ToolTip>();
        tooltip.ToolTipText = title;
        tooltip.PivotPosition = transform.TransformPoint(Vector3.up);
    }

    private void BFS()
    {
        Queue<Transform> queue = new Queue<Transform>();
        queue.Enqueue(transform);
        nestedMap[transform.name] = gameObject;
        while (queue.Count != 0)
        {
            Transform exTransform = queue.Dequeue();
            foreach (Transform child in exTransform)
            {
                if (child.name != "rigRoot")
                {
                    string path = exTransform.name + "." + child.name;
                    nestedMap[path] = child.gameObject;
                    queue.Enqueue(child);
                }
            }
        }
    }
}
