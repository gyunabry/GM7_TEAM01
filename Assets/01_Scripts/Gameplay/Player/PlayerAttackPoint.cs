using UnityEngine;
using UnityEngine.Events;

public class PlayerAttackPoint : MonoBehaviour
{
    private PlayerWeaponSO playerWeapon;
    private PlayerController playerController;
    private UnityEvent ue;
    
    private void Awake()
    {
        playerController = FindAnyObjectByType<PlayerController>();
        playerWeapon = GetComponentInParent<PlayerWeaponSO>();
        ue = GetComponent<UnityEvent>();
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            Debug.Log(playerWeapon.weaponDamage);
            ue?.Invoke();
            
        }
    }
}
