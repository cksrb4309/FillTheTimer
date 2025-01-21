using UnityEngine;

public class ItemFunction : MonoBehaviour // ������ �׼� ���� Ŭ����
{
    #region ���� �ֵθ���
    public void UseWeapon(PlayerInfo playerInfo)
    {

    }
    #endregion

    #region ���� ��ġ
    public void ActivateTrap(PlayerInfo playerInfo)
    {
        BearTrap trap = playerInfo.playerItemHandler.GetCurrentItem() as BearTrap;

        trap?.Activate();
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
        playerInfo.playerController.EquipEnhancedShoes();
    }
    #endregion

    #region ��ȭ ���ֺ� ����
    public void EquipEnhancedSuit(PlayerInfo playerInfo)
    {
        playerInfo.playerSpaceSuit.EquipEnhancedSuit();
    }
    #endregion

}
