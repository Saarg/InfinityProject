using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatManager {

    protected static StatManager _instance = null;

    protected int[] experienceTable = new int[20] { 3, 6, 8, 11, 14, 17, 21, 24, 29, 33, 38, 43, 49, 56, 63, 70, 78, 87, 97, 107 };
    public int[] ExperienceTable { get { return experienceTable; } }

    protected PlayerStats[] stats = new PlayerStats[6];
    public PlayerStats[] Stats { get { return stats; } }

    protected bool[] up = new bool[6] { false, false, false, false, false, false };
    public bool[] Up { get { return up; } }

    public PlayerStats Atk { get; }
    public PlayerStats Hp { get; }
    public PlayerStats Spe { get; }
    public PlayerStats End { get; }
    public PlayerStats Ran { get; }
    public PlayerStats Rol { get; }
    
    public static StatManager Instance
    {
        get
        {
            if (_instance == null)
                _instance = new StatManager();
            return _instance;
        }
    }

    protected StatManager()
    {
        Atk = new PlayerStats();
        Hp = new PlayerStats();
        Spe = new PlayerStats();
        End = new PlayerStats();
        Ran = new PlayerStats();
        Rol = new PlayerStats();

        Atk.GiveBonusTo(End);
        Hp.GiveBonusTo(Rol);
        Spe.GiveBonusTo(Ran);

        Atk.GiveMalusTo(Rol);
        Atk.GiveMalusTo(Hp);
        Hp.GiveMalusTo(Ran);
        Hp.GiveMalusTo(Spe);
        Spe.GiveMalusTo(Atk);
        Spe.GiveMalusTo(End);
        End.GiveMalusTo(Hp);
        Ran.GiveMalusTo(Atk);
        Rol.GiveMalusTo(Spe);

        Spe.Ratio = 10;
        Ran.Ratio = 8;
        End.Ratio = 10;

        stats[0] = End;
        stats[1] = Hp;
        stats[2] = Ran;
        stats[3] = Spe;
        stats[4] = Rol;
        stats[5] = Atk;
    }
    
    public void LevelUp()
    {
        for (int i = 0; i < stats.Length; i++)
        {
            PlayerStats ps = stats[i];
            ps.Convert();
            
            while (ps.LevelUp(experienceTable[ps.Level]))
            {
                up[i] = true;
            } 
        }
    }

    public void LevelUp(PlayerStats ps)
    {
        ps.Convert();

        while (ps.LevelUp(experienceTable[ps.Level]))
        {

        }
    }

    public bool LevelUp(PlayerStats ps, bool OneLevelOnly)
    {
        bool leveledUp = false;
        ps.Convert();

        if (OneLevelOnly)
            return ps.LevelUp(experienceTable[ps.Level]);
        else
        { 
            while (ps.LevelUp(experienceTable[ps.Level]))
            {
                leveledUp = true;
            }
            return leveledUp;
        }
    }

    public void Displayed()
    {
        for (int i = 0; i < up.Length; i++)
            up[i] = false;
    }
}
