using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerItemHandler : MonoBehaviour
{
    static PlayerItemHandler instance = null;
    public static PlayerItemHandler Instance
    {
        get { return instance; }
    }

    [SerializeField] Transform rightHand;

    [SerializeField] InputActionReference scrollWheelInputAction; // ��ũ�� �Է�
    [SerializeField] InputActionReference itemUseInputAction; // ������ ��� �Է�
    [SerializeField] InputActionReference itemDropInputAction; // ������ ��� �Է�

    [SerializeField] InputActionReference itemSelectHotkey1; // ������ ���� ����Ű 1
    [SerializeField] InputActionReference itemSelectHotkey2; // ������ ���� ����Ű 2
    [SerializeField] InputActionReference itemSelectHotkey3; // ������ ���� ����Ű 3
    [SerializeField] InputActionReference itemSelectHotkey4; // ������ ���� ����Ű 4
    [SerializeField] InputActionReference itemSelectHotkey5; // ������ ���� ����Ű 5

    PlayerInfo playerInfo;

    PlayerAnimator playerAnimator = null; // �÷��̾� �ִϸ�����

    Item selectedItem = null; // ���� ���� ������

    bool isItemUsing = false; // ������ ��� �� Ʈ���� ����

    int currentSelectedIndex = 0; // ���� ���� ���� ������
    int beforeSelectedIndex = 0; // ������ �����ϴ� ������
    int maxSelectedIndex = 5; // ������ �� �ִ� ������ �ִ� �ε���

    string unEquipTriggerName = "UnEquip"; // �������� TriggerName
    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        playerAnimator = GetComponent<PlayerAnimator>();

        InventoryUI.Instance.ItemSlotEquipSetting(0);

        playerInfo = new PlayerInfo(
            GetComponent<PlayerSpaceSuit>(),
            GetComponent<PlayerController>(),
            GetComponent<PlayerScanner>(),
            this);
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
            // �κ��丮���� ����
            PlayerInventory.Instance.RemoveItem(currentSelectedIndex);

            // ������ ������Ʈ Ȱ��ȭ !
            selectedItem.Activate();

            // ������ ���� ����
            selectedItem = null;
        }

        #endregion

        #region ������ ��� �Է�

        // ������ ��� ���̶�� ��� �Է��� �ݺ����� �ʵ��� �Լ��� ��ȯ
        if (isItemUsing) return;

        // ������ ��� �Է��ϰ�, �ش� �������� Default Ÿ���� �ƴ� ���
        if (itemUseInputAction.action.WasPressedThisFrame() &&
            selectedItem != null &&
            selectedItem.itemData.itemType != ItemType.Default)
        {
            // ������ ��� �� Ʈ���� ���� Ȱ��ȭ
            isItemUsing = true;

            // �����ۿ� ���� ��� �ִϸ��̼� Ʈ���� ���
            playerAnimator.SetItemUseTrigger(selectedItem == null ? unEquipTriggerName : selectedItem.itemData.equipTriggerName);

            // TODO : ������� (����)
            OnItemUseComplete(); // �ִϸ��̼��� �������� ���⼭ ������ ��� �׽�Ʈ�� �켱������ �����Ѵ�
        }

        #endregion

        #region ������ ���� �Է�

        // �������� ��� ���̶�� �Լ��� ��ȯ�Ͽ�
        // ������ ������ ���Ƴ��´�
        if (isItemUsing) return;

        // ���콺 �� ��ũ�� �Է� �ޱ�
        Vector2 scrollDelta = scrollWheelInputAction.action.ReadValue<Vector2>();

        // �Էµ� ���� �ִٸ�
        if (Mathf.Abs(scrollDelta.y) > float.Epsilon)
        {
            // ���� ���� �޾��� ��
            if (scrollDelta.y < 0)
            {
                currentSelectedIndex = (currentSelectedIndex + 1) >= maxSelectedIndex ? 0 : currentSelectedIndex + 1;
            }
            // ���� ���� �޾��� ��
            else
            {
                currentSelectedIndex = (currentSelectedIndex - 1) < 0 ? maxSelectedIndex - 1 : currentSelectedIndex - 1;
            }
        }

        // ���� ����Ű Index ����
        int pressedHotkeyIndex = -1;

        // ������ ���� ����Ű �Է��� Ȯ���Ѵ�
        if (itemSelectHotkey1.action.WasPressedThisFrame()) pressedHotkeyIndex = 0;
        if (itemSelectHotkey2.action.WasPressedThisFrame()) pressedHotkeyIndex = 1;
        if (itemSelectHotkey3.action.WasPressedThisFrame()) pressedHotkeyIndex = 2;
        if (itemSelectHotkey4.action.WasPressedThisFrame()) pressedHotkeyIndex = 3;
        if (itemSelectHotkey5.action.WasPressedThisFrame()) pressedHotkeyIndex = 4;

        // ���� ��ư�� �ִٸ�
        if (pressedHotkeyIndex != -1) currentSelectedIndex = pressedHotkeyIndex;

        // ���� ���� ���� �ε����� �޶����� ��
        if (beforeSelectedIndex != currentSelectedIndex)
        {
            InventoryUI.Instance.ItemSlotUnEquipSetting(beforeSelectedIndex);

            beforeSelectedIndex = currentSelectedIndex;

            InventoryUI.Instance.ItemSlotEquipSetting(currentSelectedIndex);

            // ������ ����
            EquipItem();
        }

        #endregion
    }
    public Transform GetHandTransform() => rightHand;
    public Item GetCurrentItem() => selectedItem;
    public void EquipItem() // ������ ����
    {
        // ���� Index�� �������� �����´�
        Item targetItem = PlayerInventory.Instance.GetItemFromInventory(currentSelectedIndex);

        // ���� ������ ������ �ƴ� ���
        if (selectedItem == null ||
            targetItem == null ||
            selectedItem.GetInstanceID() != targetItem.GetInstanceID())
        {
            // �����ۿ� ���� �� �ִϸ��̼� Ʈ���� ���
            playerAnimator.SetItemChangeTrigger(targetItem == null ? unEquipTriggerName : targetItem.itemData.equipTriggerName);

            selectedItem?.DisableInHand();

            selectedItem = targetItem;

            selectedItem?.EnableInHand(rightHand);
        }
    }
    void OnItemUseComplete() // ������ ��� �Ϸ� �Լ�
    {
        // ��� ������ ���� ���·� ��ȯ
        isItemUsing = false;

        // ������ �������� ��� �� ����� ȣ���Ѵ�
        selectedItem.itemData.itemUseAction.Invoke(playerInfo);

        // ���� �������� ��� �������� ���
        if (selectedItem.itemData.itemType == ItemType.Consumable)
        {
            // ���� �ε����� ������ ����
            PlayerInventory.Instance.RemoveItem(currentSelectedIndex);
        }
    }
}
