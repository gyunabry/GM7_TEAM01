using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            // 플레이어 피격 처리
            // 풀 반환 처리
            PoolManager.Instance.ReturnPool(this);
        }
    }
}