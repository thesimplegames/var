using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using Vuforia;

public class TrackingDetector : MonoBehaviour,
                                            ITrackableEventHandler
{
    //public Text title;
    //public Text text;


    private TrackableBehaviour mTrackableBehaviour;

    bool showGUI = true;

    string targetName;

    void Start()
    {
        mTrackableBehaviour = GetComponent<TrackableBehaviour>();
        if (mTrackableBehaviour)
        {
            mTrackableBehaviour.RegisterTrackableEventHandler(this);
        }

        targetName = GetComponent<ImageTargetBehaviour>().TrackableName;

        Debug.Log(targetName);
        
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

            Debug.Log(targetName);

            ContentManager.Instance.Set(targetName);
            MainMenu.Instance.Show(ContentManager.Instance.gameObject.GetComponent<RectTransform>());
            
        } else
        {
            showGUI = false;
        }
    }

    void OnGUI ()
    {
        if (showGUI == false)
            return;

        if (GUI.Button(new Rect(0, 0, 200, 100), targetName)) {
            showGUI = false;
        }
    }
}