using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Net.Mail;
using UnityEngine;

public class SendingEmailUtils
{
    static bool mailSent = false;
    private static string mailFrom = "luan.le.hcmus@gmail.com";

    private static void SendCompletedCallback(object sender, AsyncCompletedEventArgs e)
    {
        // Get the unique identifier for this asynchronous operation.
        string token = (string)e.UserState;

        if (e.Cancelled)
        {
            Debug.Log("[" + token + "] Send canceled.");
        }
        if (e.Error != null)
        {
            Debug.Log("[" + token + "] " + e.Error.ToString());
        }
        else
        {
            Debug.Log("Message sent.");
        }
        mailSent = true;
    }
    public static void SendMail ()
    {
        // Command-line argument must be the SMTP host.
        SmtpClient client = new SmtpClient("smtp.gmail.com", 465);
        // Specify the email sender.
        // Create a mailing address that includes a UTF8 character
        // in the display name.
        MailAddress from = new MailAddress(mailFrom,
           "Jane " + (char)0xD8 + " Clayton",
        System.Text.Encoding.UTF8);
        // Set destinations for the email message.
        MailAddress to = new MailAddress(AccountInfo.Instance.Email);
        // Specify the message content.
        MailMessage message = new MailMessage(from, to);
        message.Body = "This is a test email message sent by an application. ";
        // Include some non-ASCII characters in body and subject.
        string someArrows = new string(new char[] { '\u2190', '\u2191', '\u2192', '\u2193' });
        message.Body += Environment.NewLine + someArrows;
        message.BodyEncoding = System.Text.Encoding.UTF8;
        message.Subject = "test message 1" + someArrows;
        message.SubjectEncoding = System.Text.Encoding.UTF8;
        // Set the method that is called back when the send operation ends.
        client.SendCompleted += new
        SendCompletedEventHandler(SendCompletedCallback);
        // The userState can be any object that allows your callback
        // method to identify this send operation.
        // For this example, the userToken is a string constant.
        string userState = "test message1";
        client.SendAsync(message, userState);
        Debug.Log("Sending message... press c to cancel mail. Press any other key to exit.");
        string answer = Console.ReadLine();
        // If the user canceled the send, and mail hasn't been sent yet,
        // then cancel the pending operation.
        if (answer.StartsWith("c") && mailSent == false)
        {
            client.SendAsyncCancel();
        }
        // Clean up.
        message.Dispose();
        Debug.Log("Goodbye.");
    }
}
