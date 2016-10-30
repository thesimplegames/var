using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TargetLogger : MonoBehaviour {

    public static TargetLogger Instance;


    public List<string> targets;
    public string output = "";

    void Enable ()
    {
        targets = new List<string>();
    }

	// Use this for initialization
	void Start () {
        Instance = this;
	}
	
	// Update is called once per frame
	void OnGUI () {
        //GUI.Button(new Rect(Screen.width - 150, 0, 150, Screen.height), output);
	}
}
