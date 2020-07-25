using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuModelPanel : MonoBehaviour
{
    public GameObject objList;
    public GameObject modelItem;
    List<Model> models = new List<Model>();
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
        if (models.Count == 0)
        {
            Debug.Log("start load model");
            ModelModel.Instance.loadAll((data) =>
            {
                if (data != null)
                {
                    models = data;
                    loadModels();
                }
            });
        }
    }

    private void loadModels()
    {
        Debug.Log("models");
        foreach (Model model in models)
        {
            GameObject go = Instantiate(modelItem, objList.transform, false);
            go.transform.localPosition = new Vector3(0, 0, 0);
            go.transform.SetParent(objList.transform);
            go.name = model.Name;
            ModelItem item = go.GetComponent<ModelItem>();
            item.UpdateInformation(model);
        }
    }

    public void Close()
    {
        gameObject.SetActive(false);
        //TagAlongManager.Instance.ControllerOn();
        SettingMenuPanel.Instance.OnController();
    }
}
