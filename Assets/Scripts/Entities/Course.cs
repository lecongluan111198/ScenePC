using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Course
{
    int id;
    int teacherId;
    int avatarId;
    string name;
    string status;
    string discription;
    List<int> contexts;

    public int Id { get => id; set => id = value; }
    public int TeacherId { get => teacherId; set => teacherId = value; }
    public string Name { get => name; set => name = value; }
    public string Status { get => status; set => status = value; }
    public string Discription { get => discription; set => discription = value; }
    public List<int> Contexts { get => contexts; set => contexts = value; }
    public int AvatarId { get => avatarId; set => avatarId = value; }
}
