using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Chart : MonoBehaviour {

    public PlayerNumber player = PlayerNumber.Player1;
    public GameObject quitButton;
    public GameObject levelUpIcon;
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
    protected PlayerStats[] baseStats;
    protected PlayerStats[] finalStats;
    protected Text[] levelBoxes;

    // Use this for initialization
    void Start () {
        lineRenderer = this.GetComponent<LineRenderer>();
        statManager = StatManager.Instance;
        points = new Vector3[6];
        basePoints = new Vector3[6];
        lineRenderer.GetPositions(basePoints);
        levelBoxes = new Text[6] { endLevel, hpLevel, ranLevel, speLevel, rollLevel, atkLevel };
        //PlayerStats[] baseStats = statManager.Stats;
        //statManager.LevelUp();

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

    protected void InitializeChart()
    {
        for(int i = 0; i < 6; i++)
        {
            points[i].x = basePoints[i].x * 1.0F / 20;
            points[i].y = basePoints[i].y * 1.0F / 20;
        }
        lineRenderer.SetPositions(points);
    }

    protected bool UpdateLevelTextBox()
    {
        bool changed = false;

        if (AddToBox(endLevel, statManager.End.Level))
            changed = true;

        if (AddToBox(hpLevel, statManager.Hp.Level))
            changed = true;

        if (AddToBox(ranLevel, statManager.Ran.Level))
            changed = true;

        if (AddToBox(atkLevel, statManager.Atk.Level))
            changed = true;

        if (AddToBox(rollLevel, statManager.Rol.Level))
            changed = true;

        if (AddToBox(speLevel, statManager.Spe.Level))
            changed = true;

        return changed;
    }

    protected bool AddToBox(Text textBox, int limit)
    {
        int value = System.Int32.Parse(textBox.text);
        if (value < limit)
        {
            value++;
            textBox.text = "" + value;
            return true;
        }
        return false;
    }

    IEnumerator LevelUp()
    {
        yield return new WaitForSeconds(1f);
        StartCoroutine("UpdateChart");
        while (UpdateLevelTextBox())
        {
            yield return new WaitForSeconds(0.1F);
            UpdateLevelTextBox();
        }
        for(int i=0;i< levelBoxes.Length;i++)
        {
            if(statManager.Up[i] == true)
            {
                GameObject go = GameObject.Instantiate(levelUpIcon, levelBoxes[i].transform);
                go.transform.localScale += new Vector3(19,11);
                go.transform.localPosition +=  new Vector3(0,7);
            }
        }
        statManager.Displayed();

    }

    IEnumerator UpdateChart()
    {
        Vector3[] moving = new Vector3[6];
        float step = 1f * Time.deltaTime;

        InitializeChart();

        for (int i = 0; i < 6; i++)
        {
            points[i].x = basePoints[i].x * (float)(statManager.Stats[i].Level + 1) / 20;
            points[i].y = basePoints[i].y * (float)(statManager.Stats[i].Level + 1) / 20;
        }

        lineRenderer.GetPositions(moving);

        while (!AreEqual(moving, points))
        {
            for (int i = 0; i < 6; i++)
            {
                moving[i] = Vector3.MoveTowards(moving[i], points[i], step);
            }
            lineRenderer.SetPositions(moving);
            yield return null;
        }
        
    }

    protected bool AreEqual(Vector3[] a, Vector3[] b)
    {
        bool equal = true;
        for(int i =0; i<a.Length; i++)
        {
            if((a[i].x != b[i].x) || (a[i].y != b[i].y))
            {
                equal = false;
            }
        }
        return equal;
    }

    public void SaveBase(PlayerStats[] stats)
    {
        baseStats = stats;
        Debug.Log("Base stats saved");
    }

    public void SaveFinal(PlayerStats[] stats)
    {
        finalStats = stats;
        Debug.Log("Final stats saved");
    }
}
