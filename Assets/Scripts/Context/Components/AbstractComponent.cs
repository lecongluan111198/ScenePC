using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbstractComponent : MonoBehaviour
{
    public class ObjectComponent
    {
        public int idcpn { get; set; }
        public string namecpn { get; set; }
        public ObjectComponent(int id, string name)
        {
            this.idcpn = id;
            this.namecpn = name;
        }
        public ObjectComponent()
        {
            idcpn = 0;
            namecpn = "";
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
        switch (objC.idcpn)
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
