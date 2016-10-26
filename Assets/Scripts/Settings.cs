using UnityEngine;
using System.Collections;

public class Settings : MonoBehaviour {

    public TextAsset csvFile;

    public static Settings Instance { get; private set; }

    void Start()
    {
        Instance = this;

        if (csvFile == null)
            throw new System.NullReferenceException();
    }
}
