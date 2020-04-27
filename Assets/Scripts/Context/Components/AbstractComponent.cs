using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbstractComponent : MonoBehaviour
{
    public class ObjectComponent
    {
        public int id { get; set; }
        public string name { get; set; }
        public ObjectComponent(int id, string name)
        {
            this.id = id;
            this.name = name;
        }
        public ObjectComponent()
        {
            id = 0;
            name = "";
        }
    }
    public class QuestionComponent : ObjectComponent
    {
        public string question { get; set; }
        public List<string> choose { get; set; }
        public string answer { get; set; }
        public QuestionComponent(string question, List<string> choose, string answer)
        {
            this.question = question;
            this.choose = choose;
            this.answer = answer;
        }
    }
    private void loadQuestion()
    {

    }
    public void loadComponent()
    {
        ObjectComponent objC = new ObjectComponent();
        switch (objC.id)
        {
            case 1:
                loadQuestion();
                break;
            case 2:
                
                break;
            default:
                Debug.Log("ID Component not found");
                break;
        }
        
    }
}
