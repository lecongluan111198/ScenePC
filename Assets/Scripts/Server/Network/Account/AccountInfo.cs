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
    private string session = "eyJhbGciOiJIUzUxMiJ9.eyJzdWIiOiIxIiwiZXhwIjoxNTkxODAyMzU3LCJpYXQiOjE1ODkyMTAzNTd9.F3SB8vv9AZHpJSUqR50VSPCWdci8hBR1Ort9jsDYQOqv6cmyBh6P_gbFFdvturNOZKbhvmVpuIjM9ydh8iBxJg";
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
