using System.Collections.Generic;
using UnityEngine;

public class LineDrawer : MonoBehaviour
{
    public LineRenderer lineRenderer; // Reference to the LineRenderer component
    public List<Vector3> points = new List<Vector3>(); // Array of points to define the line

    void Start()
    {
        // Ensure LineRenderer is assigned
        if (lineRenderer == null)
        {
            lineRenderer = GetComponent<LineRenderer>();
        }

        if (lineRenderer == null)
        {
            Debug.LogError("LineRenderer component is missing.");
            return;
        }
    }
    public void AddPoints(Vector3 linePoints)
    {
        points.Add(linePoints);
        //Debug.Log(linePoints);
    }

    public void DrawLine()
    {
        // Set the number of positions in the LineRenderer
        lineRenderer.positionCount = points.Count;
        // Set the positions for the LineRenderer
        for (int i = 0; i < points.Count; i++)
        {
            lineRenderer.SetPosition(i, points[i]);
        }
    }
}