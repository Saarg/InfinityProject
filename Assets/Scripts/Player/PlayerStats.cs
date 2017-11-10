using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats{
    public int level;
    public int experience;
    public int count;
   
    public PlayerStats()
    {
        level = 0;
        experience = 0;
        count = 0;
    }

    public PlayerStats(int level)
    {
        this.level = level;
        this.experience = 0;
        this.count = 0;
    }

    public PlayerStats(int level, int experience)
    {
        this.level = level;
        this.experience = experience;
        this.count = 0;
    }

    public PlayerStats(int level, int experience, int count)
    {
        this.level = level;
        this.experience = experience;
        this.count = count;
    }

    

    public bool LevelUp(int experienceCap)
    {
        if(experience >= experienceCap)
        {
            experience -= experienceCap;
            level++;
            return true;
        }
        return false;
    }
}
