using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatManager {

    protected int[] experienceTable = new int[20] {3,6,8,11,14,17,21,24,29,33,38,43,49,56,63,70,78,87,97,107};
    public PlayerStats Atk { get; }
    public PlayerStats Hp { get; }
    public PlayerStats Spe { get; }
    public PlayerStats End { get; }
    public PlayerStats Ran { get; }
    public PlayerStats Rol { get; }

    protected PlayerStats[] stats = new PlayerStats[6];
    public PlayerStats[] Stats { get { return stats; } }
    protected static StatManager _instance = null;

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

        Spe.Ratio = 30;
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

        foreach (PlayerStats ps in stats)
        {
            ps.Convert();
            
            while (ps.LevelUp(experienceTable[ps.Level]))
            {

            }
        }
    }
}
