using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using Vuforia;
public class ContentManager : MonoBehaviour {

    public static ContentManager Instance { get; set; }

    public Text header;
    public Text text;
    public GameObject star;
    public UnityEngine.UI.Image picture;
    Sprite _fullStar;
    Sprite _emptyStar;
    string _currentName;
    string _position;
    public GameObject playButton;


    class Item {
        public string Name;
        public bool isLiked = false;
    }

	bool apply = false;

    Dictionary<string, Item> _items;

	int cnt = 0;

	void Update () {
		//return;
		cnt++;

		if (cnt < 100)
			return;

		cnt = 0;

		System.GC.Collect();
		Resources.UnloadUnusedAssets();

	}

	// Use this for initialization
	void Start () {
        Instance = this;

        if (header == null || text == null || star == null)
            throw new System.NullReferenceException();

		_emptyStar = star.GetComponent<UnityEngine.UI.Image>().sprite;
		_fullStar = star.transform.GetChild(0).GetComponent<UnityEngine.UI.Image>().sprite;
        _items = new Dictionary<string, Item>();
        playButton.SetActive(false);
	}
	
    public void Set(string name) {
		//System.GC.Collect ();
		if (apply)
			ApplyStar ();

        if (name.Contains("_") && name.Length > 1) {
            if (name[1] == '_')
                name = "" + name[0];

            if (name.Length > 2)
                if (name[2] == '_')
                    name = "" + name[0] + name[1];
        }


        _currentName = name;
        if (!_items.ContainsKey(name))
            _items.Add(name, new Item());
		star.GetComponent<UnityEngine.UI.Image>().sprite = _items[name].isLiked ? _fullStar : _emptyStar;

        var grid = CSVReader.Instance.grid;
        for (int i = 0; i < grid.GetLength(1); i++) {
            if (name == grid[0, i]) {
				apply = true;
                _position = grid[3, i];
				Sprite sprite = Resources.Load<Sprite>("media/" + name);
                int nameInt;
                int.TryParse(name, out nameInt);
				playButton.SetActive(name == "21" || name == "24");
                Set(grid[1, i], grid[2, i], sprite as Sprite);
                return;
            }
        }
    }

    public void Set(string newHeader, string newText, Sprite newSprite) {
        header.text = newHeader;
        text.text = newText;
        picture.sprite = newSprite;
    }


    public void StarClick() {
        _items[_currentName].isLiked = !_items[_currentName].isLiked;
		star.GetComponent<UnityEngine.UI.Image>().sprite = _items[_currentName].isLiked ? _fullStar : _emptyStar;
    }

    public void ApplyStar() {
        //var grid = CSVReader.Instance.grid;
		TrackingDetector.mapButtonAsFlagToKnowWeAreOnTheCameraScreen.SetActive(true);
		Debug.Log(_items[_currentName]);
		MapController.Instance.SetPosition(_position, _currentName, _items[_currentName].isLiked);
		InventoryItems.Instance.Set(_currentName, header.text, text.text, picture.sprite, _items[_currentName].isLiked);
		apply = false;
    }

    public void PlayVideo() {
        Debug.Log("Play video " + _currentName);
        Handheld.PlayFullScreenMovie("movies/" + _currentName + ".mp4");
    }

 
}
