using System.Collections;
using UnityEngine;

public class BearTrap : Item
{
    public override string TooltipText => !isSet ? "Get(E)" : isBeingSet ? "install(E)" : "Danger";

    public MeshRenderer meshRenderer;

    Material material;

    BearTrapHelper helper = null;

    int needInstallCount = 3;
    int currInstallCount = 0;

    bool isSet = false; // ��ġ�Ǿ������� ���� Ʈ���� ����
    bool isUsed = false;
    bool isBeingSet = false;

    private void Awake()
    {
        material = meshRenderer.material;

        material.SetFloat("_TrapOpenAmount", 0.5f);

        helper = GetComponentInChildren<BearTrapHelper>();
    }
    public void ActivateTrap()
    {
        // �θ� ����
        transform.parent = null;

        StartCoroutine(DropItemCoroutine());

        // ���� ���� ���� ȸ���� �ʱ�ȭ
        transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, 0);

        // �浹ü Ȱ��ȭ
        collider.enabled = true;

        if (!isUsed)
        {
            isSet = true;

            isBeingSet = true;

            material.SetFloat("_TrapOpenAmount", 1f);
        }
    }
    public void CompleteAttack()
    {
        isUsed = true;

        material.SetFloat("_TrapOpenAmount", 0f);
    }
    public override void Interact()
    {
        if (IsGetItem())
        {
            // �κ��丮�� ������ ����
            PlayerInventory.Instance.AddItemToInventory(this);

            PlayerItemHandler.Instance.EquipItem();
        }
        else if (isBeingSet)
        {
            currInstallCount++;

            material.SetFloat("_TrapOpenAmount", 1f - (needInstallCount / currInstallCount));

            if (currInstallCount == needInstallCount)
            {
                isBeingSet = false;

                helper.ActivateHelper();
            }
        }
    }
    private void OnEnable()
    {
        material.SetFloat("_TrapOpenAmount", 0.5f);

        helper.DeactivateHelper();

        currInstallCount = 0;

        isUsed = false;
        isBeingSet = false;
        isSet = false;
    }
    public override bool IsGetItem() => !isBeingSet && !isSet;
}
