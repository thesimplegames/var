using UnityEngine;
using System.Collections;

public class EnableCanvas : MonoBehaviour {

	public static EnableCanvas Instance;

	public GameObject iPad;
	public GameObject iPhone6;
	public GameObject iPhone6plus;
	public bool IsIpad {
		get { return iPad.active; }
	}

	// Use this for initialization
	void Awake () {
		Instance = this;

		//Debug.LogWarning(Screen.width);
		if (Screen.width != 1536)
			iPad.SetActive (false);

		if (Screen.width != 750)
			iPhone6.SetActive (false);

		if (Screen.width != 1080)
			iPhone6plus.SetActive (false);

		if (Screen.width == 640) {
			iPhone6.SetActive (true);

			for (int i = 0; i < iPhone6.transform.childCount; i++) {
				iPhone6.transform.GetChild (i).position += Vector3.up * 100;
			}

			iPhone6.transform.position += Vector3.up * 100;
		}
			
		TrackingDetector.mapButtonAsFlagToKnowWeAreOnTheCameraScreen = 
			GameObject.FindGameObjectWithTag ("Player");

		TrackingDetector.mapButtonAsFlagToKnowWeAreOnTheCameraScreen.SetActive (false);

		Debug.Log (TrackingDetector.mapButtonAsFlagToKnowWeAreOnTheCameraScreen.active);

	}
	
	// Update is called once per frame
	void Update () {
		//iPad.SetActive (false);
		//iPhone6.SetActive (false);
		//iPhone6plus.SetActive (false);

	
	}
}
