using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    static SpawnManager instance = null;
    public static SpawnManager Instance
    {
        get => instance;
    }

    [SerializeField] List<Transform> spawnPointList;

    [SerializeField] List<SpawnInfo> spawnInfoList;

    // �ֺ��� �÷��̾ �ִ� �� Ȯ���� ���̾��ũ
    LayerMask playerLayerMask;

    private void Awake()
    {
        instance = this;

        playerLayerMask = LayerMask.GetMask("Player");
    }
    public void OnSpawnCheck(float inGameTime)
    {
        foreach (SpawnInfo spawnInfo in spawnInfoList)
        {
            if (spawnInfo.IsSpawnable(inGameTime))
            {
                Spawn(spawnInfo.spawnMonster);
            }
        }
    }
    void Spawn(Monster monster)
    {
        /// TODO : ���� ���� ����
        /// GetRandomSpawnPoint()�� ������ ���� ����Ʈ�� �����ͼ�
        /// �ش� ���� ����Ʈ�� �÷��̾ �ʹ� ������� Ȯ���� ��
        /// ������ �ʴٸ� ���� ���͸� �����ؼ� �ִ� �۾�
    }
    Transform GetRandomSpawnPoint() => spawnPointList[Random.Range(0, spawnPointList.Count)];
}
