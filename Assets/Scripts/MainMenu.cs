using UnityEngine;
using System.Collections;

public class MainMenu : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void OnClick () {
		var csv = GameObject.Find ("CSVReader").GetComponent<CSVReader> ().SplitCsvGrid("");
		Debug.Log (csv.Length);
	}
}
