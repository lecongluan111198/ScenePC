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
    private string session = "eyJhbGciOiJIUzUxMiJ9.eyJzdWIiOiIxIiwiZXhwIjoxNTkyMjA5NDcxLCJpYXQiOjE1ODk2MTc0NzF9.JiTkRIH4B62pM1PfbmMPBt5yO_dAXOqRD8mXNCEzaNIAXe1BvFaiiK9NELuiPEKiFxch1rMY09qOQExicLPs1g";
    //private string session = "eyJhbGciOiJIUzUxMiJ9.eyJzdWIiOiIxIiwiZXhwIjoxNTkxNzU0NTExLCJpYXQiOjE1ODkxNjI1MTF9.N3kKesMocRBjpH56b5ElGJe5bAbQl4CAA9IRDY9bDFAy_rOP8jpapstLGcm__IcgYVcabqS7czIJSOFno_B7HA";
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
