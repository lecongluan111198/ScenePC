using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LessonHandler : MonoBehaviour
{
    [Header("Prefab")]
    public GameObject contextItem;

    [Header("Container")]
    public GameObject list;

    private List<GameObject> contexts = new List<GameObject>();
    private int length = 12;

    public void LoadLesson()
    {
        ContextModel.Instance.loadOwnContext(length, 0, (data) =>
        {
            if (data != null)
            {
                foreach (Context context in data)
                {
                    GameObject item = Instantiate(contextItem, list.transform, false);
                    item.GetComponent<ContextItem>().LoadData(context);
                    item.transform.SetParent(list.transform);
                    contexts.Add(item);
                }
            }
        });
    }

}
