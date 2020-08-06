using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ModelItem : MonoBehaviour
{
    [Header("INFORMATION")]
    public Text text;

    private Model model;
    private Vector3 distance = new Vector3(0.5f, 0.5f, 0.5f);
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void UpdateInformation(Model model)
    {
        this.model = model;
        text.text = model.Name;
    }

    public void Import()
    {
        if (model.FromServer)
        {

        }
        else
        {
            GameObject go = Instantiate(Resources.Load(ResourceManager.MRPrefab + model.Name) as GameObject);
            MREditContextManager.Instance.UpdateModel(go);
            Vector3 camForward = Camera.main.transform.forward;
            Vector3 dis = new Vector3(distance.x * camForward.x, distance.y * camForward.y, distance.z * camForward.z);
            go.transform.position = Camera.main.transform.position + dis;
            ObjBasicInfo bInfo = ConvertContextUtils.AddComponent<ObjBasicInfo>(go);
            bInfo.DownloadName = model.Name;
            bInfo.FromServer = model.FromServer;
            bInfo.Id = model.Id;
        }
    }
}
