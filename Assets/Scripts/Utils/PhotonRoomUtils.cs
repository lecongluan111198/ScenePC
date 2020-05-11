using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Photon.Pun;
using Photon.Realtime;

public class PhotonRoomUtils
{
    private static Random random = new Random();

    public static string GetRoomName()
    {
        string mess = AccountInfo.Instance.UID + RandomString(5);
        return mess;
    }

    private static string RandomString(int length)
    {
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        return new string(Enumerable.Repeat(chars, length)
          .Select(s => s[random.Next(s.Length)]).ToArray());
    }

}
