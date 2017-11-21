using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Versus : MonoBehaviour {

	public GameObject Player1;
	public GameObject Player2;
	public GameObject Player3;
	public GameObject Player4;

	public Canvas countDownCanvas;
	public Text countDownText;

	private float _startTime;

	// Use this for initialization
	void Start () {
		Time.timeScale = 0.0f;
		_startTime = Time.realtimeSinceStartup;

		Player1.SetActive(false);
		Player1.GetComponent<PlayerController>().playerCamera.gameObject.SetActive(true);

		Player2.SetActive(false);
		Player2.GetComponent<PlayerController>().playerCamera.gameObject.SetActive(true);

		Player1.GetComponent<PlayerController> ().playerCamera.rect = new Rect (0f, 0f, 0.5f, 1f);
		Player2.GetComponent<PlayerController> ().playerCamera.rect = new Rect (0.5f, 0f, 0.5f, 1f);

		Player3.SetActive(false);
		Player3.GetComponent<PlayerController>().playerCamera.gameObject.SetActive(false);

		Player4.SetActive(false);
		Player4.GetComponent<PlayerController>().playerCamera.gameObject.SetActive(false);

		StartCoroutine (CountDown ());
	}

	IEnumerator CountDown() {
		while (Time.realtimeSinceStartup - _startTime < 6) {
			if (!Player3.activeSelf && MultiOSControls.GetValue ("Join", PlayerNumber.Player3) != 0) {
				Player3.GetComponent<PlayerController> ().playerCamera.gameObject.SetActive (true);

				Player1.GetComponent<PlayerController> ().playerCamera.rect = new Rect (0f, 0.5f, 0.5f, 0.5f);
				Player2.GetComponent<PlayerController> ().playerCamera.rect = new Rect (0.5f, 0.5f, 0.5f, 0.5f);
				Player3.GetComponent<PlayerController> ().playerCamera.rect = new Rect (0.5f, 0f, 0.5f, 0.5f);
				Player4.GetComponent<PlayerController> ().playerCamera.rect = new Rect (0f, 0f, 0.5f, 0.5f);
			}

			if (!Player4.activeSelf && MultiOSControls.GetValue ("Join", PlayerNumber.Player4) != 0) {
				Player4.GetComponent<PlayerController> ().playerCamera.gameObject.SetActive (true);

				Player1.GetComponent<PlayerController> ().playerCamera.rect = new Rect (0f, 0.5f, 0.5f, 0.5f);
				Player2.GetComponent<PlayerController> ().playerCamera.rect = new Rect (0.5f, 0.5f, 0.5f, 0.5f);
				Player3.GetComponent<PlayerController> ().playerCamera.rect = new Rect (0.5f, 0f, 0.5f, 0.5f);
				Player4.GetComponent<PlayerController> ().playerCamera.rect = new Rect (0f, 0f, 0.5f, 0.5f);
			}

			countDownText.text = (5 - (int)(Time.realtimeSinceStartup - _startTime)).ToString();

			yield return null;
		}

		StartCoroutine (MainUpdate ());
	}

	IEnumerator MainUpdate() {
		countDownCanvas.gameObject.SetActive (false);
		Time.timeScale = 1.0f;

		if (!Player1.GetComponent<PlayerController> ().playerCamera.gameObject.activeSelf) {
			Destroy (Player1.GetComponent<PlayerController> ().playerCamera.gameObject);
			Destroy (Player1);
		} else {
			Player1.SetActive (true);
		}

		if (!Player2.GetComponent<PlayerController> ().playerCamera.gameObject.activeSelf) {
			Destroy (Player2.GetComponent<PlayerController> ().playerCamera.gameObject);
			Destroy (Player2);
		} else {
			Player2.SetActive (true);
		}

		if (!Player3.GetComponent<PlayerController> ().playerCamera.gameObject.activeSelf) {
			Destroy (Player3.GetComponent<PlayerController> ().playerCamera.gameObject);
			Destroy (Player3);
		} else {
			Player3.SetActive (true);
		}

		if (!Player4.GetComponent<PlayerController> ().playerCamera.gameObject.activeSelf) {
			Destroy (Player4.GetComponent<PlayerController> ().playerCamera.gameObject);
			Destroy (Player4);
		} else {
			Player4.SetActive (true);
		}

		while (true) {


			yield return null;
		}
	}
}
