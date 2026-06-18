using UnityEngine;
using UnityEngine.Events;

public class PlayerAttackPoint : MonoBehaviour
{
    
    private UnityEvent ue;
    private PlayerAttack playerAttack;
    private PlayerController playerController;
    private PlayerWeaponSO.WeaponType weaponType;
    private PlayerWeaponSO weaponStat;

    private void Awake()
    {
        ue = new UnityEvent();
    }
    private void Start()
    {
        playerAttack = GetComponentInParent<PlayerAttack>();
        playerController = FindAnyObjectByType<PlayerController>();
        if (transform.parent != null)
        {
        weaponType = playerAttack.GetParentType();
        }
        weaponStat = playerController.GetWeaponStat(weaponType);
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            float damage = weaponStat.weaponDamage;
            ue?.Invoke();
            
        }
    }
}
