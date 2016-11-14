using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class ContentManager : MonoBehaviour {

    public static ContentManager Instance { get; set; }

    public Text header;
    public Text text;
    public GameObject star;
    public Image picture;
    Sprite _fullStar;
    Sprite _emptyStar;
    string _currentName;
    string _position;
    public GameObject playButton;


    class Item {
        public string Name;
        public bool isLiked = false;
    }

    Dictionary<string, Item> _items;

	// Use this for initialization
	void Start () {
        Instance = this;

        if (header == null || text == null || star == null)
            throw new System.NullReferenceException();

        _emptyStar = star.GetComponent<Image>().sprite;
        _fullStar = star.transform.GetChild(0).GetComponent<Image>().sprite;
        _items = new Dictionary<string, Item>();
        playButton.SetActive(false);
	}
	
    public void Set(string name) {
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
        star.GetComponent<Image>().sprite = _items[name].isLiked ? _fullStar : _emptyStar;

        var grid = CSVReader.Instance.grid;
        for (int i = 0; i < grid.GetLength(1); i++) {
            if (name == grid[0, i]) {
                //MapController.Instance.SetPosition(grid[3, i]);
                _position = grid[3, i];
                Sprite sprite = Resources.Load<Sprite>("media/" + name);
                int nameInt;
                int.TryParse(name, out nameInt);

                if (name == "21" || name == "24")
                    playButton.SetActive(true);

                //if (sprite == null || (nameInt > 20 && nameInt < 42))
                  //  sprite = Resources.Load<Sprite>("media/1");
                Set(grid[1, i], grid[2, i], sprite as Sprite);
                //InventoryItems.Instance.Set(name, grid[1, i], grid[2, i], sprite as Sprite, _emptyStar);
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
        star.GetComponent<Image>().sprite = _items[_currentName].isLiked ? _fullStar : _emptyStar;
    }

    public void ApplyStar() {
        //var grid = CSVReader.Instance.grid;
        MapController.Instance.SetPosition(_position, name, _items[_currentName].isLiked);
        InventoryItems.Instance.Set(name, header.text, text.text, picture.sprite, _items[_currentName].isLiked);
    }

    public void PlayVideo() {
        Debug.Log("Play video " + _currentName);
        Handheld.PlayFullScreenMovie("movies/" + _currentName + ".mp4");
    }

    public void TestVideo(string name) {
        Debug.Log("Play video " + name);
        Handheld.PlayFullScreenMovie("movies/" + name + ".mp4");
    }
}
