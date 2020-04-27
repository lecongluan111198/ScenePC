using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Course
{
    int id;
    int teacherId;
    int avatarId;
    string name;
    bool status;
    string type;
    string description;
    private string author;
    private long createTime;
    List<Context> contexts;

    public int Id { get => id; set => id = value; }
    public int TeacherId { get => teacherId; set => teacherId = value; }
    public string Name { get => name; set => name = value; }
    public bool Status { get => status; set => status = value; }
    public string Description { get => description; set => description = value; }
    public List<Context> Contexts { get => contexts; set => contexts = value; }
    public int AvatarId { get => avatarId; set => avatarId = value; }
    public string Author { get => author; set => author = value; }
    public long CreateTime { get => createTime; set => createTime = value; }
    public string Type { get => type; set => type = value; }
}
