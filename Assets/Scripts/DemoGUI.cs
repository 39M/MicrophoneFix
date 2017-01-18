using UnityEngine;
using System.Collections;

public class DemoGUI : MonoBehaviour {


	private AudioClip newClip;
	private bool _started;

	void Start() {
		_started = false;
	}

	void Break() {
		if (Microphone.IsRecording(null)) {
			End();
		}

		_started = true;
		newClip = Microphone.Start(null, false, 100, 44100);
	}

	void End() {
		_started = false;
		Microphone.End(null);
	}



	void OnGUI() {
		if (GUI.Button(new Rect(100, 100, 400, 150), !_started ? "Break" : "End")) {

			if (!_started) {
				Break();
			} else {
				End();
			}

		}


		if (GUI.Button(new Rect(100, 400, 400, 150), "Fix")) {
			iPhoneSpeaker.ForceToSpeaker();
		}

	}

}
