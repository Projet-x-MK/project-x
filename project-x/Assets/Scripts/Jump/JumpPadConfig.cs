using UnityEngine;

[CreateAssetMenu(fileName = "JumpPadConfig", menuName = "JumpPad/New JumpPad Config")]
public class JumpPadConfig : ScriptableObject
{
    public string jumpPadName;
    public Vector3 point0;
    public Vector3 point1;
    public Vector3 point2;
    public Vector3 point3;

    // 🔥 특정 이름으로 설정 찾기
    public static JumpPadConfig LoadConfig(string name)
    {
        JumpPadConfig config = Resources.Load<JumpPadConfig>($"JumpPadConfigs/{name}");
        return config;
    }
}
