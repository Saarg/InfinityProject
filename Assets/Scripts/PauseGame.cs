using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PostProcessing;
using UnityEngine.SceneManagement;

//using UnityStandardAssets.Characters.FirstPerson;

public class PauseGame : MonoBehaviour
{
    public Transform canvas;
    public Transform pauseMenu;
    public Transform soundsMenu;
    public Transform videoSettingsMenu;
    public Transform controlsMenu;
    public Transform Player;
    public GameControl gc;

	private float lastToggle;

	void Start() {
		Player = GameObject.FindGameObjectWithTag ("Player").transform;
		lastToggle = Time.realtimeSinceStartup;
	}

    // Update is called once per frame
    void Update()
    {
		if (MultiOSControls.GetValue("Pause") != 0 )
        {
            Pause();
        }
    }
    public void Pause()
    {
		if (Time.realtimeSinceStartup - lastToggle > 0.3f)
			lastToggle = Time.realtimeSinceStartup;
		else
			return;

        if (canvas.gameObject.activeInHierarchy == false)
        {
            if (pauseMenu.gameObject.activeInHierarchy == false)
            {
                pauseMenu.gameObject.SetActive(true);
                soundsMenu.gameObject.SetActive(false);
                videoSettingsMenu.gameObject.SetActive(false);
                controlsMenu.gameObject.SetActive(false);
            }
            canvas.gameObject.SetActive(true);
            Time.timeScale = 0;
            AudioListener.volume = 0;
            Player.gameObject.SetActive(false);
        }
        else
        {
            canvas.gameObject.SetActive(false);
            Time.timeScale = 1;
            AudioListener.volume = 1;
            Player.gameObject.SetActive(true);
        }
    }

    public void Sounds(bool Open)
    {
        if (Open)
        {
            soundsMenu.gameObject.SetActive(true);
            pauseMenu.gameObject.SetActive(false);
        }
        if (!Open)
        {
            soundsMenu.gameObject.SetActive(false);
            pauseMenu.gameObject.SetActive(true);
        }
    }


    public void VideoSettings(bool Open)
    {
        if (Open)
        {
            videoSettingsMenu.gameObject.SetActive(true);
            pauseMenu.gameObject.SetActive(false);
        }
        if (!Open)
        {
            videoSettingsMenu.gameObject.SetActive(false);
            pauseMenu.gameObject.SetActive(true);
        }
    }


    public void Controls(bool Open)
    {
        if (Open)
        {
            controlsMenu.gameObject.SetActive(true);
            pauseMenu.gameObject.SetActive(false);
        }
        if (!Open)
        {
            controlsMenu.gameObject.SetActive(false);
            pauseMenu.gameObject.SetActive(true);
        }
    }

	void OnApplicationQuit() {
		Debug.Log ("Reseting video params");
		PostProcessingProfile profile = Camera.main.GetComponent<PostProcessingBehaviour>().profile;

		profile.antialiasing.enabled = true;

		AntialiasingModel.Settings s = profile.antialiasing.settings;
		s.fxaaSettings.preset = AntialiasingModel.FxaaPreset.Default;

		profile.antialiasing.settings = s;

		profile.motionBlur.enabled = true;
		profile.ambientOcclusion.enabled = true;
	}

    public void SaveAndQuit()
    {
        gc.Save();
        Time.timeScale = 1;
        SceneManager.LoadScene("MainMenuErwann");
    }

}
