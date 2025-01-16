using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerItemHandler : MonoBehaviour
{
    [SerializeField] InputActionReference scrollWheelInputAction; // ��ũ�� �Է�
    [SerializeField] InputActionReference itemUseInputAction; // ������ ��� �Է�
    [SerializeField] InputActionReference itemDropInputAction; // ������ ��� �Է�

    [SerializeField] InputActionReference itemSelectHotkey1; // ������ ���� ����Ű 1
    [SerializeField] InputActionReference itemSelectHotkey2; // ������ ���� ����Ű 2
    [SerializeField] InputActionReference itemSelectHotkey3; // ������ ���� ����Ű 3
    [SerializeField] InputActionReference itemSelectHotkey4; // ������ ���� ����Ű 4
    [SerializeField] InputActionReference itemSelectHotkey5; // ������ ���� ����Ű 5

    PlayerAnimator playerAnimator = null; // �÷��̾� �ִϸ�����

    Item selectedItem = null; // ���� ���� ������

    bool isItemUsing = false; // ������ ��� �� Ʈ���� ����

    int currentSelectedIndex = 0; // ���� ���� ���� ������
    int beforeSelectedIndex = 0; // ������ �����ϴ� ������
    int maxSelectedIndex = 5; // ������ �� �ִ� ������ �ִ� �ε���

    string unEquipTriggerName = "UnEquip"; // �������� TriggerName

    private void Start()
    {
        playerAnimator = GetComponent<PlayerAnimator>();
    }
    private void OnEnable()
    {
        scrollWheelInputAction.action.Enable();
        itemUseInputAction.action.Enable();
        itemDropInputAction.action.Enable();

        itemSelectHotkey1.action.Enable();
        itemSelectHotkey2.action.Enable();
        itemSelectHotkey3.action.Enable();
        itemSelectHotkey4.action.Enable();
        itemSelectHotkey5.action.Enable();
    }
    private void OnDisable()
    {
        scrollWheelInputAction.action.Disable();
        itemUseInputAction.action.Disable();
        itemDropInputAction.action.Disable();

        itemSelectHotkey1.action.Disable();
        itemSelectHotkey2.action.Disable();
        itemSelectHotkey3.action.Disable();
        itemSelectHotkey4.action.Disable();
        itemSelectHotkey5.action.Disable();
    }
    public void Update()
    {
        #region ������ ��� �Է�

        // ������ ��� �Է��ϰ�, ��� �ִ� �������� ���� ���
        if (itemDropInputAction.action.WasPressedThisFrame() &&
            selectedItem != null)
        {
            // TODO ��� �ִٰ� ���� �����ۿ� ���� ó�� (����)

            // ������ ������Ʈ Ȱ��ȭ !
            selectedItem.Activate();
        }

        #endregion

        #region ������ ��� �Է�

        // ������ ��� ���̶�� ��� �Է��� �ݺ����� �ʵ��� �Լ��� ��ȯ
        if (isItemUsing) return;

        // ������ ��� �Է��ϰ�, �ش� �������� Default Ÿ���� �ƴ� ���
        if (itemUseInputAction.action.WasPressedThisFrame() &&
            selectedItem.itemData.itemType != ItemType.Default)
        {
            // ������ ��� �� Ʈ���� ���� Ȱ��ȭ
            isItemUsing = true;

            // �����ۿ� ���� ��� �ִϸ��̼� Ʈ���� ���
            playerAnimator.SetItemUseTrigger(selectedItem == null ? unEquipTriggerName : selectedItem.itemData.equipTriggerName);
        }

        #endregion

        #region ������ ���� �Է�

        // �������� ��� ���̶�� �Լ��� ��ȯ�Ͽ�
        // ������ ������ ���Ƴ��´�
        if (isItemUsing) return;

        // ���콺 �� ��ũ�� �Է� �ޱ�
        Vector2 scrollDelta = scrollWheelInputAction.action.ReadValue<Vector2>();

        // �Էµ� ���� ���ٸ�
        if (Mathf.Abs(scrollDelta.y) <= float.Epsilon) return;

        // ���� ���� �޾��� ��
        if (scrollDelta.y > 0)
        {
            currentSelectedIndex = (currentSelectedIndex + 1) > maxSelectedIndex ? 0 : currentSelectedIndex + 1;
        }
        // ���� ���� �޾��� ��
        else
        {
            currentSelectedIndex = (currentSelectedIndex - 1) < 0 ? maxSelectedIndex - 1 : currentSelectedIndex - 1;
        }

        // ���� ����Ű Index ����
        int pressedHotkeyIndex = -1;

        // ������ ���� ����Ű �Է��� Ȯ���Ѵ�
        if (itemSelectHotkey1.action.WasPressedThisFrame()) pressedHotkeyIndex = 1;
        if (itemSelectHotkey2.action.WasPressedThisFrame()) pressedHotkeyIndex = 2;
        if (itemSelectHotkey3.action.WasPressedThisFrame()) pressedHotkeyIndex = 3;
        if (itemSelectHotkey4.action.WasPressedThisFrame()) pressedHotkeyIndex = 4;
        if (itemSelectHotkey5.action.WasPressedThisFrame()) pressedHotkeyIndex = 5;

        // ���� ��ư�� �ִٸ�
        if (pressedHotkeyIndex != -1) currentSelectedIndex = pressedHotkeyIndex;

        // ���� ���� ���� �ε����� �޶����� ��
        if (beforeSelectedIndex != currentSelectedIndex)
        {
            // ���� ������ Null�� �ƴ� ���տ��� ������
            selectedItem?.DisableInHand();

            // ����
            beforeSelectedIndex = currentSelectedIndex;

            // ������ ����
            EquipItem();
        }

        #endregion
    }
    void EquipItem() // ������ ����
    {
        // ���� Index�� �������� �����´�
        Item targetItem = PlayerInventory.Instance.GetItemFromInventory(currentSelectedIndex);

        // ���� ���� ������ �����۰� �ٸ� ���
        if (selectedItem != targetItem)
        {
            // �����ۿ� ���� �� �ִϸ��̼� Ʈ���� ���
            playerAnimator.SetItemChangeTrigger(selectedItem == null ? unEquipTriggerName : selectedItem.itemData.equipTriggerName);
        }
        // ���� ���� ������ �����۰� ���� ��쿡�� ������ ���� �� ����ϴ�
        // �������̶� ���� ������ ���� ���� ������ ���� �ʿ䰡 ����
    }
    void OnItemUseComplete() // ������ ��� �Ϸ� �Լ�
    {
        // ��� ������ ���� ���·� ��ȯ
        isItemUsing = false;

        // ������ �������� ��� �� ����� ȣ���Ѵ�
        selectedItem.itemData.itemUseAction.Invoke(true);

        // ���� �������� ��� �������� ���
        if (selectedItem.itemData.itemType == ItemType.Consumable) 
        {
            // ���� �ε����� ������ ����
            PlayerInventory.Instance.RemoveItem(currentSelectedIndex);
        }
    }
}
