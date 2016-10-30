using UnityEngine;
using System.Collections;
using Vuforia;

public class MainMenu : MonoBehaviour {

    Process _tutorialProcess;
    Process _toolbarProcess;


    const float _horizontalMovingTime = 0.5f;
    const int _distance = -1470;
    const int _toolbarDistance = -600;
    float mult = 1;

    public GameObject showButton;
    

	// Use this for initialization
	void Start () {
	
	}
	
    void UpdateTutorial()
    {
        if (_tutorialProcess == null)
            return;

        if (_tutorialProcess.IsFinished)
            return;

        _tutorialProcess.Update();

        if (_tutorialProcess.IsFinished)
        {
            _tutorialProcess = null;
            //Debug.LogError(123);

            if (mult < 1.1f)
                mult = 1.2f;
        }
    }

    void UpdateToolbar()
    {
        if (_toolbarProcess == null)
            return;

        if (_toolbarProcess.IsFinished)
            return;

        _toolbarProcess.Update();

        if (_toolbarProcess.IsFinished)
        {
            _toolbarProcess = null;
            openedToolbar *= -1;
            showButton.SetActiveRecursively(openedToolbar < 0);
        }
    }

	// Update is called once per frame
	void Update () {
        UpdateTutorial();
        UpdateToolbar();
	}

	public void OnClick () {
		var csv = CSVReader.SplitCsvGrid();
		Debug.Log (csv.Length);
	}

    public void MoveGOHorizontaly(Transform tr)
    {
        if (_tutorialProcess == null)
        {
            float startX = tr.position.x;
            _tutorialProcess = new Process(_horizontalMovingTime, false, () => {
                tr.position = new Vector3(startX - (int)(_tutorialProcess.Progress * _distance * mult), tr.position.y, tr.position.z);
            });
        }
    }

    public void OnTutorialButton()
    {
        Application.LoadLevel("main");
    }


    int openedToolbar = 1;
    //float toolbarPosition;

    public void MoveToolbar (Transform tr)
    {
        //toolbarPosition = tr.position.x;

        if (showButton.active)
            showButton.SetActiveRecursively(false);

        if (_toolbarProcess == null)
        {
            float startX = tr.position.x;
            _toolbarProcess = new Process(0.3f, false, () => {
                tr.position = new Vector3(startX - (int)( _toolbarProcess.Progress * _toolbarDistance * openedToolbar), tr.position.y, tr.position.z);
            });
        }
    }

    void ShowMap () {

    }
}
