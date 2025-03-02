using UnityEngine;
using System.Collections.Generic;

public static class JumpPadConfig
{
    private static readonly Dictionary<JumpPadCurve.JumpPadType, Vector3[]> jumpPadPoints = new Dictionary<JumpPadCurve.JumpPadType, Vector3[]>
    {
        { JumpPadCurve.JumpPadType.A1, new Vector3[] { new Vector3(0, 0, 0), new Vector3(4, 1.5f, 0), new Vector3(11, 4, 0), new Vector3(15, 12, 0) } },
        { JumpPadCurve.JumpPadType.A2, new Vector3[] { new Vector3(0, 12, 0), new Vector3(4, 4, 0), new Vector3(12, 1, 0), new Vector3(15, 6, 0) } },
        { JumpPadCurve.JumpPadType.B, new Vector3[] { new Vector3(0, 0, 0), new Vector3(4, 12, 8), new Vector3(11, 12, 8), new Vector3(15, 0, 0) } },
        { JumpPadCurve.JumpPadType.C, new Vector3[] { new Vector3(0, 5, 0), new Vector3(13, 2.5f, 0), new Vector3(15, 2.5f, -2), new Vector3(15, 5, -15) } },
    };

    public static Vector3[] GetJumpPadPoints(JumpPadCurve.JumpPadType type)
    {
        return jumpPadPoints.ContainsKey(type) ? jumpPadPoints[type] : null;
    }
}
