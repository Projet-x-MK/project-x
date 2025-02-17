using UnityEngine;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer), typeof(MeshCollider))]
public class BezierMeshGenerator : MonoBehaviour
{
    private Vector3[] controlPoints = new Vector3[4]; // 4개의 제어점
    [SerializeField] private float width = 10f; // 점프대 폭
    [SerializeField] private int meshResolution = 10; // 해상도 (곡선 분할 개수)

    private Mesh mesh;

    public void SetControlPoints(Vector3 p0, Vector3 p1, Vector3 p2, Vector3 p3)
    {
        controlPoints[0] = p0;
        controlPoints[1] = p1;
        controlPoints[2] = p2;
        controlPoints[3] = p3;

        GenerateMesh();
    }

    private void GenerateMesh()
    {
        if (controlPoints.Length < 4)
        {
            Debug.LogError("Bezier 곡선을 만들기 위한 4개의 포인트가 설정되지 않았습니다.");
            return;
        }

        mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;

        int totalVertices = (meshResolution + 1) * 4;
        Vector3[] vertices = new Vector3[totalVertices];
        int[] triangles = new int[meshResolution * 12];

        float thickness = 0.2f; // 점프대의 두께 (얇으면 0.1f, 두꺼우면 0.5f)

        for (int i = 0; i <= meshResolution; i++)
        {
            float t = i / (float)meshResolution;
            Vector3 center = BezierCurve.GetPoint(controlPoints[0], controlPoints[1], controlPoints[2], controlPoints[3], t);
            Vector3 direction = (BezierCurve.GetPoint(controlPoints[0], controlPoints[1], controlPoints[2], controlPoints[3], t + 0.01f) - center).normalized;
            Vector3 perpendicular = Vector3.Cross(direction, Vector3.up).normalized * width * 0.5f;

            // 윗면 점들
            vertices[i * 4] = center - perpendicular;
            vertices[i * 4 + 1] = center + perpendicular;

            // 아랫면 점들 (두께 추가)
            vertices[i * 4 + 2] = center - perpendicular - Vector3.up * thickness;
            vertices[i * 4 + 3] = center + perpendicular - Vector3.up * thickness;

            if (i < meshResolution)
            {
                int startIndex = i * 4;
                // 윗면 삼각형
                triangles[i * 12] = startIndex;
                triangles[i * 12 + 1] = startIndex + 4;
                triangles[i * 12 + 2] = startIndex + 1;

                triangles[i * 12 + 3] = startIndex + 1;
                triangles[i * 12 + 4] = startIndex + 4;
                triangles[i * 12 + 5] = startIndex + 5;

                // 아랫면 삼각형 (뒤집어서 추가)
                triangles[i * 12 + 6] = startIndex + 2;
                triangles[i * 12 + 7] = startIndex + 3;
                triangles[i * 12 + 8] = startIndex + 6;

                triangles[i * 12 + 9] = startIndex + 3;
                triangles[i * 12 + 10] = startIndex + 7;
                triangles[i * 12 + 11] = startIndex + 6;
            }
        }

        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.RecalculateNormals();

        // MeshCollider 적용
        MeshCollider meshCollider = GetComponent<MeshCollider>();
        if (meshCollider == null)
            meshCollider = gameObject.AddComponent<MeshCollider>();

        meshCollider.sharedMesh = mesh;
        meshCollider.convex = false;
    }
}
