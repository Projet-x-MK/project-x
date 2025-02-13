using UnityEngine;

public class BezierCurve : MonoBehaviour
{
    public Transform point0, point1, point2, point3; // 4개의 제어점
    public LineRenderer lineRenderer;
    public int curveResolution = 50; // 곡선의 세밀함

    private void Start()
    {
        DrawCurve();
    }

    public Vector3 GetPoint(float t)
    {
        // Bézier 곡선 공식 적용
        float u = 1 - t;
        float tt = t * t;
        float uu = u * u;
        float uuu = uu * u;
        float ttt = tt * t;

        Vector3 p = uuu * point0.position; 
        p += 3 * uu * t * point1.position;
        p += 3 * u * tt * point2.position;
        p += ttt * point3.position;

        return p;
    }

    public void DrawCurve()
    {
        if (lineRenderer == null) return;
        lineRenderer.positionCount = curveResolution + 1;

        for (int i = 0; i <= curveResolution; i++)
        {
            float t = i / (float)curveResolution;
            lineRenderer.SetPosition(i, GetPoint(t));
        }
    }
}
