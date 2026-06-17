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
    private EnemyData currentEnemy;
    public Transform target;

    private Rigidbody2D rb;
    private NavMeshAgent agent;

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
        currentHp = maxHp;
    }

    private void Update()
    {
        DetectTarget();
        if (target != null)
        {
            ChaseTarget();
        }
    }

    // 외부에서 적 오브젝트에게 데이터를 주입하는 메서드
    public void Initialize(EnemyData data)
    {
        currentEnemy = data;

        maxHp = data.maxHp;
        armor = data.armor;
        moveSpeed = data.moveSpeed;

        ApplyStatusAgent();
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

    // 플레이어를 추적해 이동하는 메서드
    private void ChaseTarget()
    {
        //Vector3 direction = target.position - transform.position;
        //direction.Normalize();

        //Vector3 nextPosition = transform.position + direction * moveSpeed * Time.fixedDeltaTime;
        //rb.MovePosition(nextPosition);
        agent.SetDestination(target.position);
    }

    // 주입받은 데이터를 NavMeshAgent의 필드에 할당
    private void ApplyStatusAgent()
    {
        agent.speed = moveSpeed;
    }
}