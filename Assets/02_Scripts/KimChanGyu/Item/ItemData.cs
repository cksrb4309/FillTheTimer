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

    public float itemWeight; // ������ ����

    public Sprite itemIconImage; // ������ ������ �̹���

    public string equipTriggerName = string.Empty; // ���� �ִϸ��̼� Ʈ���� �̸�
    public string useTriggerName = string.Empty; // ��� �ִϸ��̼� Ʈ���� �̸�

    public UnityEvent<bool> itemUseAction = null; // ������ ��� Action
}

public enum ItemType
{
    Default, // �⺻ ������
    Consumable, // �Ҹ� ������
    NonConsumable, // ��Ҹ� ������
}