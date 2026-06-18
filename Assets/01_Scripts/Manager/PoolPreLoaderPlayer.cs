using UnityEngine;

public class PoolPreLoaderPlayer : MonoBehaviour
{
    [Header("생성할 프리팹")]
    [SerializeField] private Arrow arrow;
    // 필요시 여기에 추가

    [Header("초기 생성 수")]
    [SerializeField] private int arrowCount;

    private void Start()
    {
        PoolManager.Instance.PreLoadPool(arrow, arrowCount);
    }
}
