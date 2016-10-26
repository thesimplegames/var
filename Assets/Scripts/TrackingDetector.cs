using UnityEngine;
using System.Collections;
using Vuforia;

public class TrackingDetector : MonoBehaviour,
                                            ITrackableEventHandler
{
    private TrackableBehaviour mTrackableBehaviour;

    bool showGUI = false;

    string targetName;

    void Start()
    {
        mTrackableBehaviour = GetComponent<TrackableBehaviour>();
        if (mTrackableBehaviour)
        {
            mTrackableBehaviour.RegisterTrackableEventHandler(this);
        }

        targetName = GetComponent<ImageTargetBehaviour>().TrackableName;
        
    }

    public void OnTrackableStateChanged(TrackableBehaviour.Status previousStatus, TrackableBehaviour.Status newStatus)
    {
        if (newStatus == TrackableBehaviour.Status.DETECTED ||
            newStatus == TrackableBehaviour.Status.TRACKED ||
            newStatus == TrackableBehaviour.Status.EXTENDED_TRACKED)
        {
            showGUI = true;
            //throw new System.Exception("ASD");

            if (!TargetLogger.Instance.targets.Contains(targetName))
            {
                TargetLogger.Instance.targets.Add(targetName);
                TargetLogger.Instance.output += targetName + "\n";
            }
        }
        else
        {
            showGUI = false;
        }
    }

    void OnGUI ()
    {
        if (showGUI == false)
            return;

        GUI.Button(new Rect(0, 0, 200, 100), targetName);
    }
}