using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.UI;

public class LevelUp : MonoBehaviour
{
    [SerializeField] private PlayerWeaponManager playerWeapon;
    [SerializeField] private PlayerController playerController;
    [SerializeField] private Button button;

    private Dictionary<PlayerWeaponSO.WeaponType, PlayerWeaponSO> equWeaponList = new Dictionary<PlayerWeaponSO.WeaponType, PlayerWeaponSO>();
    private PlayerWeaponSO[] weaponList;
    private Image[] weaponImage = new Image[3];
    private Button[] go = new Button[3];
    private Coroutine co;
    
    private string[] weaponDes = new string[3];
    private int[] ran = new int[3];
    private int i = 0;
    private int dho = 0;
    private string iconName;
    private void OnEnable()
    {
        
        equWeaponList = playerController.GetWeaponList();
        weaponList = playerWeapon.GetWeaponList();
        GetImageTask();
    }
    
    public async Task GetImageTask()
    {
        for (i = 0; i < 3; i++)
        {
            while (true)
            {
                ran[i] = Random.Range(0, weaponList.Length);
                if (i == 1)
                {
                    if (ran[i - 1] == ran[i])
                    {
                        continue;
                    }
                }
                else if (i == 2)
                {
                    if (ran[i - 2] == ran[i] || ran[i - 1] == ran[i])
                    {
                        continue;
                    }
                }
                break;
            }
            PlayerWeaponSO pws;
            if (equWeaponList.TryGetValue(weaponList[ran[i]].weaponType, out pws))
            {
                weaponDes[i] = weaponList[ran[i]].weaponDes;
                iconName = weaponList[ran[i]].weaponIcon.ToString().Replace("_0 (UnityEngine.Sprite)", "");
                if (transform.childCount != 3)
                {
                    go[i] = Instantiate(button);
                }
                Image[] childImage = go[i].GetComponentsInChildren<Image>();
                weaponImage[i] = childImage[1];
                Sprite sprite = await Addressables.LoadAssetAsync<Sprite>(iconName).Task;
                weaponImage[dho].sprite = sprite;
                dho++;
                if (i == 0)
                {
                    go[i].onClick.AddListener(GetUpgrade1);
                }
                else if (i == 1)
                {
                    go[i].onClick.AddListener(GetUpgrade2);
                }
                else if (i == 2)
                {
                    go[i].onClick.AddListener(GetUpgrade3);
                }
                TextMeshProUGUI text = go[i].GetComponentInChildren<TextMeshProUGUI>();
                text.text = weaponDes[i] + "";
            }
            else
            {
                weaponDes[i] = weaponList[ran[i]].weaponDes;
                iconName = weaponList[ran[i]].weaponIcon.ToString().Replace("_0 (UnityEngine.Sprite)", "");
                if(transform.childCount != 3)
                {
                    go[i] = Instantiate(button);
                }
                Image[] childImage = go[i].GetComponentsInChildren<Image>();
                weaponImage[i] = childImage[1];
                Sprite sprite = await Addressables.LoadAssetAsync<Sprite>(iconName).Task;
                weaponImage[dho].sprite = sprite;
                dho++;
                go[i].transform.SetParent(transform);
                if (i == 0)
                {
                    go[i].onClick.AddListener(GetWeapon1);
                }
                else if (i == 1)
                {
                    go[i].onClick.AddListener(GetWeapon2);
                }
                else if (i == 2)
                {
                    go[i].onClick.AddListener(GetWeapon3);
                }
                TextMeshProUGUI text = go[i].GetComponentInChildren<TextMeshProUGUI>();
                text.text = weaponDes[i] + "";
            }
            if (i == 0)
            {
                go[i].transform.localPosition = new Vector3(-100f, 0f, 0f);
            }
            else if (i == 1)
            {
                go[i].transform.localPosition = new Vector3(0f, 0f, 0f);
            }
            else if (i == 2)
            {
                go[i].transform.localPosition = new Vector3(100f, 0f, 0f);
            }
        }
    }
    public void GetWeapon1()
    {
        playerController.OnWeaponArm(weaponList[ran[0]].weaponType);
        i = 0;
        dho = 0;
        gameObject.SetActive(false);
    }
    public void GetWeapon2()
    {
        playerController.OnWeaponArm(weaponList[ran[1]].weaponType);
        i = 0;
        dho = 0;
        gameObject.SetActive(false);
    }
    public void GetWeapon3()
    {
        playerController.OnWeaponArm(weaponList[ran[2]].weaponType);
        i = 0;
        dho = 0;
        gameObject.SetActive(false);
    }
    public void GetUpgrade1()
    {
        i = 0;
        dho = 0;
        gameObject.SetActive(false);
    }
    public void GetUpgrade2()
    {
        i = 0;
        dho = 0;
        gameObject.SetActive(false);
    }
    public void GetUpgrade3()
    {
        i = 0;
        dho = 0;
        gameObject.SetActive(false);
    }
}
