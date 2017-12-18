using System.Collections;
using UnityEngine;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine.SceneManagement;

/*
 * Class managing saving and loading stats in game
 */
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

    /*
     *  Function used to save the game. It save statistics into a .dat file when call.
     */
    public void Save()
    {
        StatManager sm = StatManager.Instance;

        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/playerInfo.dat");
        PlayerData data = new PlayerData();

        //data.life = player.GetComponent<PlayerController>().life;

        data.atkLevel = sm.Atk.Level;
        data.atkExperience = sm.Atk.Experience;
        data.atkCount = sm.Atk.Count;
        data.atkratio = sm.Atk.Ratio;
        data.hpLevel = sm.Hp.Level;
        data.hpExperience = sm.Hp.Experience;
        data.hpCount = sm.Hp.Count;
        data.hpRatio = sm.Hp.Ratio;
        data.speLevel = sm.Spe.Level;
        data.speExperience = sm.Spe.Experience;
        data.speCount = sm.Spe.Count;
        data.speRatio = sm.Spe.Ratio;
        data.endLevel = sm.End.Level;
        data.endExperience = sm.End.Experience;
        data.endCount = sm.End.Count;
        data.endRatio = sm.End.Ratio;
        data.ranLevel = sm.End.Level;
        data.ranExperience = sm.End.Experience;
        data.ranCount = sm.End.Count;
        data.ranRatio = sm.End.Ratio;
        data.rolLevel = sm.Rol.Level;
        data.rolExperience = sm.Rol.Experience;
        data.rolCount = sm.Rol.Count;
        data.rolRatio = sm.Rol.Ratio;

        bf.Serialize(file, data);
        file.Close();
    }

    /*
     *  Function used to load the game. It load statistics from the .dat created when saving.
     */
    public void Load()
    {
		try {
        if(File.Exists(Application.persistentDataPath + "/playerInfo.dat"))
	        {
	            StatManager sm = StatManager.Instance;

	            BinaryFormatter bf = new BinaryFormatter();
	            FileStream file = File.Open(Application.persistentDataPath + "/playerInfo.dat", FileMode.Open);
	            PlayerData data = (PlayerData)bf.Deserialize(file);

	            file.Close();

	            //player.GetComponent<PlayerController>().life = data.life;

	            sm.Atk.Level = data.atkLevel;
	            sm.Atk.Experience = data.atkExperience;
	            sm.Atk.Count = data.atkCount;
	            sm.Atk.Ratio = data.atkratio;
	            sm.Hp.Level = data.hpLevel;
	            sm.Hp.Experience = data.hpExperience;
	            sm.Hp.Count = data.hpCount;
	            sm.Hp.Ratio = data.hpRatio;
	            sm.Spe.Level = data.speLevel;
	            sm.Spe.Experience = data.speExperience;
	            sm.Spe.Count = data.speCount;
	            sm.Spe.Ratio = data.speRatio;
	            sm.End.Level = data.endLevel;
	            sm.End.Experience = data.endExperience;
	            sm.End.Count = data.endCount;
	            sm.End.Ratio = data.endRatio = sm.End.Ratio;
	            sm.End.Level = data.ranLevel = sm.End.Level;
	            sm.End.Experience = data.ranExperience = sm.End.Experience;
	            sm.End.Count = data.ranCount;
	            sm.End.Ratio = data.ranRatio;
	            sm.Rol.Level = data.rolLevel;
	            sm.Rol.Experience = data.rolExperience;
	            sm.Rol.Count = data.rolCount;
	            sm.Rol.Ratio = data.rolRatio;

	        }
		} catch {
			Debug.Log("failled to loag stats");
		}
    }
}

/* 
 * Local class used to save the attributs. It containe all variables for the statistics. It is this class that's save into the .dat file
 */
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

/*
 * This class is based and inspired by the tutorial "Persistence - Saving and Loading Data " on unity3d.com
 */