using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats{
	// Stat level
    protected int _level;
    public int Level { get { return _level; } set { _level = Mathf.Clamp (value, 0, 19); } }

	// stat exp
    protected int _experience;
    public int Experience { get { return _experience; } set { _experience = value > 0 ? value : 0; }}

	// stat count =>> exp = count / ratio
    protected int _count;
	public int Count { get { return _count; } set { _count = value > 0 ? value : 0; }}

	// stat count ratio
    protected int _ratio = 2;
	public int Ratio { get { return _ratio; } set { _ratio = value > 1 ? value : 1; }}

	// links to upgrade or downgrades stats every 3 levels
	public ArrayList bonus = new ArrayList ();
	public ArrayList malus = new ArrayList ();

    public PlayerStats()
    {
        _level = 0;
        _experience = 0;
        _count = 0;
        bonus = new ArrayList();
        malus = new ArrayList();
    }

	// every 3 level ps.level will increment
    public void GiveBonusTo(PlayerStats ps)
    {
        bonus.Add(ps);
    }

	// every 3 level ps.level will decrement
    public void GiveMalusTo(PlayerStats ps)
    {
        malus.Add(ps);
    }

	// Level up the stats and bonus/malus
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

	// convert count To exp
    public void Convert()
    {
        Experience += Count / Ratio;
        Count = 0;
    }
}
