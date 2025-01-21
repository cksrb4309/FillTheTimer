using UnityEngine;

public class PlayerSpaceSuit : MonoBehaviour, IDamagable
{
    // �Ϲ� ���ֺ��� ��� �Ҹ�
    [SerializeField] float minOxygenDrain = 0.05f;
    [SerializeField] float maxOxygenDrain = 3f;

    // ��ȭ ���ֺ��� ��� �Ҹ�
    [SerializeField] float minExSuitOxygenDrain = 0.025f;
    [SerializeField] float maxExSuitOxygenDrain = 2f;

    [SerializeField] float maxHp = 3f;

    PlayerOxygen playerOxygen = null;

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
    }
    public void Hit(float damage)
    {
        if (currHp <= 0) return;

        // ü�� ����
        Hp -= damage;

        playerOxygen.SetOxygenDecreaseValue(
            Mathf.Lerp(
                minOxygenDrain,
                maxOxygenDrain,
                currHp / maxHp));
    }
    public void EquipEnhancedSuit()
    {
        minOxygenDrain = minExSuitOxygenDrain;
        maxOxygenDrain = maxExSuitOxygenDrain;

        playerOxygen.SetOxygenDecreaseValue(
            Mathf.Lerp(
                minOxygenDrain,
                maxOxygenDrain,
                currHp / maxHp));
    }
}
