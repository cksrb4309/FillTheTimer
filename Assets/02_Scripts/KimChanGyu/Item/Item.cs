using UnityEngine;

public class Item : MonoBehaviour, IInteractable
{
    public virtual string TooltipText => "�ݱ�"; // ��ȣ�ۿ� UI �ؽ�Ʈ

    public ItemData itemData = null; // ������ ����

    new MeshRenderer renderer = null; // ������
    new Collider collider = null; // �ݶ��̴�
    new Rigidbody rigidbody = null; // ��ü

    private void Start()
    {
        // GetComponent�� �ʿ��� Ŭ���� ���� ��������
        collider = GetComponent<Collider>();
        renderer = GetComponent<MeshRenderer>();
        rigidbody = GetComponent<Rigidbody>();
    }
    public void Interact() // ��ȣ�ۿ�
    {
        // �κ��丮�� ������ ����
        PlayerInventory.Instance.AddItemToInventory(this);
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
    public void EnableInHand() // ������ �տ� ��� �ִ� ���·� Ȱ��ȭ
    {
        // ���� ���̱�
        renderer.enabled = true;

        // �浹ü ��Ȱ��ȭ
        collider.enabled = false;

        // ��ü isKinematic Ȱ��ȭ
        rigidbody.isKinematic = false;
    }
    public void Activate() // Ȱ��ȭ
    {
        // ���� ���̱�
        renderer.enabled = true;

        // �浹ü Ȱ��ȭ
        collider.enabled = true;

        // ��ü isKinematic ��Ȱ��ȭ
        rigidbody.isKinematic = false;
    }
}