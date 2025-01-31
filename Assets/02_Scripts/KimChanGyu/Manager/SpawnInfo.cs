using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

[CreateAssetMenu(fileName = "SpawnInfo", menuName = "Spawn/SpawnInfo")]
public class SpawnInfo : ScriptableObject
{
    // ������ų ����
    public Monster spawnMonster;

    [SerializeField] private List<SpawnTimerData> spawnTimerDataList;

    // ���� ���� ��ü ��
    // NonSerialized�� ���� ScriptableObject�� ����Ǵ� ���� ����
    // �� �÷��̸��� 0���� �ʱ�ȭ�� �Ǿ��ֱ⸦ ����
    [NonSerialized] int currentMonsterCount = 0;
    public bool IsSpawnable(float currentTime)
    {
        return false;

        // InGameTime ���� 1�� ������ ������ �ν����Ϳ��� ������ ���� �ð��� ���� ���� ���ǻ� 1�ð� ������ �ֱ� ����
        // currentTime ���� 3600���� ������
        currentTime /= 3600;

        // ��� SpawnTimerData�� Ȯ���ؼ� 
        foreach (SpawnTimerData spawnTimerData in spawnTimerDataList)
        {
            // ���� Ȯ���� �޾ƿ´�
            float spawnChance = spawnTimerData.GetSpawnChance(currentTime, currentMonsterCount);

            // ���� Ȯ���� ������ ��
            if (spawnChance > 0f)
            {
                // ������ �����ߴٸ�
                if (spawnChance > Random.Range(0f, 100f))
                {
                    return true;
                }

                // ������ �����ߴٸ�
                else
                {
                    return false;
                }
            }
        }

        return false;
    }
}

[Serializable]
public struct SpawnTimerData
{
    public float targetTime; // ���� �ð�
    public float spawnRate;

    public int monsterSpawnLimit; // ���� �ð� ���ķ� ���� ������ ��ü ��

    // ���� �ð��� ������ ���� ���� ������ ������ �ð��̶�� ���� Ȯ���� ��ȯ�ϰ�,
    // ������ �������� �ʴٸ� 0�� ��ȯ�Ѵ�
    public float GetSpawnChance(float currentTime, int currentMonsterCount)
    {
        // ���� �ð��� ������ �Ǵ� targetTime �ð����� ������ ��쿡 0�� ��ȯ�Ѵ�
        if (targetTime > currentTime) return 0f;

        // ���� ������ ������ ���� �ִ� ���������� ������ Ŭ ��쿡 0�� ��ȯ�Ѵ�
        if (monsterSpawnLimit <= currentMonsterCount) return 0f;

        // ��� ��츦 ����߱� ������ ���� Ȯ���� ��ȯ�Ѵ�
        return spawnRate;
    }
}
