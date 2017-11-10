using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class SaveGame : MonoBehaviour {
    public Transform Player;

    public void SaveGameSettings(bool Quit)
    {
        if (Quit)
        {
            Time.timeScale = 1;
            SceneManager.LoadScene("MainMenu");
        }
    }
}
