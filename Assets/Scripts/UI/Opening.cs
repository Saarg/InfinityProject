using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class Opening : MonoBehaviour {

	[SerializeField] VideoPlayer openingVid;
	[SerializeField] string nextSceneName;

	void Start () {
		openingVid.Play ();
	}
	
	void LateUpdate () {
		//if any is pressed, video is skipped
		if(Input.anyKey && Time.time > 2f){
			LoadNextScene ();
		}

		//else wait for the end of the clip to load next scene
		if ((!openingVid.isPlaying) && Time.time > 2f) {
			LoadNextScene ();
		}
	}

	private void LoadNextScene(){
		SceneManager.LoadScene (nextSceneName);
	}
}
