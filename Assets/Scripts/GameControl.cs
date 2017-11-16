using System.Collections;
using UnityEngine;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine.SceneManagement;

public class GameControl : MonoBehaviour {

    public static GameControl control;
    public GameObject player;

    void Awake() {
        if(control == null)
        {
            //DontDestroyOnLoad(gameObject);
            control = this;
            player = GameObject.FindGameObjectWithTag("Player");
        }
        else if(control != this)
        {
            Destroy(gameObject);
        }
        Load();
	}

    public void Save()
    {    
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/playerInfo.dat");
        PlayerData data = new PlayerData();

        data.life = player.GetComponent<PlayerController>().life;
        /*data.level = player.GetComponent<PlayerStats>().level;
        data.experience = player.GetComponent<PlayerStats>().experience;
        data.count = player.GetComponent<PlayerStats>().count;*/

        bf.Serialize(file, data);
        file.Close();
    }

    public void Load()
    {
        if(File.Exists(Application.persistentDataPath + "/playerInfo.dat"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/playerInfo.dat", FileMode.Open);
            PlayerData data = (PlayerData)bf.Deserialize(file);

            file.Close();

            player.GetComponent<PlayerController>().life = data.life;
        }
    }

    void Update()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Load();
    }
}

[Serializable]
class PlayerData
{
       public float life;
}