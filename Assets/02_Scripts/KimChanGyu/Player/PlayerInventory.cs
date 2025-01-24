using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    static PlayerInventory instance = null;
    public static PlayerInventory Instance
    {
        get 
        {
            return instance;
        }
    }

    public NotificationTextUI notificationTextUI;

    // ������ ���� ������
    Item[] items = new Item[5] {
        null, null, null, null, null
    }; 

    // ������ ���Գ����� ���� ����Ʈ
    List<int>[] linkedItemSlots = new List<int>[5]{ 
        new List<int>(), new List<int>(), new List<int>(), new List<int>(), new List<int>()
    }; 

    int itemCount = 0; // ���� ������ �ִ� �������� ��
    int itemMaxCount = 5; // �ִ� ���� �� �ִ� �������� ��

    public void AddItemToInventory(Item item) // ������ �߰� �Լ�
    {
        // ���� �κ��丮�� ������ ������ ��
        if (itemCount > itemMaxCount - item.itemData.itemSize)
        {
            // UIManager�� �������� ���Դ´ٴ� ��ȣ�� ����
            notificationTextUI.NotificationText("�κ��丮�� �ڸ��� �����ϴ�");

            return; // ��ȯ
        }

        // �������� �߰��� �� �ִ� �� Ȯ���Ѵ�
        int index = -1;

        // �������� �߰��� �� ���� ������ �ݺ�
        int tmp = 0; // ���� �ݺ� ����
        while (tmp++ < 500) 
        {
            // �������� �߰� ���Ѵٸ� -1, �� �� �ִٸ� �ش� �ε���
            index = CanAddItem(item.itemData.itemSize);

            // �������� �߰��� �� �ִٸ� while �ݺ� ����
            if (index != -1) break;

            // �߰��� �� ���ٸ� �ڷ� �ű� �� �ִ� �� �߿� ���� �տ� �ִ� �������� ��ĭ �̵�
            for (int i = 0; i < itemMaxCount - 1; i++)
            {
                // ���� ���� �ִ� items[i]�� �������� �����ϰ�,
                // ���� ������ ĭ�� ������� ��
                if (items[i] != null && items[i + 1] == null) 
                {
                    // ������ ��ġ �̵�
                    items[i + 1] = items[i];
                    items[i] = null;

                    // ������ ���� ���� �̵�
                    linkedItemSlots[i + 1] = linkedItemSlots[i];
                    linkedItemSlots[i] = new List<int>();

                    break; // ��ĭ �̵� ���״ٸ� for �ݺ��� �ߴ� !
                }
            }
        }

        #region index �� ���� üũ�뵵
        if (index == -1)
        {
            Debug.LogWarning("Index�� -1? �� �κ��丮 Ȯ����.");
            return;
        }
        #endregion

        // ������ ���� ��ġ�� �Ѵ�
        for (int i = index; i < index + item.itemData.itemSize; i++)
        {
            // ������ �迭�� �ִ´�
            items[i] = item;

            // ������ ���� ���� ����Ʈ ����
            for (int j = index; j < index + item.itemData.itemSize; j++)
            {
                // ���� ��ġ�ϴ� �����۰� �ٸ��ٸ�
                if (i != j)
                {
                    // ������ ���� ���� ����Ʈ�� �߰��Ѵ�
                    linkedItemSlots[i].Add(j);
                }
            }
        }

        // ���� �ִ� �������� ���� �������� ũ�⸸ŭ �ø���
        itemCount += item.itemData.itemSize;

        // ������ ������ ������Ʈ �Ѵ�
        UpdateItemSlots();

        // ���������� �������� �����
        item.DisableInHand();
    }
    public Item GetItemFromInventory(int index) // ������ ���� �Լ�
    {
        return items[index];
    }
    public void RemoveItem(int index)
    {
        items[index] = null; // ���� �ε��� null ��ȯ
        InventoryUI.Instance.RemoveItem(index); // ������ ����

        itemCount--;

        // �ش� �ε����� ����� ���Ը�ŭ �ݺ�
        for (int i = 0; i < linkedItemSlots[index].Count; i++)
        {
            items[linkedItemSlots[index][i]] = null; // ����� ������ ������ ����

            linkedItemSlots[linkedItemSlots[index][i]].Clear();

            InventoryUI.Instance.RemoveItem(linkedItemSlots[index][i]); // ���� ����

            itemCount--;
        }

        linkedItemSlots[index].Clear();
    }
    int CanAddItem(int itemSize)
    {
        // �������� �ּ� �� �� �ִ� ��ġ��ŭ �ݺ�
        for (int index = 0; index <= itemMaxCount - itemSize; index++)
        {
            for (int j = 0; j < itemSize; j++)
            {
                // ���� �������� ����ִٸ�
                if (items[index + j] != null)
                {
                    // ���� �ݺ����� ��ŵ�Ѵ�
                    break;
                }
                // ���� �������� ������� �ʰ� ������ �ݺ����̶��
                else if (j == itemSize - 1)
                {
                    return index; // �� �� �ִ� ������ Index�� ��ȯ�Ѵ�
                }
            }
        }

        // ���� �������� ���� �� �ִ� ������ ���ٸ� -1�� ��ȯ�Ѵ�
        return -1;
    }
    void UpdateItemSlots()
    {
        for (int i = 0; i < itemMaxCount; i++)
        {
            if (items[i] != null)
            {
                InventoryUI.Instance.SetItem(i, items[i]);
            }
            else
            {
                InventoryUI.Instance.RemoveItem(i);
            }
        }
    }
    public void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public void OnDisable()
    {
        instance = null;

        Destroy(gameObject);
    }
}
