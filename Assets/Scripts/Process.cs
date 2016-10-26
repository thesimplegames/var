using UnityEngine;
using System.Collections;

public class Process {
	
	private bool looped;
	private float duration;	
	private float elapsed;
	private bool paused;
    private System.Action _callback;

	public bool Paused {
		get {
			return this.paused;
		}
		set {			
			paused = value;
		}
	}	
	public float Progress {
		get { return elapsed/duration; }
		set { 
			elapsed = value*duration;
			if (float.IsNaN(elapsed))
				Debug.Log("log");
		}
	}
	
	public float LoopProgress {
		get { return Progress - LoopInd; }
	}
	
	public float RemainingTime {
		get { return duration-elapsed; }
	}
		
	public Process (float duration, bool looped = false, System.Action callback = null) {
		this.duration = duration;
		this.elapsed = 0;
		this.looped = looped;
        _callback = callback;
	}
	
	public bool IsFinished {
		get {
			return Progress>=1&&!looped;
		}
	}
	
	public int LoopInd {
		get {
			return (int)Progress;
		}
	}
	
	public void Update() {
        if (paused)
            return;

		elapsed += Time.deltaTime;

        if (_callback != null)
            _callback.Invoke();
	}
	
	public void UpdateWithDT(float dt) {
		//if (!paused)
			elapsed += dt;
	}
	
	public float Duration { get { return duration; } }
	public void Reset() {
		elapsed = 0;
	}
}
