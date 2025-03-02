using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]     // 씬 뷰에서 자동으로 보이게 해주는 속성
public class JumpPadMesh : MonoBehaviour
{
    public Mesh Mesh { get; private set; }

    public void CreateMesh(int curveResolution, float width, Vector3 p0, Vector3 p1, Vector3 p2, Vector3 p3)
{
    Mesh = new Mesh();
    List<Vector3> vertices = new List<Vector3>();
    List<int> triangles = new List<int>();

    Vector3 previousTangent = (p1 - p0).normalized; // 첫 번째 Tangent 미리 계산

    for (int i = 0; i < curveResolution; i++)
    {
        float t = i / (float)(curveResolution - 1);
        Vector3 point = JumpPadCurveUtility.CalculateBezierPoint(t, p0, p1, p2, p3);

        // 현재 Tangent 계산
        Vector3 tangent = (i < curveResolution - 1)
            ? (JumpPadCurveUtility.CalculateBezierPoint(t + (1f / curveResolution), p0, p1, p2, p3) - point).normalized
            : previousTangent;

        previousTangent = tangent;

        // 항상 Vector3.up 기준으로 Right 방향 구하기 (월드 좌표 기준이 아니라 곡선 기준)
        Vector3 right = Vector3.Cross(Vector3.up, tangent).normalized;

        Vector3 leftPoint = point - right * (width / 2);
        Vector3 rightPoint = point + right * (width / 2);

        vertices.Add(leftPoint);
        vertices.Add(rightPoint);

        if (i < curveResolution - 1)
        {
            int index = i * 2;
            triangles.Add(index);
            triangles.Add(index + 2);
            triangles.Add(index + 1);

            triangles.Add(index + 1);
            triangles.Add(index + 2);
            triangles.Add(index + 3);
        }
    }

    Mesh.vertices = vertices.ToArray();
    Mesh.triangles = triangles.ToArray();
    Mesh.RecalculateNormals();
    Mesh.RecalculateBounds();

    GetComponent<MeshFilter>().mesh = Mesh;
}
private void OnDrawGizmos()
    {
        if (Mesh == null) return;

        Gizmos.color = Color.magenta;

        for (int i = 0; i < Mesh.vertices.Length; i++)
        {
            Gizmos.DrawSphere(transform.TransformPoint(Mesh.vertices[i]), 0.05f);
        }
    }

    // 씬 뷰에서 바로 자동으로 보이게
    private void OnValidate()
    {
        if (!Application.isPlaying)
        {
            CreateMesh(30, 1.5f, new Vector3(0, 0, 0), new Vector3(5, 3, 4), new Vector3(10, 3, 4), new Vector3(15, 0, 0));
        }
    }

}