using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Dictionary<string, object> test = new Dictionary<string, object>();
        test.Add("a", "b");
        test.Add("c", 1234678979789879.5);
        string json = JsonConvert.SerializeObject(test);
        Debug.Log(json);
        Dictionary<string, object> result = JsonConvert.DeserializeObject<Dictionary<string, object>>(json);
        Debug.Log(result["c"].GetType());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
