using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [Header("구독할 이벤트")]
    [SerializeField] private VoidEventChannel playerDeadEvent;
    [SerializeField] private VoidEventChannel bossClearEvent;

    [Header("레벨업 시 띄울 오브젝트")]
    [SerializeField] private GameObject levelUpButton;

    public int KillCount { get; private set; }
    public int Gold { get; private set; }

    public event Action<int> OnKillEnemy;
    public event Action<int> OnGoldChanged;
    public event Action<int, int> OnExpChanged;

    // 레벨
    private int level;
    private int[] requireExp = new int[100];
    private int currentExp;

    public int Level => level;
    public int CurrentExp => currentExp;
    public int[] RequireExp => requireExp;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        level = 0;
        currentExp = 0;
        KillCount = 0;
        Gold = 0;
        SetNeedExp();
    }

    private void OnEnable()
    {
        if (playerDeadEvent != null)
        {
            playerDeadEvent.OnEventRaised += OnPlayerDead;
        }

        if (bossClearEvent != null)
        {
            bossClearEvent.OnEventRaised += OnBossDead;
        }
    }

    private void OnDisable()
    {
        if (playerDeadEvent != null)
        {
            playerDeadEvent.OnEventRaised -= OnPlayerDead;
        }

        if (bossClearEvent != null)
        {
            bossClearEvent.OnEventRaised -= OnBossDead;
        }
    }

    public void SetNeedExp()
    {
        for (int i = 0; i < requireExp.Length; i++) 
        { 
            if(i == 0)
            {
                requireExp[i] = 0;
            }
            else if(i == 1)
            {
                requireExp[i] = 25;
            }
            else if (i <= 20)
            {
                requireExp[i] = 25 + (i - 1) * 25;
            }
            else if (i <= 40)
            {
                requireExp[i] = 900 + (i - 20) * 36;
            }
            else if (i <= 60)
            {
                requireExp[i] = 2000 + (i - 40) * 47;
            }
            else if (i <= 80)
            {
                requireExp[i] = 3500 + (i - 60) * 58;
            }
            else if (i <= 100)
            {
                requireExp[i] = 6000 + (i - 80) * 69;
            }
        }
    }
    public void AddKillCount()
    {
        KillCount++;
        OnKillEnemy?.Invoke(KillCount);
    }

    public int GetKillCount()
    {
        return KillCount;
    }

    public void AddGold()
    {
        Gold++;
        OnGoldChanged?.Invoke(Gold);
    }

    public void AddExp(int amount)
    {
        currentExp += amount;
        CheckLevelUp();
        OnExpChanged?.Invoke(currentExp, requireExp[level]);
    }

    public void CheckLevelUp()
    {
        if (currentExp < 0) return;

        while (currentExp >= requireExp[level])
        {
            PauseGame();

            // 최고레벨 도달 시 더이상 레벨업 X
            if (level >= requireExp.Length)
            {
                // 현재, 최대 EXP를 레벨업에 필요한 수치로 전달해 갱신
                OnExpChanged?.Invoke(requireExp[level], requireExp[level]);
                level = requireExp.Length - 1;
                break;
            }

            // 레벨업 시 현재 경험치를 필요 경험치만큼 삭감
            currentExp -= requireExp[level];
            level++;
            levelUpButton.SetActive(true);

            // TODO:레벨업 효과 이벤트
        }
    }

    public void OnPlayerDead()
    {
        GameOver();
    }

    public void GameOver()
    {
        PauseGame();
    }

    public void OnBossDead()
    {
        StageClear();
    }

    public void StageClear()
    {
        PauseGame();
    }

    public void PauseGame()
    {
        Time.timeScale = 0.0f;
    }

    public void ResumeGame()
    {
        Time.timeScale = 1.0f;
    }
}
