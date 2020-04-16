using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoginHandler : MonoBehaviour
{
    [Header("RESOURCE")]
    public Animator loadingAnim;

    [Header("INFORMATION")]
    public TMP_Text role;
    public TMP_InputField email;
    public TMP_InputField password;
    public TMP_Text errorMessage;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void _Login()
    {
        loadingAnim.Play("Modal Window In");
        //check login
        Debug.Log(email.text + " " + password.text);
        Debug.Log(UserModel.Instance);
        UserModel.Instance.Login(email.text, password.text, (isSucess) =>
        {
            if (isSucess)
            {
                //load new scene
                Debug.Log("success");
                SceneManager.LoadScene("MainBoard");
            }
            else
            {
                Debug.Log("fail");
                errorMessage.text = "Email or password are wrong!";
                loadingAnim.Play("Modal Window Out");
            }
        });
    }

}
