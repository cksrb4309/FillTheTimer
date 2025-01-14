using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    Animator playerAnimator; // �÷��̾� �ִϸ�����

    bool isGround = true; // ���� isGround ����

    private void Start()
    {
        // Animator ��������
        playerAnimator = GetComponentInChildren<Animator>();
    }
    void SetMoveSpeed(float value) // �̵��ӵ� �� ����
    {
        playerAnimator.SetFloat("Move", value);
    }
    public void SetIsCrouch(bool isCrouch) // ���� ���� ���� ����
    {
        playerAnimator.SetBool("IsCrouch", isCrouch);
    }
    public bool SetJumpTrigger() // ���� �ִ��� �Ұ����ϸ� False, �����ϸ� True ��ȯ �� �ִϸ��̼� ����
    {
        // ���� �ޱ����� ���� ��
        if (playerAnimator.GetCurrentAnimatorStateInfo(0).IsName("CrouchMove"))
        {
            // false�� ��ȯ
            return false;
        }
        // ���� ���ִٸ� Jump Trigger ����
        playerAnimator.SetTrigger("Jump");

        // true�� ��ȯ
        return true;
    }
    public void SetIsGround(bool isGround) // IsGround ���� ����
    {
        // ���� ���¿� �����ϴٸ� ��ȯ
        if (this.isGround == isGround) return;

        // ���� ���¿� ����
        this.isGround = isGround;

        // IsGround ����
        playerAnimator.SetBool("IsGround", isGround);
    }
    private void OnEnable()
    {
        // �Լ� ���� ���ѳ���
        GetComponent<PlayerController>().BindToPlayerAnimator(SetMoveSpeed);
    }
    private void OnDisable()
    {
        // �Լ� ���� �����ϱ�
        GetComponent<PlayerController>().UnbindFromPlayerAnimator(SetMoveSpeed);
    }
}