using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AccountInfo
{
    private static AccountInfo instance = null;
    private int uid = 1;
    private string username = "LuanLee";
    private string email = "lecongluan111198@gmail.com";
    private string role = "ADMIN";
    private string session = "eyJhbGciOiJIUzUxMiJ9.eyJzdWIiOiIxIiwiZXhwIjoxNTkyMjA4ODYyLCJpYXQiOjE1ODk2MTY4NjJ9.nOvz8nq8v3S-P-wAY9_8LRNSZEdQheS2mbvyGJMNURhCvfuPi8u8U8EMGPL7GeWubBaFbCcI3Nc4c8XivoUWTg";
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
