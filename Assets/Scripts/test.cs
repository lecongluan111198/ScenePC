using Newtonsoft.Json;
using SimpleJSON;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //string str = "{\"result\":[{\"id\":1,\"teacherId\":1,\"name\":\"course1\",\"status\":true,\"avatarId\":1,\"type\":\"Education\",\"description\":\"new test\",\"creatTime\":1587616140873,\"contexts\":[]},{\"id\":2,\"teacherId\":1,\"name\":\"course2\",\"status\":true,\"avatarId\":1,\"type\":\"Education\",\"description\":\"test1\",\"creatTime\":1587616147383,\"contexts\":[]},{\"id\":3,\"teacherId\":1,\"name\":\"course3\",\"status\":true,\"avatarId\":1,\"type\":\"Education\",\"description\":\"test1\",\"creatTime\":1587616151172,\"contexts\":[]},{\"id\":4,\"teacherId\":1,\"name\":\"course4\",\"status\":true,\"avatarId\":1,\"type\":\"Education\",\"description\":\"test1\",\"creatTime\":1587616155316,\"contexts\":[]},{\"id\":5,\"teacherId\":1,\"name\":\"course5\",\"status\":true,\"avatarId\":1,\"type\":\"Education\",\"description\":\"test1\",\"creatTime\":1587616158841,\"contexts\":[]},{\"id\":6,\"teacherId\":1,\"name\":\"course6\",\"status\":true,\"avatarId\":1,\"type\":\"Education\",\"description\":\"test1\",\"creatTime\":1587616163729,\"contexts\":[]},{\"id\":7,\"teacherId\":1,\"name\":\"course7\",\"status\":true,\"avatarId\":1,\"type\":\"Education\",\"description\":\"test1\",\"creatTime\":1587626796264,\"contexts\":[]},{\"id\":8,\"teacherId\":1,\"name\":\"course8\",\"status\":true,\"avatarId\":1,\"type\":\"Education\",\"description\":\"test1\",\"creatTime\":1587626800198,\"contexts\":[]},{\"id\":9,\"teacherId\":1,\"name\":\"course9\",\"status\":true,\"avatarId\":1,\"type\":\"Education\",\"description\":\"test1\",\"creatTime\":1587626803847,\"contexts\":[]},{\"id\":10,\"teacherId\":1,\"name\":\"course10\",\"status\":true,\"avatarId\":1,\"type\":\"Education\",\"description\":\"test1\",\"creatTime\":1587626807514,\"contexts\":[]},{\"id\":11,\"teacherId\":1,\"name\":\"course11\",\"status\":true,\"avatarId\":1,\"type\":\"Education\",\"description\":\"test1\",\"creatTime\":1587626810883,\"contexts\":[]},{\"id\":12,\"teacherId\":1,\"name\":\"course12\",\"status\":true,\"avatarId\":1,\"type\":\"Education\",\"description\":\"test1\",\"creatTime\":1587626814254,\"contexts\":[]}],\"error\":0,\"message\":\"success\"}";
        //APIResponse response = APIResponse.textToReponse(str);
        //string json = response.getStringParam("result", "cannot parse");
        //Debug.Log(json);
        //List<Course> courses = JsonConvert.DeserializeObject<List<Course>>(json);
        //foreach(Course course in courses)
        //{
        //    Debug.Log(course.Name);
        //}
        TimeSpan time = TimeSpan.FromMilliseconds(1587740328899);
        DateTime dt = new DateTime(1970, 1, 1) + time;
        Debug.Log(dt.ToShortDateString());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
