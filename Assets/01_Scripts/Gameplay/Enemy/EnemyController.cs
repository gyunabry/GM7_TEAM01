using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

/*
- 스포너에서 데이터를 주입할 수 있도록 메서드 정의
- 플레이어 추적 기능
 */

public class EnemyController : MonoBehaviour
{
    [Header("기본 설정")]
    [SerializeField] private int maxHp;
    [SerializeField] private int armor;
    [SerializeField] private float moveSpeed;
    [SerializeField] private LayerMask targetLayer;

    private int currentHp;

    // 현재 적 오브젝트에 주입된 몬스터 데이터
    [SerializeField] private EnemyData currentEnemy;
    public Transform target;

    private Rigidbody2D rb;
    private NavMeshAgent agent;

    private Coroutine chaseCoroutine;
    // 코루틴 내에서 SetDestination 호출 딜레이
    private WaitForSeconds chaseInterval = new WaitForSeconds(0.5f);

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        agent = GetComponent<NavMeshAgent>();

        // Z축 회전 방지
        agent.updateRotation = false;
        agent.updateUpAxis = false;
    }

    private void OnEnable()
    {
        // 풀에서 꺼내어질 때 스스로 기본 데이터 초기화
        maxHp = currentEnemy.maxHp;
        armor = currentEnemy.armor;
        moveSpeed = currentEnemy.moveSpeed;

        currentHp = maxHp;
        ApplyStatusAgent();
    }

    private void Update()
    {
        DetectTarget();

        // 타겟을 찾았다면 추적 코루틴 실행
        if (target != null || chaseCoroutine == null)
        {
            chaseCoroutine = StartCoroutine(ChaseTargetCo());
        }
    }

    private void DetectTarget()
    {
        if (target != null) return;

        Collider2D hits = Physics2D.OverlapCircle(transform.position, 100f, targetLayer);
        if (hits != null)
        {
            target = hits.transform;
        }
    }

    // 주입받은 데이터를 NavMeshAgent의 필드에 할당
    private void ApplyStatusAgent()
    {
        agent.speed = moveSpeed;
    }

    // NavMeshAgent의 이동 목적지로 타겟의 위치를 설정
    // 최적화를 위해 코루틴에서 SetDestination 호출에 딜레이
    private IEnumerator ChaseTargetCo()
    {
        while (target != null)
        {
            agent.SetDestination(target.position);
            yield return chaseInterval;
        }
        chaseCoroutine = null;
    }

    private void ReturnToPool()
    {
        PoolManager.Instance.ReturnPool(this);
    }
}