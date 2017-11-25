using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats{
    protected int _level;
    public int Level
    {
        get
        {
            return this._level;
        }
        set
        {
            if (value <= 0)
                this._level = 0;
            else if (value >= 19)
                this._level = 19;
            else
                this._level = value;
        }
    }

    protected int _experience;
    public int Experience
    {
        get
        {
            return this._experience;
        }
        set
        {
            if (value < 0)
                this._experience = 0;
            else
                this._experience = value;
        }
    }

    protected int _count;
    public int Count
    {
        get
        {
            return this._count;
        }
        set
        {
            if (value < 0)
                this._count = 0;
            else
                this._count = value;
        }
    }

    protected int _ratio = 2;
    public int Ratio
    {
        get
        {
            return this._ratio;
        }
        set
        {
            if (value < 1)
                this._ratio = 1;
            else
                this._ratio = value;
        }
    }
	public ArrayList bonus = new ArrayList ();
	public ArrayList malus = new ArrayList ();

    public PlayerStats()
    {
        _level = 0;
        _experience = 0;
        _count = 200;
        bonus = new ArrayList();
        malus = new ArrayList();
    }

    public void GiveBonusTo(PlayerStats ps)
    {
        bonus.Add(ps);
    }

    public void GiveMalusTo(PlayerStats ps)
    {
        malus.Add(ps);
    }

    public bool LevelUp(int experienceCap)
    {
        if(_experience >= experienceCap)
        {
            _experience -= experienceCap;
            Level++;
            if(Level % 3 == 0)
            {
                foreach(PlayerStats ps in bonus)
                {
                    ps.Level++;
                }
                foreach (PlayerStats ps in malus)
                {
                    ps.Level--;
                }
            }
            return true;
        }
        return false;
    }

    public void Convert()
    {
        Experience += Count / Ratio;
        Count = 0;
    }
}
