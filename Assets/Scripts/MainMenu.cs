﻿using UnityEngine;
using System.Collections;
using Vuforia;

public class MainMenu : MonoBehaviour {

    Process _movingProcess;
    float _movingTime = 0.5f;

    void Start () {

    }

    void Update () {
        if (_movingProcess != null) {
            _movingProcess.Update();

            if (_movingProcess.IsFinished)
                _movingProcess = null;
        }
    }

    bool InitMovingProcess () {
        if (_movingProcess != null)
            return false;

        _movingProcess = new Process(_movingTime);

        return true;
    }

    public void OnLogoPageButtonClick(RectTransform rt) {
        if (!InitMovingProcess() || rt == null)
            return;

        float startX = rt.position.x;
        _movingProcess.Callback = () => {
            rt.position = new Vector3(startX + Screen.width * _movingProcess.Progress,
                rt.position.y, rt.position.z);
        }; 
    }

    public void OnTutorialPageButtonClick () {
        if (!InitMovingProcess())
            return;
    }

    public void Show(RectTransform rt) {
        if (!InitMovingProcess())
            return;

        float startX = rt.position.x;
        _movingProcess.Callback = () => {
            rt.position = new Vector3(startX + rt.sizeDelta.x * _movingProcess.Progress,
                rt.position.y, rt.position.z);
        };
    }

    public void Hide(RectTransform rt) {
        if (!InitMovingProcess())
            return;

        float startX = rt.position.x;
        _movingProcess.Callback = () => {
            rt.position = new Vector3(startX - rt.sizeDelta.x * _movingProcess.Progress,
                rt.position.y, rt.position.z);
        };
    }

    public void Reset () {
        Application.LoadLevel(0);
    }

}
