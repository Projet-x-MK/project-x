using UnityEngine;
using System.Collections.Generic;

public static class JumpPadConfig
{
    private static readonly Dictionary<JumpPadCurve.JumpPadType, Vector3[]> jumpPadPoints = new Dictionary<JumpPadCurve.JumpPadType, Vector3[]>
    {
        { JumpPadCurve.JumpPadType.A, new Vector3[] { new Vector3(0, 0, 0), new Vector3(0, 1.5f, 4), new Vector3(0, 3, 9), new Vector3(0, 12, 15) } },
        { JumpPadCurve.JumpPadType.B, new Vector3[] { new Vector3(0, 0, 0), new Vector3(2, 3, 4), new Vector3(4, 3, 4), new Vector3(6, 0, 0) } }
    };

    public static Vector3[] GetJumpPadPoints(JumpPadCurve.JumpPadType type)
    {
        return jumpPadPoints.ContainsKey(type) ? jumpPadPoints[type] : null;
    }
}
