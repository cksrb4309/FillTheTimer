using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    Animator playerAnimator; // �÷��̾� �ִϸ�����

    PlayerAnimationState currState; // ���� ����

    bool isGround = true;
    private void Start()
    {
        // Animator ��������
        playerAnimator = GetComponentInChildren<Animator>();
    }
    public PlayerAnimationState GetState() // ���� �ִϸ��̼� ���� ��ȯ
    {
        return currState;
    }
    void SetMoveSpeed(float value)
    {
        playerAnimator.SetFloat("Move", value);
    }
    public void SetIsCrouch(bool isCrouch)
    {
        playerAnimator.SetBool("IsCrouch", isCrouch);
    }
    public bool SetJumpTrigger()
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
    public void SetIsGround(bool isGround)
    {
        if (this.isGround == isGround) return;

        this.isGround = isGround;

        playerAnimator.SetBool("IsGround", isGround);
    }
    private void OnEnable()
    {
        GetComponent<PlayerController>().BindToPlayerAnimator(SetMoveSpeed);
    }
    private void OnDisable()
    {
        GetComponent<PlayerController>().UnbindFromPlayerAnimator(SetMoveSpeed);
    }
}

public enum PlayerAnimationState
{
    Stand,
    Crouch,
    Jump
}