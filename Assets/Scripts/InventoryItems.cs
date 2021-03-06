﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class InventoryItems : MonoBehaviour {

    public static InventoryItems Instance;
    Dictionary<string, Transform> _items;
    int _recognnized = 1;

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

    public void Set(string name, string header, string text, Sprite pic, bool isLiked) {
        if (!_items.ContainsKey(_recognnized.ToString()))
            return;
        //Debug.Log(name);
        if (isLiked) {
            foreach (var item in _items.Values) {
                if (item.FindChild("Title").GetComponent<Text>().text == header) {
                    return;
                }
            }

            _items[_recognnized.ToString()].FindChild("Title").GetComponent<Text>().text = header;
            _items[_recognnized.ToString()].FindChild("Text").GetComponent<Text>().text = text;
            _items[_recognnized.ToString()].FindChild("Picture").GetComponent<Image>().sprite = pic;
            _recognnized++;
        } else {
            int i = 0;
            bool flag = false;
            Transform prev = null;

            foreach (var item in _items.Values) {
                if (item.FindChild("Title").GetComponent<Text>().text == header) {
                    flag = true;
                    _recognnized--;
                }

                if (prev != null) {
                    prev.FindChild("Title").GetComponent<Text>().text =
                        item.FindChild("Title").GetComponent<Text>().text;
                    prev.FindChild("Text").GetComponent<Text>().text =
                        item.FindChild("Text").GetComponent<Text>().text;
                    prev.FindChild("Picture").GetComponent<Image>().sprite =
                        item.FindChild("Picture").GetComponent<Image>().sprite;
                    item.FindChild("Title").GetComponent<Text>().text = "";
                    item.FindChild("Text").GetComponent<Text>().text = "";
                    item.GetComponent<Image>().sprite = new Sprite();
                }

                if (flag)
                    prev = item;
            }
        }
    }

}
