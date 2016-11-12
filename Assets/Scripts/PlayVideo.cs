using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

[RequireComponent (typeof(AudioSource))]
public class PlayVideo : MonoBehaviour {

    public static PlayVideo Instance;

    //public MovieTexture movie21;
    //public MovieTexture movie24;
    public List<MovieTexture> movies;
    private AudioSource _audio;
    private RawImage _image;

	// Use this for initialization
	void Start () {
        Instance = this;
        _audio = GetComponent<AudioSource>();
        _image = GetComponent<RawImage>();
        PlayStop();
	}

    public void Play (string name) {
        if (name == "21" && movies[0] != null) {
            Play(movies[0]);
        }

        if (name == "24" && movies[1] != null) {
            Play(movies[1]);
        }
    }

    void Play(MovieTexture movie) {
        PlayStop(true);
        _image.texture = movie as MovieTexture;
        _audio.clip = movie.audioClip;
        movie.Play();
        _audio.Play();
    }

    public void PlayStop (bool state = false) {
        _audio.Stop();
        if (_image.texture != null)
            (_image.texture as MovieTexture).Stop();
        _image.enabled = state;
        _audio.enabled = state;
    }

}
