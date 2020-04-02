using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AccountInfo
{
    private static AccountInfo instance = null;
    private int uid;
    private string username;
    private string email;
    private string sessionKey = "eyJhbGciOiJIUzUxMiJ9.eyJzdWIiOiIxIiwiZXhwIjoxNTg2MTYyNzIwLCJpYXQiOjE1ODM1NzA3MjB9.cCb-jUHdLVlCtDzCAx1vGlimeHTSV52OIN3vSdT0SPZCdCoj4dmZOT2BjuVyRBrc_eqQgkHwzBN4tqgDe8ehIg";


    public static AccountInfo Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new AccountInfo();
            }
            return instance;
        }
    }

    public int UID
    {
        get { return uid; }
        set { uid = value; }
    }

    public string Username
    {
        get { return username; }
        set { username = value; }
    }

    public string Email
    {
        get { return email; }
        set { email = value; }
    }

    public string Session
    {
        get { return sessionKey; }
        set { sessionKey = value; }
    }
}
