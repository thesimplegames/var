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
	GameObject _sentWindow;

	Process _colorProc;

	void Start() {
		_sentWindow = GameObject.FindGameObjectWithTag ("Sent");

		if (_sentWindow == null)
			throw new NullReferenceException ("Mail.cs: no Sent Window GO or active==false");

		_sentWindow.SetActive (false);
	}

	void Update (){
		if (_sentWindow == null)
			return;

		if (_colorProc == null)
			return;

		_colorProc.Update ();
		var color = _sentWindow.GetComponent<Image> ().color;
		_sentWindow.GetComponent<Image> ().color = new Color (color.r, color.g, color.b, _colorProc.Progress);

		if (!_colorProc.IsFinished)
			return;

		_colorProc = null;
		_sentWindow.GetComponent<Image> ().color = new Color (color.r, color.g, color.b, 1);
	}

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

	//string imgPath = "https://drive.google.com/open?id=0B0Qb8V3AHw-SNlVUM3A0enZQUXc";

	string GetItems() {
		
		string result = "<center><table width='100%'>";

		result += "<tr><td><img width='100%' src='http://icba.echt.me/email_header.png'></td></tr>";
		result += "<tr align='center'><td align='center' width='90%'><table'><div align = 'center' width='90%'>";

		foreach (var item in InventoryItems.Instance.liked) {
			var tr = InventoryItems.Instance._items [item.Key];
			var picNumber = tr.FindChild ("Hidden").GetComponent<Text> ().text;
			if (picNumber.ToString ().Length != 0) {
				
				result += "<tr><td><br><h1 style='color:#ce4753'>" + tr.FindChild ("Title").GetComponent<Text> ().text + "</h1><td></tr>";
				result += "<tr><td><img width='100%' src='http://icba.echt.me/" + picNumber + ".jpg'></td></tr>";

				if (!tr.FindChild ("Text").GetComponent<Text> ().text.Contains ("Tap on play"))
					result += "<tr><td><h3>" + tr.FindChild ("Text").GetComponent<Text> ().text + "</h3><br><br></td></tr>";
			}
		}
		result += "</div></td></tr></table>";
		result += "<tr><td><img width='100%' src='http://icba.echt.me/email_footer.png'></td></tr>";
		result += "</table></center>";
		Debug.Log (result);
		return result;
		//http://icba.echt.me/7.jpg      http://octavacentre.com/temp/
	}

	void SendMessage(string path) {
        MailMessage mail = new MailMessage();
		mail.From = new MailAddress("emiratessoilmuseum@gmail.com", "Emirates Soil Museum");
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
		//returnButton.onClick.Invoke ();

		_colorProc = new Process (1f, false, null);
		_sentWindow.SetActive (true);
		Update ();

		Debug.Log ("Message sended");

		//Debug.Log ("success");

    }
		

}