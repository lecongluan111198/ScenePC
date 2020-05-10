using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceManager
{
    public static List<string> COURSE_AVATARS = new List<string>()
    {
        "Images/Backgrounds/Courses/Placeholder 1",
        "Images/Backgrounds/Courses/Placeholder 2",
        "Images/Backgrounds/Courses/Placeholder 3",
        "Images/Backgrounds/Courses/Placeholder 4",
        "Images/Backgrounds/Courses/Placeholder 5",
        "Images/Backgrounds/Courses/Placeholder 6",
        "Images/Backgrounds/Courses/Placeholder 7",
        "Images/Backgrounds/Courses/Placeholder 8",
        "Images/Backgrounds/Courses/Placeholder 9",
        "Images/Backgrounds/Courses/Placeholder 10"
    };

    public static List<string> LESSON_AVATARS = new List<string>()
    {
        "Images/Backgrounds/Lessons/book_icon",
        "Images/Backgrounds/Lessons/city_icon",
        "Images/Backgrounds/Lessons/history_icon",
        "Images/Backgrounds/Lessons/leaf_icon",
        "Images/Backgrounds/Lessons/tree_icon",
        "Images/Backgrounds/Lessons/tube_icon"
    };

    static Dictionary<string, int> COURSE_ID = new Dictionary<string, int>()
    {
        { "Placeholder 1" , 0},
        { "Placeholder 2" , 1},
        { "Placeholder 3" , 2},
        { "Placeholder 4" , 3},
        { "Placeholder 5" , 4},
        { "Placeholder 6" , 5},
        { "Placeholder 7" , 6},
        { "Placeholder 8" , 7},
        { "Placeholder 9" , 8},
        { "Placeholder 10" , 9},
    };


    public static int GetCourseAvatarId(string name)
    {
        if (COURSE_ID.ContainsKey(name))
        {
            return -1;
        }
        return COURSE_ID[name];
    }

}
