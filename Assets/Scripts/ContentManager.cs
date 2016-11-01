using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ContentManager : MonoBehaviour {

    public static ContentManager Instance { get; set; }

    public Text header;
    public Text text;


	// Use this for initialization
	void Start () {
        Instance = this;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void Set(string name) {
        if (name.Contains("_") && name.Length > 1) {
            if (name[1] == '_')
                name = "" + name[0];

            if (name.Length > 2)
                if (name[2] == '_')
                    name = "" + name[0] + name[1];
        }

        for (int i = 0; i < CSVReader.Instance.grid.GetLength(1); i++) {
            if (name == CSVReader.Instance.grid[0, i]) {
                Set(CSVReader.Instance.grid[1, i], CSVReader.Instance.grid[2, i]);
                return;
            }
        }

    }

    public void Set (string newHeader, string newText) {
        header.text = newHeader;
        text.text = newText;
    }
}
