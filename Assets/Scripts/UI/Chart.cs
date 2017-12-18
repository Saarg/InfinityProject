using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Chart : MonoBehaviour {

    public PlayerNumber player = PlayerNumber.Player1;
    public GameObject quitButton;
	public GameObject restartButton;
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
    protected Vector3[] endPoints;
    protected Vector3[] basePoints;
    protected Text[] levelBoxes;

    // Use this for initialization
    void Start () {
        lineRenderer = this.GetComponent<LineRenderer>();
        statManager = StatManager.Instance;
        endPoints = new Vector3[6];
        basePoints = new Vector3[6];
        lineRenderer.GetPositions(basePoints);
        levelBoxes = new Text[6] { endLevel, hpLevel, ranLevel, speLevel, rollLevel, atkLevel };
        statManager.LevelUp();

		StartCoroutine(LevelUp());

		Time.timeScale = 1;
    }

    // Update is called once per frame
    void Update()
    {
        //Quit the StatDisplayer Scene
        if (MultiOSControls.GetValue("Jump", player) != 0)
        {
            quitButton.GetComponent<Button>().onClick.Invoke();
        }
		// if (MultiOSControls.GetValue ("Fire1", player) != 0)
		// 	restartButton.GetComponent<Button> ().onClick.Invoke ();
    }

    // Put every stat on chart at level 1
    protected void InitializeChart()
    {
        for(int i = 0; i < 6; i++)
        {
            endPoints[i].x = basePoints[i].x / 10f;
            endPoints[i].y = basePoints[i].y / 10f;
        }
        lineRenderer.SetPositions(endPoints);
    }

    // Return true if a textBox have been updated, false otherwise
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

    // Add 1 to selected text box if under the limit
    protected bool AddToBox(Text textBox, int limit)
    {
        int value = System.Int32.Parse(textBox.text);
        if (value <= limit)
        {
            value++;
            textBox.text = "" + value;
            return true;
        }
        return false;
    }

    IEnumerator LevelUp()
    {
        Debug.Log("Coroutine de textBox update");
        yield return new WaitForSeconds(1f);
		StartCoroutine(UpdateChart());

        while (UpdateLevelTextBox())
        {
            yield return new WaitForSeconds(0.1F);
        }

        // For each stat check if level has gone up and Instatiate lvl up icon
        for (int i=0;i< levelBoxes.Length;i++)
        {
            if(statManager.Up[i] == true)
            {
                GameObject go = GameObject.Instantiate(levelUpIcon, levelBoxes[i].transform);
                go.transform.localScale += new Vector3(19,11);
                go.transform.localPosition +=  new Vector3(0,7);
            }
        }
        statManager.Displayed();
		Debug.Log("Coroutine de textBox ended");

    }

    IEnumerator UpdateChart()
    {
        Debug.Log("Coroutine de chart");
        Vector3[] moving = new Vector3[6];
        float step = 1f * Time.deltaTime;

        InitializeChart();

        // Define end position of chart
        for (int i = 0; i < 6; i++)
        {
            endPoints[i].x = basePoints[i].x * (float)(statManager.Stats[i].Level + 1) / 10f;
            endPoints[i].y = basePoints[i].y * (float)(statManager.Stats[i].Level + 1) / 10f;
        }

        // Animate chart and move it toward end position
        lineRenderer.GetPositions(moving);
        while (!AreEqual(moving, endPoints))
        {
            for (int i = 0; i < 6; i++)
            {
                moving[i] = Vector3.MoveTowards(moving[i], endPoints[i], step);
            }
            lineRenderer.SetPositions(moving);
            yield return null;
        }
        
    }

    // Define if 2 arrays of same size have every points on the same x and y points
    protected bool AreEqual(Vector3[] a, Vector3[] b)
    {
        if (a.Length != b.Length)
            return false;
        for(int i =0; i<a.Length; i++)
        {
            if((a[i].x != b[i].x) || (a[i].y != b[i].y))
            {
                return false;
            }
        }
        return true;
    }
    
}
