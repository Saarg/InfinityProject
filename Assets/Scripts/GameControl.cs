using System.Collections;
using UnityEngine;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine.SceneManagement;

public class GameControl : MonoBehaviour {

    public static GameControl control;
    public GameObject player;
    public float life;

    void Awake() {
        if(control == null)
        {
            //DontDestroyOnLoad(gameObject);
            control = this;
            player = GameObject.FindGameObjectWithTag("Player");
            life = player.GetComponent<PlayerController>().life;
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

        float pl = player.GetComponent<PlayerController>().life;
        data.life = pl;
        life = pl;

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

            life = data.life;
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