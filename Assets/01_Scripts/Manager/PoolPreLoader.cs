using UnityEngine;

public class PoolPreLoader : MonoBehaviour
{
    [Header("생성할 프리팹")]
    [SerializeField] private EnemyController enemyPrefab;
    [SerializeField] private EnemyBullet enemyBulletPrefab;
    // 필요시 여기에 추가

    [Header("초기 생성 수")]
    [SerializeField] private int enemyCount;
    [SerializeField] private int enemyBulletCount;

    private void Start()
    {
        PoolManager.Instance.PreLoadPool(enemyPrefab, enemyCount);
        PoolManager.Instance.PreLoadPool(enemyBulletPrefab, enemyBulletCount);
    }
}
