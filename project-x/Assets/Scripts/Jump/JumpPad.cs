using UnityEngine;

[RequireComponent(typeof(BezierMeshGenerator))]
public class JumpPad : MonoBehaviour
{
    private BezierMeshGenerator bezierMeshGenerator;
    private JumpPadConfig jumpPadConfig;

    public void Initialize(string configName)
    {
        bezierMeshGenerator = GetComponent<BezierMeshGenerator>();

        // 🔥 자동으로 설정 불러오기
        jumpPadConfig = JumpPadConfig.LoadConfig(configName);

        if (jumpPadConfig != null)
        {
            bezierMeshGenerator.SetControlPoints(
                jumpPadConfig.point0,
                jumpPadConfig.point1,
                jumpPadConfig.point2,
                jumpPadConfig.point3
            );
        }
        else
        {
            Debug.LogError($"점프대 설정을 찾을 수 없습니다: {configName}");
        }
    }

    private void Start()
    {
        Initialize("CurvedJump"); // 🔥 기본적으로 'CurvedJump' 설정을 불러옴
    }
}
