using UnityEngine;

public class ItemFunction : MonoBehaviour // ������ �׼� ���� Ŭ����
{
    #region ���� �ֵθ���
    public void UseWeapon(PlayerInfo playerInfo)
    {

    }
    #endregion

    #region ���� ��ġ
    public void DropTrap(PlayerInfo playerInfo)
    {

    }
    #endregion

    #region ��ĳ�� ����
    public void ScanArea(PlayerInfo playerInfo)
    {

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
