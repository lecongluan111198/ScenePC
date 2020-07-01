using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Model
{
    int id;
    string name;
    bool fromServer;
    string template;

    public int Id { get => id; set => id = value; }
    public string Name { get => name; set => name = value; }
    public bool FromServer { get => fromServer; set => fromServer = value; }
    public string Template { get => template; set => template = value; }
}
