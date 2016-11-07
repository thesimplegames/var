using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class InventoryItems : MonoBehaviour {

    public static InventoryItems Instance;
    Dictionary<string, Transform> _items;
	// Use this for initialization
	void Start () {
        Instance = this;

        _items = new Dictionary<string, Transform>();
        RectTransform rt = gameObject.GetComponent<RectTransform>();
        GameObject prefab = Resources.Load<GameObject>("prefabs/inventoryItem");
        //GameObject prefab = transform.GetChild(0).gameObject;
        for (int i = 0; i < 80; i++) {
            GameObject newGO = Instantiate(prefab);
            newGO.transform.SetParent(transform);
            newGO.GetComponent<RectTransform>().localPosition = new Vector3(0, -333 * i + 800 + 333, 0);
            newGO.name =  i.ToString(); 
            rt.position = new Vector3(rt.position.x, rt.position.y - 333 / 2, rt.position.z);
            rt.sizeDelta = new Vector2(rt.sizeDelta.x, rt.sizeDelta.y);
            _items.Add(newGO.name, newGO.transform);
        }
	}

    void SetState(string name, string header, string text, Sprite pic, Sprite starPic) {
        if (!_items.ContainsKey(name))
            return;

        _items[name].FindChild("Title").GetComponent<Text>().text = header;
        _items[name].FindChild("Text").GetComponent<Text>().text = text;
        _items[name].FindChild("Picture").GetComponent<Image>().sprite = pic;
        _items[name].FindChild("Start").GetComponent<Image>().sprite = starPic;
    }

}
