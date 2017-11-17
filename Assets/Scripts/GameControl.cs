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
        StatManager sm = new StatManager();

        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/playerInfo.dat");
        PlayerData data = new PlayerData();

        data.life = player.GetComponent<PlayerController>().life;
        data.atk = sm.atk;
        data.hp = sm.hp;
        data.spe = sm.spe;
        data.end = sm.end;
        data.ran = sm.ran;
        data.rol = sm.rol;


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
            player.GetComponent<StatManager>().atk = data.atk;
            player.GetComponent<StatManager>().hp = data.hp;
            player.GetComponent<StatManager>().spe = data.spe;
            player.GetComponent<StatManager>().end = data.end;
            player.GetComponent<StatManager>().ran = data.ran;
            player.GetComponent<StatManager>().rol = data.rol ;
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
    public PlayerStats atk;
    public PlayerStats hp;
    public PlayerStats spe;
    public PlayerStats end;
    public PlayerStats ran;
    public PlayerStats rol;
}