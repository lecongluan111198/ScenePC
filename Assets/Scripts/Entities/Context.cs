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
    private int avatarId;
    private string author;
    private long createTime;
    //private long viewer;

    public int Id { get => id; set => id = value; }
    public int TeacherId { get => teacherId; set => teacherId = value; }
    public string Name { get => name; set => name = value; }
    public string Description { get => description; set => description = value; }
    public string Content { get => content; set => content = value; }
    public int AvatarId { get => avatarId; set => avatarId = value; }
    public string Author { get => author; set => author = value; }
    public long CreateTime { get => createTime; set => createTime = value; }
}
