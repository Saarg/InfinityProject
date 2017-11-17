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

        Spe.Ratio = 1000;
        Ran.Ratio = 8;
        
        stats[0] = Atk;
        stats[1] = Hp;
        stats[2] = Spe;
        stats[3] = End;
        stats[4] = Ran;
        stats[5] = Rol;
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
