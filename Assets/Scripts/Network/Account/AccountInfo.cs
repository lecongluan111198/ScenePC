﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AccountInfo
{
    private static AccountInfo instance = null;
    private int uid;
    private string username;
    private string email;
    private string role;
    private string session = "eyJhbGciOiJIUzUxMiJ9.eyJzdWIiOiIxIiwiZXhwIjoxNTg2MTYyNzIwLCJpYXQiOjE1ODM1NzA3MjB9.cCb-jUHdLVlCtDzCAx1vGlimeHTSV52OIN3vSdT0SPZCdCoj4dmZOT2BjuVyRBrc_eqQgkHwzBN4tqgDe8ehIg";
    private byte[] image;


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

    //public int UID
    //{
    //    get { return uid; }
    //    set { uid = value; }
    //}

    //public string Username
    //{
    //    get { return username; }
    //    set { username = value; }
    //}

    //public string Email
    //{
    //    get { return email; }
    //    set { email = value; }
    //}

    //public string Session
    //{
    //    get { return sessionKey; }
    //    set { sessionKey = value; }
    //}

    public string Role { get => role; set => role = value; }
    public byte[] Image { get => image; set => image = value; }
    public int UID { get => uid; set => uid = value; }
    public string Username { get => username; set => username = value; }
    public string Email { get => email; set => email = value; }
    public string Session { get => session; set => session = value; }

    public void reset()
    {
        uid = -1;
        username = "";
        email = "";
        session = "";
        role = "";
        image = null;
    }
}
