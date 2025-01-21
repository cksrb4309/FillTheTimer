using System.Collections;
using UnityEngine;

public class Item : MonoBehaviour, IInteractable
{
    public virtual string TooltipText => itemData.guideText; // ��ȣ�ۿ� UI �ؽ�Ʈ

    public ItemData itemData = null; // ������ ����

    public Transform rayStartPosition;

    new MeshRenderer renderer = null; // ������
    new Collider collider = null; // �ݶ��̴�
    new Rigidbody rigidbody = null; // ��ü

    private void Start()
    {
        // GetComponent�� �ʿ��� Ŭ���� ���� ��������
        collider = GetComponentInChildren<Collider>();
        renderer = GetComponentInChildren<MeshRenderer>();
        rigidbody = GetComponentInChildren<Rigidbody>();
    }
    public void Interact() // ��ȣ�ۿ�
    {
        Debug.Log("D");

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

        // ��ü isKinematic Ȱ��ȭ
        rigidbody.isKinematic = true;
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

        // ��ü isKinematic Ȱ��ȭ
        rigidbody.isKinematic = false;
    }
    public void Activate() // Ȱ��ȭ
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

        // ��ü isKinematic ��Ȱ��ȭ
        rigidbody.isKinematic = false;
    }
    IEnumerator DropItemCoroutine()
    {
        LayerMask layerMask = LayerMask.GetMask("Ground");

        Ray ray = new Ray(rayStartPosition.position, Vector3.down);

        float positionY = transform.position.y;
        Vector3 position = new Vector3(transform.position.x, positionY, transform.position.z);

        if (Physics.Raycast(ray, out RaycastHit hitInfo, float.PositiveInfinity, layerMask))
        {
            float goal = hitInfo.point.y + (rayStartPosition.position.y - transform.position.y);

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
}