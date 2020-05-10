using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ContextItem : MonoBehaviour
{
    [Header("COURSE CONTEXT ITEM")]
    public TextMeshProUGUI contextTitle;

    private Context context;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void UpdateInformation(Context context)
    {
        this.context = context;
        this.contextTitle.text = context.Name; ;
    }

    public Context getContext()
    {
        return context;
    }
}
