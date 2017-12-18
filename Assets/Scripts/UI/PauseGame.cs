using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PostProcessing;
using UnityEngine.SceneManagement;

/*
 * Class managing the pause Menu and its components in game 
 */
public class PauseGame : MonoBehaviour
{
    public Transform canvas;
    public Transform pauseMenu;
    public Transform soundsMenu;
    public Transform videoSettingsMenu;
    public Transform controlsMenu;
	public GameObject[] players;
    public GameControl gc;

	private float lastToggle;

	void Start() {
		players = GameObject.FindGameObjectsWithTag ("Player");
		lastToggle = Time.realtimeSinceStartup;
	}

    // Update is called once per frame
    void Update()
    {
		if (!soundsMenu.gameObject.activeSelf &&
			!videoSettingsMenu.gameObject.activeSelf &&
			!controlsMenu.gameObject.activeSelf &&
			MultiOSControls.GetValue("Pause", PlayerNumber.All) != 0)
        {
            Pause();
        }
    }

    /*
     * Function Allowing the player to pause the game. When the pause button is pressed, the game time and audio stop.
     * When the pause button is pressed a second time, the game time and audio restart and the player leaver the pause menu
     */
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

			foreach (GameObject player in players) {
				if (player != null) {
					player.SetActive (false);
				}
			}
        }
        else
        {
            canvas.gameObject.SetActive(false);
            Time.timeScale = 1;
            AudioListener.volume = 1;
			foreach (GameObject player in players) {
				if (player != null) {
					player.SetActive (player.GetComponent<PlayerController> ().playerCamera.gameObject.activeSelf);
				}
			}
        }
    }

    /*
     * All Function under this are called by the canvas inside the game, when the button corresponding button is pressed.
     * They allow the player nagivate trought all menus the game has.
     */

    /*
     * This function handle the Sound menu, and is called when the Sound button from the pause menu is pressed
     */
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

    /*
     * This function handle the Video menu, and is called when the Video button from the pause menu is pressed
     */
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

    /*
     * This function handle the Control menu, and is called when the Control button from the pause menu is pressed
     */
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

    /*
     *  This function handle the Save & Quit of the game. It call for a save trought the GameControl script, restart the sound and time, and load the main menu
     */

    public void SaveAndQuit()
    {
		if (gc != null) {
			gc.Save ();
		}
        Time.timeScale = 1;
        AudioListener.volume = 1;
        SceneManager.LoadScene("MainMenu");
    }
}

/*
 * This class is based and inspired by the series "Unity 5 making a more advanced pause menu" by MrBuFF 1 on youtube 
 * 
 */
