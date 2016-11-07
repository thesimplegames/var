using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class MapController : MonoBehaviour {

    public static MapController Instance { get; set; }
    Dictionary<string, GameObject> _locations;
    GameObject _current;

	void Start () {
        Instance = this;
        _locations = new Dictionary<string, GameObject>();

        for (int i = 0; i < transform.childCount; i++) {
            var go = transform.GetChild(i).gameObject;
            _locations.Add(go.name, go.gameObject);
            go.SetActive(false);
        }
	}

    public void SetPosition(string pos) {
        //if (_current != null)
        //    _current.SetActive(false);

        if (_locations.TryGetValue(pos, out _current))
            _current.SetActive(true);
    }
	
}
