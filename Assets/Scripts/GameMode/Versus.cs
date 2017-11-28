using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Versus : GameMode {
	
	public Canvas countDownCanvas;
	public Text countDownText;

	private float _startTime;

	public UnityEvent EndCountDown;

	// Use this for initialization
	protected override void Start () {
		allowCoop = true;

		base.Start ();

		Time.timeScale = 0.0f;
		_startTime = Time.realtimeSinceStartup;

		_player2.transform.parent.gameObject.SetActive (true);

		_player1.GetComponent<PlayerController> ().playerCamera.rect = new Rect (0f, 0f, 0.5f, 1f);
		_player2.GetComponent<PlayerController> ().playerCamera.rect = new Rect (0.5f, 0f, 0.5f, 1f);

		StartCoroutine (CountDown ());
	}
		
	IEnumerator CountDown() {
		yield return new WaitForSeconds (0.1f);

		while (Time.realtimeSinceStartup - _startTime < 6) {
			Time.timeScale = 0.0f;

			if (MultiOSControls.GetValue ("Join", PlayerNumber.Player4) != 0 || MultiOSControls.GetValue ("Join", PlayerNumber.Player3) != 0) {
				Vector3 cameraPos = new Vector3 (0, 5f, 0);

				_player1.GetComponent<PlayerController> ().playerCamera.transform.position = cameraPos;
				_player1.GetComponent<PlayerController> ().playerCamera.rect = new Rect (0f, 0.5f, 0.5f, 0.5f);

				_player2.GetComponent<PlayerController> ().playerCamera.transform.position = cameraPos;
				_player2.GetComponent<PlayerController> ().playerCamera.rect = new Rect (0.5f, 0.5f, 0.5f, 0.5f);

				_player3.GetComponent<PlayerController> ().playerCamera.transform.position = cameraPos;
				_player3.GetComponent<PlayerController> ().playerCamera.rect = new Rect (0.5f, 0f, 0.5f, 0.5f);

				_player4.GetComponent<PlayerController> ().playerCamera.transform.position = cameraPos;
				_player4.GetComponent<PlayerController> ().playerCamera.rect = new Rect (0f, 0f, 0.5f, 0.5f);
			}

			countDownText.text = (5 - (int)(Time.realtimeSinceStartup - _startTime)).ToString();

			yield return null;
		}

		countDownCanvas.gameObject.SetActive (false);

		EndCountDown.Invoke ();

		Time.timeScale = 1.0f;
		allowCoop = false;
	}
}
