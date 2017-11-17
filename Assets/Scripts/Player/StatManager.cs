using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatManager {

    public int[] experienceTable = new int[20] {3,6,8,11,14,17,21,24,29,33,38,43,49,56,63,70,78,87,97,107};
    public PlayerStats atk;
    public PlayerStats hp;
    public PlayerStats spe;
    public PlayerStats end;
    public PlayerStats ran;
    public PlayerStats rol;

    protected PlayerStats[] stats = new PlayerStats[6];
    protected static StatManager _instance = null;

    public static StatManager instance
    {
        get
        {
            if (_instance == null)
                _instance = new StatManager();
            return _instance;
        }
    }

    public StatManager()
    {
        atk = new PlayerStats();
        hp = new PlayerStats();
        spe = new PlayerStats();
        end = new PlayerStats();
        ran = new PlayerStats();
        rol = new PlayerStats();

        spe.ratio = 1000;
        ran.ratio = 8;

        stats[0] = atk;
        stats[1] = hp;
        stats[2] = spe;
        stats[3] = end;
        stats[4] = ran;
        stats[5] = rol;
    }

    public void LevelUp()
    {
        foreach (PlayerStats ps in stats)
        {
            ps.Convert();
            while (ps.LevelUp(experienceTable[ps.level]))
            {
                // Debug.Log("Level :" + ps.level + " xp restant :" + ps.experience);
            }
        }
    }
}
