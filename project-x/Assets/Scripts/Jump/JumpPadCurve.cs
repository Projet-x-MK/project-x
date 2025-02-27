using UnityEngine;

public class JumpPadCurve : MonoBehaviour
{
    public enum JumpPadType { A, B }

    [SerializeField] private JumpPadType jumpPadType;
    [SerializeField] public int curveResolution = 20;
    [SerializeField] public float width = 1f;

    private JumpPadMesh jumpPadMesh;
    private JumpPadCollider jumpPadCollider;

    private void Awake()
    {
        jumpPadMesh = gameObject.AddComponent<JumpPadMesh>();
        jumpPadCollider = gameObject.AddComponent<JumpPadCollider>();

        Vector3[] points = JumpPadConfig.GetJumpPadPoints(jumpPadType);

        jumpPadMesh.CreateMesh(curveResolution, width, points[0], points[1], points[2], points[3]);
        jumpPadCollider.ApplyCollider(jumpPadMesh.Mesh);
    }
}
