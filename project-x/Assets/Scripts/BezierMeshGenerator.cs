using UnityEngine;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer), typeof(MeshCollider))]
public class BezierMeshGenerator : MonoBehaviour
{
    public BezierCurve bezierCurve;
    public float width = 2f;
    public int meshResolution = 10;
    public bool addBackfaceCollider = true; // 추가적인 뒷면 충돌 여부

    private Mesh mesh;

    private void Start()
    {
        GenerateMesh();
    }

    private void GenerateMesh()
    {
        if (bezierCurve == null) 
        {
            Debug.LogError("BezierCurve가 BezierMeshGenerator에 연결되지 않았습니다!");
            return;
        }

        Debug.Log("Bezier Mesh 생성 시작");

        mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;

        int totalVertices = (meshResolution + 1) * 4; // 윗면 + 아랫면
        Vector3[] vertices = new Vector3[totalVertices];
        int[] triangles = new int[meshResolution * 12]; // 윗면 6개 + 아랫면 6개 = 12개

        float thickness = 0.2f; // 🔥 두께 조절 가능 (얇으면 0.1f, 두꺼우면 0.5f)

        for (int i = 0; i <= meshResolution; i++)
        {
            float t = i / (float)meshResolution;
            Vector3 center = bezierCurve.GetPoint(t);
            Vector3 direction = (bezierCurve.GetPoint(t + 0.01f) - center).normalized;
            Vector3 perpendicular = Vector3.Cross(direction, Vector3.up).normalized * width * 0.5f;

            // 윗면 (기존 점프대)
            vertices[i * 4] = center - perpendicular;
            vertices[i * 4 + 1] = center + perpendicular;

            // 아랫면 (두께 추가)
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

        Debug.Log("Bezier Mesh 생성 완료! 두께 추가됨");
    }

}
