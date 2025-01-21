using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ItemSlot : MonoBehaviour
{
    [SerializeField] Image iconImageUI; // ������ ������ �̹���

    Image slotBackgroundUI; // ������ ���� ��� �̹���

    float unEquipAlpha; // ������ ���� ��
    float currAlpha; // ���� Alpha ��
    float equipAlpha; // ���� Alpha ��
    float alphaSpeed; // ���� �� ���� ������ ���� Alpha ���� �ӵ�

    Coroutine equipCoroutine = null; // ���� �ڷ�ƾ
    Coroutine unEquipCoroutine = null; // ���� ���� �ڷ�ƾ

    private void Start()
    {
        slotBackgroundUI = GetComponent<Image>();

        unEquipAlpha = slotBackgroundUI.color.a;
        currAlpha = unEquipAlpha;
        equipAlpha = 0.8f;
        alphaSpeed = 3f;
    }
    public void SetItem(Item item)
    {
        iconImageUI.color = new Color(1, 1, 1, 1);

        iconImageUI.sprite = item.itemData.itemIconImage;
    }
    public void RemoveItem()
    {
        iconImageUI.color = new Color(1, 1, 1, 0);

        iconImageUI.sprite = null;
    }
    public void Equip() // ������ ���� ��
    {
        // ���� ���� ���� �ڷ�ƾ�� �ִٸ� �����Ѵ�
        if (unEquipCoroutine != null)
        {
            StopCoroutine(unEquipCoroutine);
        }
        equipCoroutine = StartCoroutine(EquipCoroutine());
    }
    public void UnEquip() // ������ ���� ���� ��
    {
        // ���� ���� �ڷ�ƾ�� �ִٸ� �����Ѵ�
        if (equipCoroutine != null)
        {
            StopCoroutine(equipCoroutine);
        }
        unEquipCoroutine = StartCoroutine(UnEquipCoroutine());
    }
    IEnumerator EquipCoroutine()
    {
        Color color = slotBackgroundUI.color;
        for (; currAlpha < 0.8f; currAlpha += Time.deltaTime * alphaSpeed)
        {
            color.a = currAlpha;
            slotBackgroundUI.color = color;

            yield return null;
        }
        currAlpha = equipAlpha;
        color.a = currAlpha;
        slotBackgroundUI.color = color;
    }
    IEnumerator UnEquipCoroutine()
    {
        Color color = slotBackgroundUI.color;

        Debug.Log("currAlpha : " + currAlpha.ToString());
        Debug.Log("unEquipAlpha : " + unEquipAlpha.ToString());

        for (; currAlpha > unEquipAlpha; currAlpha -= Time.deltaTime * alphaSpeed)
        {
            color.a = currAlpha;

            slotBackgroundUI.color = color;

            yield return null;
        }
        currAlpha = unEquipAlpha;

        color.a = currAlpha;

        slotBackgroundUI.color = color;
    }
}
