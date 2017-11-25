using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Chart : MonoBehaviour {

    public PlayerNumber player = PlayerNumber.Player1;
    public GameObject quitButton;
    [Space(10)]
    public Text endLevel;
    public Text hpLevel;
    public Text ranLevel;
    [Space(5)]
    public Text atkLevel;
    public Text rollLevel;
    public Text speLevel;

    protected RectTransform rectTransform;
    protected LineRenderer lineRenderer;
    protected StatManager statManager;
    protected Vector3[] points;
    protected Vector3[] basePoints;
    
    // Use this for initialization
    void Start () {
        lineRenderer = this.GetComponent<LineRenderer>();
        statManager = StatManager.Instance;
        points = new Vector3[6];
        basePoints = new Vector3[6];
        lineRenderer.GetPositions(basePoints);

        PlayerStats[] baseStats = statManager.Stats;
        StartCoroutine("LevelUp");
    }

    // Update is called once per frame
    void Update()
    {
        //Quit the StatDisplayer Scene
        if (MultiOSControls.GetValue("Jump", player) != 0)
        {
            quitButton.GetComponent<Button>().onClick.Invoke();
        }
    }

    protected void UpdateChart()
    {
        for (int i = 0; i < 6; i++)
        {
            points[i].x = basePoints[i].x * (float)(statManager.Stats[i].Level + 1) / 20;
            points[i].y = basePoints[i].y * (float)(statManager.Stats[i].Level + 1) / 20;
        }
        lineRenderer.SetPositions(points);
    }

    protected void UpdateLevelTextBox()
    {
        endLevel.text = "" + statManager.End.Level;
        hpLevel.text = "" + statManager.Hp.Level;
        ranLevel.text = "" + statManager.Ran.Level;
        atkLevel.text = "" + statManager.Atk.Level;
        rollLevel.text = "" + statManager.Rol.Level;
        speLevel.text = "" + statManager.Spe.Level;
    }

    IEnumerator LevelUp()
    {
        UpdateChart();
        foreach (PlayerStats ps in statManager.Stats)
        {
            while (statManager.LevelUp(ps,true))
            {
                yield return new WaitForSeconds(0.1F);
                UpdateLevelTextBox();
                UpdateChart();
            }
            
        }
        
    }
}
