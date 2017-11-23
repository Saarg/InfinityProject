using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chart : MonoBehaviour {

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
        /*
        Color c1 = Color.black;
        lineRenderer.startColor = c1;
        lineRenderer.endColor = c1;
        */
    }

    // Update is called once per frame
    void Update()
    {
        lineRenderer.GetPositions(points);

        for (int i = 0; i < 6; i++)
        {
            points[i].x = basePoints[i].x * (float)(statManager.Stats[i].Level + 1) / 20;
            points[i].y = basePoints[i].y * (float)(statManager.Stats[i].Level + 1) / 20;
        }
        lineRenderer.SetPositions(points);
    }
}
