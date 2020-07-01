using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ModelItem : MonoBehaviour
{
    [Header("INFORMATION")]
    public Text text;

    private Model model;

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
            ObjBasicInfo bInfo = ConvertContextUtils.addComponent<ObjBasicInfo>(go);
            bInfo.DownloadName = model.Name;
            bInfo.FromServer = model.FromServer;
            bInfo.Id = model.Id;
        }
    }
}
