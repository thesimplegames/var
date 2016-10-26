using UnityEngine;
using System.Collections;

public class MainMenu : MonoBehaviour {

    Process _movingProcess;

    const float _horizontalMovingTime = 0.5f;
    const int _distance = -1470;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (_movingProcess == null)
            return;

        if (_movingProcess.IsFinished)
            return;

        _movingProcess.Update();

        if (_movingProcess.IsFinished)
            _movingProcess = null;
	}

	public void OnClick () {
		var csv = CSVReader.SplitCsvGrid();
		Debug.Log (csv.Length);
	}

    public void MoveGOHorizontaly(Transform tr)
    {
        if (_movingProcess == null)
        {
            float startX = tr.position.x;
            _movingProcess = new Process(_horizontalMovingTime, false, () => {
                tr.position = new Vector3(startX - (int)(_movingProcess.Progress * _distance), tr.position.y, tr.position.z);
            });
        }
    }
}
