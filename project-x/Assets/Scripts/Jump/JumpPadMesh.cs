using System.Collections.Generic;
using UnityEngine;

public class JumpPadMesh : MonoBehaviour
{
    public Mesh Mesh { get; private set; }

    public void CreateMesh(int curveResolution, float width, Vector3 p0, Vector3 p1, Vector3 p2, Vector3 p3)
    {
        Mesh = new Mesh();
        List<Vector3> vertices = new List<Vector3>();
        List<int> triangles = new List<int>();

        for (int i = 0; i < curveResolution; i++)
        {
            float t = i / (float)(curveResolution - 1);
            Vector3 point = JumpPadCurveUtility.CalculateBezierPoint(t, p0, p1, p2, p3);

            Vector3 left = point - Vector3.right * (width / 2);
            Vector3 right = point + Vector3.right * (width / 2);


            vertices.Add(left);
            vertices.Add(right);

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

        GetComponent<MeshFilter>().mesh = Mesh;
    }
}
