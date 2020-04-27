using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoginSignUpHandlers : MonoBehaviour
{
    [Header("LOGIN INFORMATION")]
    public TMP_Text role;
    public TMP_InputField email;
    public TMP_InputField password;
    public TMP_Text errorLoginMessage;

    [Header("SIGNUP INFORMATION")]
    public TMP_Text signupRole;
    public TMP_Text signupGender;
    public TMP_InputField signupUsername;
    public TMP_InputField signupEmail;
    public TMP_InputField signupPassword;
    public TMP_InputField signupConfirmPassword;
    public TMP_Text errorSignUpMessage;

    [Header("Authentication")]
    public TMP_InputField verifyCode;
    public TMP_Text errorAuthenMessage;
    public TMP_Text infoAuthenMessage;

    [Header("PANEL")]
    public GameObject authenticationPanel;
    public GameObject signUpPanel;

    [Header("RESOURCE")]
    public Animator loadingAnim;
    public Animator signUpAnim;

    private bool isOffsignUpPanel = false;

    private bool _IsEmpty(string text)
    {
        return text == null || text == "";
    }

    private void Start()
    {
        //signUpAnim.Play("Normal");
    }
    private void _EmptyMessage()
    {
        errorSignUpMessage.text = "";
        errorAuthenMessage.text = "";
        infoAuthenMessage.text = "";
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
                errorLoginMessage.text = "Email or password are wrong!";
                loadingAnim.Play("Modal Window Out");
            }
        });
    }

    public void _SignUp()
    {
        //_EmptyMessage();
        //if (!_IsEmpty(signupRole.text) && !_IsEmpty(signupGender.text) && !_IsEmpty(signupUsername.text) && !_IsEmpty(signupEmail.text) && !_IsEmpty(signupPassword.text) && !_IsEmpty(signupConfirmPassword.text))
        //{
        //    errorSignUpMessage.text = "Please fill out the form!";
        //    return;
        //}
        //if (signupPassword.text != signupConfirmPassword.text)
        //{
        //    errorSignUpMessage.text = "Password and confirm password aren't the same!";
        //    return;
        //}
        Debug.Log("Signup");
        //signUpAnim.Play("VerifyCode");
        signUpAnim.SetTrigger("VerifyCode");
        //loadingAnim.Play("Modal Window In");
        //UserModel.Instance.Register(signupEmail.text, signupUsername.text, signupPassword.text, signupRole.text, (isSuccess) =>
        //{
        //    loadingAnim.Play("Modal Window Out");
        //    if (isSuccess)
        //    {
        //        signUpAnim.Play("VerifyCode");
        //    }
        //    else
        //    {
        //        errorSignUpMessage.text = "Cannot register new account!";
        //    }
        //});
    }

    public void _Authentication()
    {
        _EmptyMessage();
        if (!_IsEmpty(verifyCode.text))
        {
            errorAuthenMessage.text = "Empty verification code!";
            return;
        }
        loadingAnim.Play("Modal Window In");
        UserModel.Instance.Authentication(verifyCode.text, (isSuccess) =>
        {
            loadingAnim.Play("Modal Window Out");
            if (isSuccess)
            {
                SceneManager.LoadScene("MainBoard");
            }
            else
            {
                Debug.Log("fail");
                errorAuthenMessage.text = "Wrong verification code!";
            }
        });
    }

    public void _ResendVerifyCode()
    {
        _EmptyMessage();
        loadingAnim.Play("Modal Window In");
        UserModel.Instance.ResendVerifyCode((isSuccess) =>
        {
            loadingAnim.Play("Modal Window Out");
            if (isSuccess)
            {
                infoAuthenMessage.text = "Resend verification code successfully!";
            }
            else
            {
                errorAuthenMessage.text = "Resend verification code failed!";
            }
        });
    }
}
