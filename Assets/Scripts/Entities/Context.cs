using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Context
{
    private int id;
    private int teacherId;
    private string name;
    private string description;
    private string content;

    public int Id { get => id; set => id = value; }
    public int TeacherId { get => teacherId; set => teacherId = value; }
    public string Name { get => name; set => name = value; }
    public string Description { get => description; set => description = value; }
    public string Content { get => content; set => content = value; }
}
