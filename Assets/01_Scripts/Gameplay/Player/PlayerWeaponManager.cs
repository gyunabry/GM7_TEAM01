using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using AYellowpaper.SerializedCollections;

public class PlayerWeaponManager : MonoBehaviour
{
    [SerializedDictionary("WeaponType", "Weapon")]
    public SerializedDictionary<PlayerWeaponSO.WeaponType, PlayerWeaponSO> playerWeapon;
    [SerializedDictionary("unWeaponType", "unWeapon")]
    public SerializedDictionary<PlayerWeaponSO.WeaponType, PlayerWeaponSO> unlockWeapon;
    [SerializeField]
    private List<int> unlockCount = new List<int>();
    [SerializeField]
    private List<PlayerWeaponSO.WeaponType> unlockType = new List<PlayerWeaponSO.WeaponType>();
    private int unlockList = 0;
    GameManager gameManager;
    WeaponUnlockData wud;
    private void Awake()
    {
        unlockList = 0;
        gameManager = FindAnyObjectByType<GameManager>();
        wud = new WeaponUnlockData();
        foreach (KeyValuePair<PlayerWeaponSO.WeaponType, PlayerWeaponSO> pW in playerWeapon)
        {
            pW.Value.ResetStatUpgrade();
        }
        UnlockData unlockData = new UnlockData();
        wud.LoadWeaponUnlockData();
        foreach(PlayerWeaponSO pws in unlockData.unlockWeaponData)
        {

        }
    }
    private void Update()
    {
        if(unlockCount.Count <= unlockList)
        {
            return;
        }
        else
        {
            if (unlockWeapon.TryGetValue(unlockType[unlockList], out var unlockWeaponGet))
            {

            }
            if (gameManager.KillCount >= unlockCount[unlockList])
            {

                if (unlockWeapon.TryGetValue(unlockType[unlockList], out var nowUnlockWeapon))
                {
                    playerWeapon.TryAdd(unlockType[unlockList], nowUnlockWeapon);
                    UnlockData unlockData = new UnlockData();
                    unlockData.unlockWeaponData.Add(nowUnlockWeapon);
                    unlockData.unlockWeaponData[unlockList].unlocking = true;
                    unlockList++;
                    unlockData.killCount = gameManager.KillCount;
                    
                    wud.SaveWeaponUnlockData();
                }
            }
        }
    }
    public PlayerWeaponSO GetWeapon(PlayerWeaponSO.WeaponType type)
    {
        foreach (KeyValuePair<PlayerWeaponSO.WeaponType, PlayerWeaponSO> weapon in playerWeapon)
        {
            if (weapon.Value.weaponType == type)
            {
                return weapon.Value;
            }
        }
        return null;
    }
    public void SetWeapon(PlayerWeaponSO.WeaponType type, PlayerWeaponSO value)
    {
        playerWeapon.TryAdd(type, value);
    }
    public PlayerWeaponSO.WeaponType GetWeaponType(PlayerWeaponSO.WeaponType type)
    {
        foreach (KeyValuePair<PlayerWeaponSO.WeaponType, PlayerWeaponSO> weapon in playerWeapon)
        {
            if (weapon.Value.weaponType == type)
            {
                return weapon.Value.weaponType;
            }
        }
        return PlayerWeaponSO.WeaponType.Null;
    }
    public PlayerWeaponSO[] GetWeaponList()
    {
        return playerWeapon.Values.ToArray();
    }
}