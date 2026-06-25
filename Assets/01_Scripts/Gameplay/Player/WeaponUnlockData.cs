using DG.Tweening.Plugins.Core.PathCore;
using System.Collections.Generic;
using System.IO;
using Unity.VisualScripting;
using UnityEngine;

public class WeaponUnlockData : MonoBehaviour
{
    string path;
    void Start()
    {
        path = System.IO.Path.Combine(Application.dataPath + "/05_Data/Weapon/", "UnlockWeaponData.json");
    }

    public void SaveWeaponUnlockData()
    {
        UnlockData unlockData = new UnlockData();
        string jsonText = JsonUtility.ToJson(unlockData, true);
        File.WriteAllText(path, jsonText);
    }
    public void LoadWeaponUnlockData()
    {
        UnlockData unlockData = new UnlockData();
        if (!File.Exists(path))
        {
            Debug.Log("불러올 파일이 없음");
        }
        else
        {
            string jsonText = File.ReadAllText(path);

            unlockData = JsonUtility.FromJson<UnlockData>(jsonText);
        }
    }
}
[System.Serializable]
public class UnlockData
{
    public List<PlayerWeaponSO> unlockWeaponData = new List<PlayerWeaponSO>();
    public int killCount = 0;
}