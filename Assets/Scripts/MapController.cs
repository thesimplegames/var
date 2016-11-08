using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class MapController : MonoBehaviour {

    public static MapController Instance { get; set; }
    Dictionary<string, GameObject> _locations;
    GameObject _current;
    Dictionary<string, List<string>> _items;

	void Start () {
        Instance = this;
        _locations = new Dictionary<string, GameObject>();
        _items = new Dictionary<string, List<string>>();

        for (int i = 0; i < transform.childCount; i++) {
            var go = transform.GetChild(i).gameObject;
            _locations.Add(go.name, go.gameObject);
            go.SetActive(false);
        }
	}



    public void SetPosition(string pos, string name, bool set = true) {
        //if (_current != null)
        //    _current.SetActive(false);

        if (!_items.ContainsKey(pos))
            _items.Add(pos, new List<string>());

        if (set)
            if (!_items[pos].Contains(name))
                _items[pos].Add(name);

        if (!set)
            if (_items[pos].Contains(name))
                _items[pos].Remove(name);

        if (_locations.TryGetValue(pos, out _current))
            _current.SetActive(_items[pos].Count > 0);
    }
	
}
