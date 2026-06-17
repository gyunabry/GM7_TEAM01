using UnityEngine;

public enum AttackType
{
    None,
    Melee,
    Dash,
    Range,
    Length
}

public enum BulletPattern
{
    None,
    Straight,
    Cone,
    Circle,
    Orbit
}

[CreateAssetMenu(fileName = "Enemy Attack Data", menuName = "GamePlay/EnemyAttackData")]
public class EnemyAttackData : ScriptableObject
{
    [Header("기본 공격 설정")]
    public AttackType attackType;
    public int attackDamage;
    public float attackCooltime;

    [Header("대쉬 공격 설정")]
    public float dashRange;

    [Header("원거리 공격 설정")]
    public BulletPattern bulletPattern;
    public GameObject projectilePrefab;
    public float projectileSpeed;

    public int projectileCount;
    public float spreadAngle;
    public float orbitRadius;
    public float orbitSpeed;
}
