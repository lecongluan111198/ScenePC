using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LessonHandler : MonoBehaviour
{
    [Header("Prefab")]
    public GameObject contextItem;

    [Header("Container")]
    public GameObject list;

    [Header("RESOURCE")]
    public Animator loadingAnim;

    private List<GameObject> contexts = new List<GameObject>();
    private int length = 12;
    private int currentOwnOffset = -1;

    public void LoadLesson()
    {
        if (currentOwnOffset < 0)
        {
            currentOwnOffset = 0;
            loadingAnim.Play("Modal Window In");
            ContextModel.Instance.loadOwnContext(length, currentOwnOffset, (data) =>
            {
                if (data != null)
                {
                    foreach (Context context in data)
                    {
                        GameObject item = Instantiate(contextItem, list.transform, false);
                        item.GetComponent<LessonItem>().UpdateInformation(context);
                        item.transform.SetParent(list.transform);
                        contexts.Add(item);
                    }
                }
                loadingAnim.Play("Modal Window Out");
            });
        }
    }
}
