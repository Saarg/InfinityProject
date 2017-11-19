using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats{
    protected int level;
    public int Level
    {
        get
        {
            return this.level;
        }
        set
        {
            if (value < 0)
                this.level = 0;
            else if (value > 20)
                this.level = 20;
            else
                this.level = value;
        }
    }

    protected int experience;
    public int Experience
    {
        get
        {
            return this.experience;
        }
        set
        {
            if (value < 0)
                this.experience = 0;
            else
                this.experience = value;
        }
    }

    protected int count;
    public int Count
    {
        get
        {
            return this.count;
        }
        set
        {
            if (value < 0)
                this.count = 0;
            else
                this.count = value;
        }
    }

    protected int ratio = 2;
    public int Ratio
    {
        get
        {
            return this.ratio;
        }
        set
        {
            if (value < 0)
                this.ratio = 0;
            else
                this.ratio = value;
        }
    }
	public ArrayList bonus = new ArrayList ();
	public ArrayList malus = new ArrayList ();

    public PlayerStats()
    {
        level = 0;
        experience = 0;
        count = 0;
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
        if(experience >= experienceCap)
        {
            experience -= experienceCap;
            level++;
            if(level % 3 == 0)
            {
                foreach(PlayerStats ps in bonus)
                {
                    ps.level++;
                }
                foreach (PlayerStats ps in malus)
                {
                    ps.level--;
                }
            }
            return true;
        }
        return false;
    }

    public void Convert()
    {
        
        experience += count / Ratio;
        count = 0;
    }
}
