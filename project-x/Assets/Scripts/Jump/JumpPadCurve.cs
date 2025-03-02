using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class JumpPadCurve : MonoBehaviour
{
    public enum JumpPadType { A1, A2, B, C }

    [SerializeField] private JumpPadType jumpPadType;
    [SerializeField] public int curveResolution = 20;
    [SerializeField] public float width = 1f;

    private JumpPadMesh jumpPadMesh;
    private JumpPadCollider jumpPadCollider;

    private void OnValidate()
    {
        if (Application.isPlaying) return;

        if (jumpPadMesh == null)
            jumpPadMesh = gameObject.GetComponent<JumpPadMesh>() ?? gameObject.AddComponent<JumpPadMesh>();

        if (jumpPadCollider == null)
            jumpPadCollider = gameObject.GetComponent<JumpPadCollider>() ?? gameObject.AddComponent<JumpPadCollider>();

        Vector3[] points = JumpPadConfig.GetJumpPadPoints(jumpPadType);
        if (points.Length == 4)
        {
            jumpPadMesh.CreateMesh(curveResolution, width, points[0], points[1], points[2], points[3]);
            jumpPadCollider.ApplyCollider(jumpPadMesh.Mesh);
        }
    }


    #if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        Vector3[] points = JumpPadConfig.GetJumpPadPoints(jumpPadType);
        Gizmos.color = Color.red;
        if (curveResolution > 1 && points.Length == 4)
        {
            for (int i = 0; i < curveResolution - 1; i++)
            {
                float t1 = i / (float)(curveResolution - 1);
                float t2 = (i + 1) / (float)(curveResolution - 1);
                Gizmos.DrawLine(
                    JumpPadCurveUtility.CalculateBezierPoint(t1, points[0], points[1], points[2], points[3]),
                    JumpPadCurveUtility.CalculateBezierPoint(t2, points[0], points[1], points[2], points[3])
                );
            }
        }
    }
    #endif
}