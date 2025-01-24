using UnityEngine;

public class ItemFunction : MonoBehaviour // ������ �׼� ���� Ŭ����
{
    #region ���� �ֵθ���
    public float weaponDamage, weaponRange;
    public void UseWeapon(PlayerInfo playerInfo)
    {
        playerInfo.playerAttacker.Attack(weaponDamage, weaponRange);
    }
    #endregion

    #region ���� ��ġ
    public void ActivateTrap(PlayerInfo playerInfo)
    {
        BearTrap trap = playerInfo.playerItemHandler.GetCurrentItem() as BearTrap;

        trap?.ActivateTrap();
    }
    #endregion

    #region ��ĳ�� ����

    [SerializeField] float scannerScanRange = 40f;

    public void ScanArea(PlayerInfo playerInfo)
    {
        playerInfo.playerScanner.Scan(scannerScanRange);
    }
    #endregion

    #region ��ȭ �Ź� ����
    public void EquipEnhancedShoes(PlayerInfo playerInfo)
    {
        Debug.Log("��ȭ �Ź� ����");

        playerInfo.playerController.EquipEnhancedShoes();
    }
    #endregion

    #region ��ȭ ���ֺ� ����
    public void EquipEnhancedSuit(PlayerInfo playerInfo)
    {
        Debug.Log("��ȭ ���ֺ� ����");

        playerInfo.playerSpaceSuit.EquipEnhancedSuit();
    }
    #endregion
}
