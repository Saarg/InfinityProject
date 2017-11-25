using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelDisplay : MonoBehaviour {
    
    public Text endLevel;
    public Text hpLevel;
    public Text ranLevel;
    [Space(5)]
    public Text atkLevel;
    public Text rollLevel;
    public Text speLevel;

    protected StatManager statManager;
    /*
    // Use this for initialization
    void Start () {
        statManager = StatManager.Instance;

        statManager.LevelUp();
        
        endLevel.text = ""+ statManager.End.Level;
        hpLevel.text = "" + statManager.Hp.Level;
        ranLevel.text = "" + statManager.Ran.Level;
        atkLevel.text = "" + statManager.Atk.Level;
        rollLevel.text = "" + statManager.Rol.Level;
        speLevel.text = "" + statManager.Spe.Level;

}

    // Update is called once per frame
    void Update()
    {
        endLevel.text = "" + statManager.End.Level;
        hpLevel.text = "" + statManager.Hp.Level;
        ranLevel.text = "" + statManager.Ran.Level;
        atkLevel.text = "" + statManager.Atk.Level;
        rollLevel.text = "" + statManager.Rol.Level;
        speLevel.text = "" + statManager.Spe.Level;
    }*/
}