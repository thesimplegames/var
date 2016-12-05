using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class InventoryItems : MonoBehaviour {

    public static InventoryItems Instance;
    public Dictionary<string, Transform> _items;
	public Dictionary<string, string> liked = new Dictionary<string, string> ();

	public int _recognized = 1;

	Vector3 _startPos;

	public int Recognized {
		get {
			return _recognized;
		}

		set {
			transform.gameObject.GetComponent<RectTransform> ().sizeDelta = 
			new Vector2 (
			transform.gameObject.GetComponent<RectTransform> ().sizeDelta.x,
			transform.gameObject.GetComponent<RectTransform> ().sizeDelta.y + 333f * (_recognized > value ? -1 : 1));
			
			transform.localPosition = new Vector3 (transform.localPosition.x, 
				transform.localPosition.y + 333f * (_recognized > value ? -1 : 1),
				transform.localPosition.z);

			_recognized = value;
		}


	}



	[HideInInspector]
	public GameObject sendButton;
	//public Transform bottom;

	// Use this for initialization
	void Start () {
        Instance = this;
		sendButton = transform.parent.FindChild ("Button").gameObject;
		_startPos = transform.localPosition;


        _items = new Dictionary<string, Transform>();
        RectTransform rt = gameObject.GetComponent<RectTransform>();
        GameObject prefab = Resources.Load<GameObject>("prefabs/inventoryItem");
        //GameObject prefab = transform.GetChild(0).gameObject;
        for (int i = -5; i < 80; i++) {
            GameObject newGO = Instantiate(prefab);
            newGO.transform.SetParent(transform);
			newGO.transform.FindChild ("Picture").gameObject.GetComponent<Image> ().color = new Color (255, 255, 255, 0);
            newGO.GetComponent<RectTransform>().localPosition = new Vector3(0, -333 * i + 475 + 333, 0);
			newGO.transform.localScale = Vector3.one;
            newGO.name =  i.ToString(); 
			newGO.transform.FindChild ("Title").GetComponent<Text> ().text = i.ToString ();
            rt.position = new Vector3(rt.position.x, rt.position.y - 333 / 2, rt.position.z);
            rt.sizeDelta = new Vector2(rt.sizeDelta.x, rt.sizeDelta.y);
            _items.Add(newGO.name, newGO.transform);
        }
	}

    public void Set(string itemName, string header, string text, Sprite pic, bool isLiked) {
        if (!_items.ContainsKey(Recognized.ToString()))
            return;


        //Debug.Log(name);
        if (isLiked) {
			if (!liked.ContainsKey (Recognized.ToString ())) {
				liked.Add (Recognized.ToString (), itemName);
				Debug.Log (liked [Recognized.ToString ()]);
			}

            foreach (var item in _items.Values) {
                if (item.FindChild("Title").GetComponent<Text>().text == header) {
                    return;
                }
            }

//			bottom.position += -333 * Vector3.up;

            _items[Recognized.ToString()].FindChild("Title").GetComponent<Text>().text = header;
            _items[Recognized.ToString()].FindChild("Text").GetComponent<Text>().text = text;
            _items[Recognized.ToString()].FindChild("Picture").GetComponent<Image>().sprite = pic;
			_items[Recognized.ToString()].FindChild("Picture").GetComponent<Image>().color = new Color (255, 255, 255, 255);
			_items[Recognized.ToString()].FindChild("Hidden").GetComponent<Text>().text = itemName;

            Recognized++;
        } else {
			liked.Remove (Recognized.ToString());

            bool flag = false;
            Transform prev = null;
//			bottom.position += 333 * Vector3.up;

            foreach (var item in _items.Values) {
                if (item.FindChild("Title").GetComponent<Text>().text == header) {
                    flag = true;
                    Recognized--;
                    item.FindChild("Title").GetComponent<Text>().text = "";
                    item.FindChild("Text").GetComponent<Text>().text = "";
					item.FindChild("Hidden").GetComponent<Text>().text = "";
                    item.GetComponent<Image>().sprite = new Sprite();
					item.GetComponent<Image>().color = new Color (255, 255, 255, 0);
                }

                if (prev != null) {
					prev.FindChild("Hidden").GetComponent<Text>().text =
						item.FindChild("Hidden").GetComponent<Text>().text;
					prev.FindChild("Title").GetComponent<Text>().text =
						item.FindChild("Title").GetComponent<Text>().text;
                    prev.FindChild("Text").GetComponent<Text>().text =
                        item.FindChild("Text").GetComponent<Text>().text;
                    prev.FindChild("Picture").GetComponent<Image>().sprite =
                        item.FindChild("Picture").GetComponent<Image>().sprite;
					prev.GetComponent<Image> ().color = item.FindChild ("Picture").GetComponent<Image> ().color;

                    item.FindChild("Title").GetComponent<Text>().text = "";
                    item.FindChild("Text").GetComponent<Text>().text = "";
					item.FindChild("Hidden").GetComponent<Text>().text = "";
                    item.GetComponent<Image>().sprite = new Sprite();
					item.GetComponent<Image>().color = new Color (255, 255, 255, 0);
                }

                if (flag)
                    prev = item;
            }
            
        }
    }

}
