﻿using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using static UnityEngine.GraphicsBuffer;

public class M_Golem : Monster, IMonsterSight
{
    protected override void Start()
    {
        base.Start();
        StartCoroutine(CoFindTarget()); // 플레이어 탐지는 항상 한다(while(true))
    }

    IEnumerator CoFindTarget()
    {
        while (true)
        {
            target = ((IMonsterSight)this).FindTargetInSight();
            yield return new WaitForSeconds(0.1f);  // N초마다 타겟을 탐색
        }
    }

    /// <summary>
    /// 1. OverlapSphere로 범위내의 모든 콜라이더를 탐지한다.
    /// 2. 각 콜라이더방향으로 Ray를 쏴서 Player인지 확인한다.
    /// </summary>
    GameObject IMonsterSight.FindTargetInSight()
    {
        Collider[] hitCollidersInMaxSight = Physics.OverlapSphere(transform.position, stat.maxSightRange); // 최대 범위 내의 hit되는 콜라이더를 모두 탐색한다.

        foreach (Collider hitCollider in hitCollidersInMaxSight)
        {
            if (hitCollider.CompareTag("Player"))
            {
                // 벽 따위의 장애물을 고려해서 레이로 다시 체크해준다.
                // TODO 턱살에 걸려서 타게팅 못하는 중. 헤드 사이즈를 재조정해야할 듯. 빈 오브젝트 하나 만들던지
                Vector3 directionToTarget = hitCollider.transform.position - headSight.transform.position; // 몬스터와 플레이어의 방향 벡터
                float distanceToTarget = Vector3.Distance(headSight.transform.position, hitCollider.transform.position); // 거리 계산

                // 레이 쏴서 플레이어 아니면 Continue
                if (Physics.Raycast(headSight.transform.position, directionToTarget.normalized, out RaycastHit hitInfo, distanceToTarget))
                {                    
                    if (!hitInfo.collider.CompareTag("Player"))
                    {
                        continue;  
                    }
                }

                // 이미 타겟팅이 됐을 때는 최대 탐지 범위 내에서 찾아 준다.
                if (target != null && distanceToTarget < stat.maxSightRange)
                {
                    return hitCollider.gameObject;
                }

                // 최소 탐지 범위 내에서는 플레이어를 항상 찾는다.
                if (distanceToTarget < stat.minSightRange)
                {
                    return hitCollider.gameObject;
                }

                // 최대 탐지 거리 안이고 시야각 내에서 플레이어를 찾을 때
                float angleToPlayer = Vector3.Angle(transform.forward, directionToTarget); // 몬스터와 플레이어의 각도
                if (angleToPlayer <= stat.sightAngle / 2) // 좌, 우 때문에 1/2씩 나눔
                {
                    return hitCollider.gameObject;
                }
            }
        }

        // 여끼까지 왔으면 플레이어를 못 찾았으므로 타겟을 밀어준다.
        return null;
    }

    protected override M_Stat SetStat()
    {
        M_Stat _stat;
        if (MonsterStat.Instance.StatDict.TryGetValue("Golem", out _stat))
        {
            Debug.Log(stat.hp);
        }
        else
        {
            Debug.LogWarning($"Golem not found in stat Dictionary");
        }
        return _stat;
    }
}
