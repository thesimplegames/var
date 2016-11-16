using UnityEngine;
using System.Collections;
using System;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using UnityEngine.UI;

public class Mail : MonoBehaviour {

	Texture2D texture = null;
	public InputField pathField;
	public Button returnButton;

	public void GetScreenshot () {
		texture = Resources.Load ("media/21") as Texture2D;
		Debug.Log (texture.ToString ());
	}

	public void TrySendMessage() {
		if (pathField == null)
			throw new Exception ("No input field in mail GO");

		var textPath = pathField.transform.FindChild("Text").GetComponent<Text>();

		if (InventoryItems.Instance.liked.Count == 0) {
			textPath.text = "You have no items in your inventory";
			return;
		}
			
		if (textPath.text.Contains ("@")) {
			SendMessage (textPath.text);
		} else {
			textPath.text = "Enter valid e-mail";
		}
	}

	string imgPath = "https://drive.google.com/open?id=0B0Qb8V3AHw-SNlVUM3A0enZQUXc";

	string GetItems() {
		string result = "<table width='350px'>";

		foreach (var item in InventoryItems.Instance.liked) {
			var tr = InventoryItems.Instance._items [item.Key];
			var picNumber = tr.FindChild ("Hidden").GetComponent<Text> ().text;
			result +="<tr><td><h3>" + tr.FindChild("Title").GetComponent<Text>().text + "</h3><td></tr>";
			result += "<tr><td><img src='http://octavacentre.com/temp/" + picNumber + ".jpg'></td></tr>";

			if (!tr.FindChild("Text").GetComponent<Text>().text.Contains("Tap on play"))
				result +="<tr><td>" + tr.FindChild("Text").GetComponent<Text>().text + "<br><br></td></tr>";
		}

		result += "</table>";
		Debug.Log (result);
		return result;
	}

	void SendMessage(string path) {
        MailMessage mail = new MailMessage();
		mail.From = new MailAddress("emiratessoilmuseum@gmail.com");
		mail.To.Add(path);
        mail.Subject = "Emirates Soil Museum";
		mail.Body = GetItems ();
		mail.IsBodyHtml = true;

		SmtpClient smtpServer = new SmtpClient ("smtp.gmail.com");
		smtpServer.Port = 587;
		smtpServer.Credentials = new System.Net.NetworkCredential ("emiratessoilmuseum@gmail.com", "123a456S") as ICredentialsByHost;
		smtpServer.EnableSsl = true;
		ServicePointManager.ServerCertificateValidationCallback =
	    delegate (object s, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors) {
			return true;
		};
		smtpServer.Send (mail);
		returnButton.onClick.Invoke ();
		Debug.Log ("Message sended");

		//Debug.Log ("success");

    }
		

}