using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityStandardAssets.Characters.FirstPerson;

public class PauseGame : MonoBehaviour
{
    public Transform canvas;
    public Transform pauseMenu;
    public Transform soundsMenu;
    public Transform videoSettingsMenu;
    public Transform controlsMenu;
    public Transform Player;
    SaveGame saveGame;




    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("p"))
        {
            Pause();
        }
    }
    public void Pause()
    {
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
            saveGame = gameObject.GetComponent<SaveGame>();
            saveGame.SaveGameSettings(false);
        }
        else
        {
            canvas.gameObject.SetActive(false);
            Time.timeScale = 1;
            AudioListener.volume = 1;
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
}
