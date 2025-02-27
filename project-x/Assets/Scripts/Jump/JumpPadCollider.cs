using UnityEngine;

public class JumpPadCollider : MonoBehaviour
{
    private MeshCollider meshCollider;

    private void Awake()
    {
        meshCollider = gameObject.AddComponent<MeshCollider>();
        meshCollider.convex = false; // Convex 비활성화 (Frustum Culling 문제 방지)
    }

    public void ApplyCollider(Mesh mesh)
    {
        meshCollider.sharedMesh = mesh;
        meshCollider.enabled = true;
    }
}
