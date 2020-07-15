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
            go.transform.position = Camera.main.transform.position + distance;
            ObjBasicInfo bInfo = ConvertContextUtils.AddComponent<ObjBasicInfo>(go);
            bInfo.DownloadName = model.Name;
            bInfo.FromServer = model.FromServer;
            bInfo.Id = model.Id;
        }
    }
}
