using UnityEngine;
using System.Collections;

public class SelfTurnOf : MonoBehaviour {

    Process _turnOff;

	// Use this for initialization
	void Start () {
        _turnOff = new Process(1.5f);
	}
	
	// Update is called once per frame
	void Update () {
        if (_turnOff.IsFinished)
            return;

        _turnOff.Update();

        //Debug.Log(_turnOff.Progress);

        if (_turnOff.IsFinished)
            MainMenu.Instance.Hide(gameObject.GetComponent<RectTransform>());
	}
}
