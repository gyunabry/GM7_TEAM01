using System.Collections;
using Unity.Cinemachine;
using UnityEngine;

public class CameraTrasition : MonoBehaviour
{
    [Header("구독할 이벤트")]
    [SerializeField] private VoidEventChannel bossEncounterEvent;
    [SerializeField] private VoidEventChannel bossDeadEvent;

    [Header("시네머신 카메라")]
    [SerializeField] private CinemachineCamera playerCamera;
    [SerializeField] private CinemachineCamera bossCamera;

    [Header("보스 스폰 위치")]
    [SerializeField] private Transform bossSpawnPoint;

    [Header("연출 시간 설정")]
    [SerializeField] private float spawnDuration = 3f;
    [SerializeField] private float deathDuration = 3f;

    private void Start()
    {
        if (bossEncounterEvent != null)
        {
            bossEncounterEvent.OnEventRaised += PlayBossSpawn;
        }
        if (bossDeadEvent != null)
        {
            bossDeadEvent.OnEventRaised += PlayBossDead;
        }
    }

    private void OnDisable()
    {
        if (bossEncounterEvent != null)
        {
            bossEncounterEvent.OnEventRaised -= PlayBossSpawn;
        }
        if (bossDeadEvent != null)
        {
            bossDeadEvent.OnEventRaised -= PlayBossDead;
        }
    }

    private void PlayBossSpawn()
    {
        StartCoroutine(BossSpawnCameraCo());
    }

    private IEnumerator BossSpawnCameraCo()
    {
        if (bossCamera != null)
        {
            // 보스 스폰 위치를 카메라 타겟으로 설정
            bossCamera.Follow = bossSpawnPoint;
        }

        bossCamera.Priority = 20;
        playerCamera.Priority = 10;

        GameManager.Instance.PauseGame();

        // 일시정지 상태이기 때문에 Realtime 사용
        yield return new WaitForSecondsRealtime(spawnDuration);

        bossCamera.Priority = 0;
        GameManager.Instance.ResumeGame();
    }

    private void PlayBossDead()
    {
        StartCoroutine(BossDeadCameraCo());
    }

    private IEnumerator BossDeadCameraCo()
    {
        BossController boss = FindFirstObjectByType<BossController>();

        if (bossCamera != null)
        {
            bossCamera.Follow = boss.transform;
        }

        bossCamera.Priority = 20;
        playerCamera.Priority = 10;

        Time.timeScale = 0.3f;

        yield return new WaitForSeconds(deathDuration);

        bossCamera.Priority = 0;
        GameManager.Instance.ResumeGame();
    }
}
