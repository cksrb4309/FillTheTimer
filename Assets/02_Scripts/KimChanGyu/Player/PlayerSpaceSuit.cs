﻿using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;

public class PlayerSpaceSuit : MonoBehaviour, IDamagable
{
    // 일반 우주복의 산소 소모량
    [SerializeField] float minOxygenDrain = 0.05f;
    [SerializeField] float maxOxygenDrain = 3f;

    // 강화 우주복의 산소 소모량
    [SerializeField] float minExSuitOxygenDrain = 0.025f;
    [SerializeField] float maxExSuitOxygenDrain = 2f;

    [SerializeField] float maxHp = 3f;

    [SerializeField] Volume volume;

    [SerializeField] Image scratchImage;

    private Bloom bloom = null;

    PlayerOxygen playerOxygen = null;

    Coroutine damagedCoroutine = null;

    float currHp = 0;
    float Hp
    {
        get
        {
            return currHp;
        }
        set
        {
            currHp = value;

            if (currHp < 0) currHp = 0;

            PlayerStateUI.Instance.SetSuitSpaceFillImage(currHp > 0 ? currHp / maxHp : 0);
        }
    }
    private void Start()
    {
        currHp = maxHp;

        playerOxygen = GetComponent<PlayerOxygen>();

        playerOxygen.SetOxygenDecreaseValue(minOxygenDrain);

        if (!volume.profile.TryGet(out bloom))
        {
            Debug.LogWarning("Bloom 효과를 찾을 수 없습니다.");
        }

        bloom.tint.Override(Color.white);
    }
    public void Damaged(float damage)
    {
        if (currHp <= 0) return;

        // 체력 감소
        Hp -= damage;

        ScreenEffectController.TriggerScreenNoiseEffect(2f, 0.2f, 1f);

        if (damagedCoroutine != null) StopCoroutine(damagedCoroutine);

        damagedCoroutine = StartCoroutine(DamagedCoroutine());

        SetOxygenDecreaseValue();
    }
    public void EquipEnhancedSuit()
    {
        minOxygenDrain = minExSuitOxygenDrain;
        maxOxygenDrain = maxExSuitOxygenDrain;

        SetOxygenDecreaseValue();
    }
    void SetOxygenDecreaseValue() =>
        playerOxygen.SetOxygenDecreaseValue(Mathf.Lerp(minOxygenDrain, maxOxygenDrain, Mathf.Pow(1 - (currHp / maxHp), 2f)));
    IEnumerator DamagedCoroutine()
    {
        bloom.tint.Override(Color.red);

        scratchImage.enabled = true;

        yield return new WaitForSeconds(1f);

        float t = 1;

        Color imageColor = Color.white;

        while (t > 0f)
        {
            t -= Time.deltaTime;

            bloom.tint.Override(Color.Lerp(Color.white, Color.red, t));

            imageColor.a = t;

            scratchImage.color = imageColor;

            yield return null;
        }

        bloom.tint.Override(Color.white);

        imageColor.a = 1f;

        scratchImage.enabled = false;

        scratchImage.color = imageColor;
    }
}
