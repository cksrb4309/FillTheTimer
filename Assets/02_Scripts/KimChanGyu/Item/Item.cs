using System.Collections;
using UnityEngine;

public class Item : MonoBehaviour, IInteractable
{
    public virtual string TooltipText => itemData.guideText; // ��ȣ�ۿ� UI �ؽ�Ʈ

    public ItemData itemData = null; // ������ ����

    public Transform rayStartPosition;

    protected new MeshRenderer renderer = null; // ������
    protected new Collider collider = null; // �ݶ��̴�

    protected ScanResultDisplay scanResultDisplay = null;

    private void Start()
    {
        // GetComponent�� �ʿ��� Ŭ���� ���� ��������
        collider = GetComponentInChildren<Collider>();
        renderer = GetComponentInChildren<MeshRenderer>();

        scanResultDisplay = GetComponentInChildren<ScanResultDisplay>();
    }
    public virtual void Interact() // ��ȣ�ۿ�
    {
        if (!IsGetItem()) return;

        // �κ��丮�� ������ ����
        PlayerInventory.Instance.AddItemToInventory(this);

        PlayerItemHandler.Instance.EquipItem();
    }
    public void DisableInHand() // ������ �����
    {
        // ���� �����
        renderer.enabled = false;

        // �浹ü ��Ȱ��ȭ
        collider.enabled = false;

        scanResultDisplay.DisableDisplay();
    }
    public void EnableInHand(Transform parent) // ������ �տ� ��� �ִ� ���·� Ȱ��ȭ
    {
        // �θ� ����
        transform.parent = parent;

        transform.position = parent.transform.position;

        transform.rotation = parent.transform.rotation;

        // ���� ���̱�
        renderer.enabled = true;

        // �浹ü ��Ȱ��ȭ
        collider.enabled = false;

        scanResultDisplay.DisableDisplay();
    }
    public virtual void ConsumeItem()
    {
        renderer.enabled = false;
        collider.enabled = false;
        transform.parent = null;

        scanResultDisplay.DisableDisplay();

        //Destroy(gameObject);
    }
    public virtual void Activate() // Ȱ��ȭ
    {
        // �θ� ����
        transform.parent = null;

        StartCoroutine(DropItemCoroutine());

        // ���� ���� ���� ȸ���� �ʱ�ȭ
        transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, 0);

        // ���� ���̱�
        renderer.enabled = true;

        // �浹ü Ȱ��ȭ
        collider.enabled = true;

        scanResultDisplay.EnableDisplay();
    }
    protected IEnumerator DropItemCoroutine()
    {
        LayerMask layerMask = LayerMask.GetMask("Ground");

        Ray ray = new Ray(rayStartPosition.position, Vector3.down);

        if (Physics.Raycast(ray, out RaycastHit hitInfo, 100f, layerMask))
        {
            float goal = hitInfo.point.y + (ray.origin.y - transform.position.y);

            float positionY = transform.position.y;

            Vector3 position = new Vector3(transform.position.x, positionY, transform.position.z);

            while (positionY > goal)
            {
                yield return null;

                positionY -= Time.deltaTime * itemData.itemDropSpeed;

                position.y = positionY;

                transform.position = position;
            }
            position.y = goal;

            transform.position = position;
        }
    }
    public virtual bool IsGetItem() => true;
}