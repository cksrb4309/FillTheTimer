using System;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "ItemData", menuName = "Item/ItemData")]
public class ItemData : ScriptableObject // Item Ŭ������ ���� ���� ����
{
    public string itemName; // ������ �̸�

    public ItemType itemType = ItemType.Default; // ������ Ÿ��

    public int itemSize = 1; // �κ��丮 ���� ����

    public int itemPrice; // ������ ����

    public float itemWeight; // ������ ���� TODO : ������ ���� ���� (����)
    public float itemDropSpeed; // ������ �������� �ӵ�

    public Sprite itemIconImage; // ������ ������ �̹���

    public string guideText; // ���� ���̵� �ؽ�Ʈ

    public AnimationParameter equipTrigger = AnimationParameter.HoldItem; // ���� �ִϸ��̼� Ʈ���� �̸�
    public AnimationParameter useTrigger = AnimationParameter.UseItem; // ��� �ִϸ��̼� Ʈ���� �̸�

    public UnityEvent<PlayerInfo> itemUseAction = null; // ������ ��� Action
}
public enum ItemType
{
    Default, // �⺻ ������
    Consumable, // �Ҹ� ������
    NonConsumable, // ��Ҹ� ������
}