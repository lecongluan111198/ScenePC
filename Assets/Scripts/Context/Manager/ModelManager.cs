using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class ModelManager : MonoBehaviour
{
   //define
    public class managerSource
    {
        public int id { get; set; }
        public string name { get; set; }
    }
    public managerSource checkInList(int id, List<managerSource> list)
    {
        foreach(managerSource s in list)
        {
            Debug.Log("id " + s);
            if (s.id == id)
                return s;
        }
        return null;
    }
    public void loadObjectFromSource(int id, managerSource tmp)
    {
        var path = Path.Combine(Application.dataPath, "source.json");
        var dataManager = File.ReadAllText(path);
        Debug.Log(dataManager);
        List<managerSource> ms = JsonConvert.DeserializeObject<List<managerSource>>(dataManager);
        Debug.Log(ms);
        tmp = checkInList(id, ms);
    }
}
