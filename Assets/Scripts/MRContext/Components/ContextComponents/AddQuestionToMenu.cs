using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class AddQuestionToMenu : MonoBehaviour
{
    // Start is called before the first frame update
    [Header("MENU SETTINGS")]
    public GameObject content;
    public GameObject menuPopup;
    public Text notify;
    [Header("QUESTION")]
    public string questionText;
    public List<string> choose;
    public int answer;
    void Start()
    {
        show();
    }
    private void show()
    {
        string pathAnswer = "Assets/Prefabs/UI/Buttons/Question.prefab";
        int tmp = 0;
        foreach (var i in choose)
        {
            GameObject button = (GameObject)Instantiate((GameObject)AssetDatabase.LoadAssetAtPath(pathAnswer, typeof(GameObject)));
            button.name = i;
            button.GetComponentInChildren<TextMeshProUGUI>().text = i;
            Button buttonCtrl = button.GetComponent<Button>();
            button.transform.SetParent(content.transform);
            if (answer != tmp)
            {
                buttonCtrl.onClick.AddListener(() => answerWrong());
            }
            else
            {
                buttonCtrl.onClick.AddListener(() => answerCorrect());
            }
            tmp++;
        }
    }
    private void answerCorrect()
    {
        notify.text = "CHÚC MỪNG BẠN! CÂU TRẢ LỜI CỦA BẠN HOÀN TOÀN CHÍNH XÁC";
        menuPopup.SetActive(true);
    }
    private void answerWrong()
    {
        notify.text = "CÂU TRẢ LỜI CỦA BẠN CHƯA CHÍNH XÁC, XEM LẠI BÀI GIẢNG NHÉ!";
        menuPopup.SetActive(true);
    }
}
