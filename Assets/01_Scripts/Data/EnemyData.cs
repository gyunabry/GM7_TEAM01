using UnityEngine;

[CreateAssetMenu(fileName = "Enemy Data", menuName = "GamePlay/Enemy Data")]
public class EnemyData : ScriptableObject
{
    public string id;
    public string enemyName;
    public int hp;
    public int maxHp;
    public int armor;
    public float moveSpeed;
}