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

        data.atkLevel = sm.atk.level;
        data.atkExperience = sm.atk.experience;
        data.atkCount = sm.atk.count;
        data.atkratio = sm.atk.ratio;
        data.hpLevel = sm.hp.level;
        data.hpExperience = sm.hp.experience;
        data. hpCount = sm.hp.count;
        data.hpRatio = sm.hp.ratio;
        data. speLevel = sm.spe.level;
        data.speExperience = sm.spe.experience;
        data.speCount = sm.spe.count;
        data.speRatio = sm.spe.ratio;
        data.endLevel = sm.end.level;
        data.endExperience = sm.end.experience;
        data.endCount = sm.end.count;
        data.endRatio = sm.end.ratio;
        data.ranLevel = sm.end.level;
        data.ranExperience = sm.end.experience;
        data.ranCount = sm.end.count;
        data.ranRatio = sm.end.ratio;
        data.rolLevel = sm.rol.level;
        data.rolExperience = sm.rol.experience;
        data.rolCount = sm.rol.count;
        data.rolRatio = sm.rol.ratio;

        bf.Serialize(file, data);
        file.Close();
    }

    public void Load()
    {
        if(File.Exists(Application.persistentDataPath + "/playerInfo.dat"))
        {
            StatManager sm = new StatManager();

            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/playerInfo.dat", FileMode.Open);
            PlayerData data = (PlayerData)bf.Deserialize(file);

            file.Close();

            player.GetComponent<PlayerController>().life = data.life;

            sm.atk.level = data.atkLevel;
            sm.atk.experience = data.atkExperience;
            sm.atk.count = data.atkCount;
            sm.atk.ratio = data.atkratio;
            sm.hp.level = data.hpLevel;
            sm.hp.experience = data.hpExperience;
            sm.hp.count = data.hpCount;
            sm.hp.ratio = data.hpRatio;
            sm.spe.level = data.speLevel;
            sm.spe.experience = data.speExperience;
            sm.spe.count = data.speCount;
            sm.spe.ratio = data.speRatio;
            sm.end.level = data.endLevel;
            sm.end.experience = data.endExperience;
            sm.end.count = data.endCount;
            sm.end.ratio = data.endRatio = sm.end.ratio;
            sm.end.level = data.ranLevel = sm.end.level;
            sm.end.experience = data.ranExperience = sm.end.experience;
            sm.end.count = data.ranCount;
            sm.end.ratio = data.ranRatio;
            sm.rol.level = data.rolLevel;
            sm.rol.experience = data.rolExperience;
            sm.rol.count = data.rolCount;
            sm.rol.ratio = data.rolRatio;

        }
    }
}

[Serializable]
class PlayerData
{
    public float life;
    public int atkLevel;
    public int atkExperience;
    public int atkCount;
    public int atkratio;
    public int hpLevel;
    public int hpExperience;
    public int hpCount;
    public int hpRatio;
    public int speLevel;
    public int speExperience;
    public int speCount;
    public int speRatio;
    public int endLevel;
    public int endExperience;
    public int endCount;
    public int endRatio;
    public int ranLevel;
    public int ranExperience;
    public int ranCount;
    public int ranRatio;
    public int rolLevel;
    public int rolExperience;
    public int rolCount;
    public int rolRatio;
}


