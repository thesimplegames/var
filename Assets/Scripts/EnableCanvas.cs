using UnityEngine;
using System.Collections;

public class EnableCanvas : MonoBehaviour {

	public GameObject iPad;
	public GameObject iPhone6;
	public GameObject iPhone6plus;

	// Use this for initialization
	void Awake () {
		//Debug.LogWarning(Screen.width);
		if (Screen.width != 1536)
			iPad.SetActive (false);

		if (Screen.width != 750)
			iPhone6.SetActive (false);

		if (Screen.width != 1080)
			iPhone6plus.SetActive (false);
	}
	
	// Update is called once per frame
	void Update () {
		//iPad.SetActive (false);
		//iPhone6.SetActive (false);
		//iPhone6plus.SetActive (false);

	
	}
}
