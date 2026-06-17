using UnityEngine;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
   public static GameManager instance { get; private set; }

    [System.Serializable]
    public struct StageSetup
    {
        public string stageNaem;
        public List<WaveData> thisStageWaveData;

        
    }
    [SerializeField] private List<StageSetup> allStages;
    private int currentStageIndex = 0;

    public bool isInShop = false;

    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        StartCurrentStage();
    }
    private void StartCurrentStage()
    {
        if (currentStageIndex >= allStages.Count) return;
        isInShop = false;
        List<WaveData> currentStageWaves = allStages[currentStageIndex].thisStageWaveData;
        WaveManager.Instance.SetupStageWaves(currentStageWaves);
        WaveManager.Instance.StartStage();
    }

    public void OnStageClear()
    {
        isInShop = false;

    }

    public void CloseShopToNextStage()
    {
        if (!isInShop) return;

        isInShop = false;

        currentStageIndex++;

        StartCurrentStage();
    }
}
