using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    private void OnEnable()
    {
        // 활성화 4초 이후 풀로 반환
        Invoke(nameof(ReturnToPool), 4f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            // 플레이어 피격 처리
            // 풀 반환 처리
            ReturnToPool();
        }
    }

    private void ReturnToPool()
    {
        CancelInvoke(nameof(ReturnToPool));
        PoolManager.Instance.ReturnPool(this);
    }
}