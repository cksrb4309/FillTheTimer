using UnityEngine;

public class OxygenTank : MonoBehaviour, IInteractable
{
    [SerializeField] float oxygen = 2000; // 2L=2000ml
    [SerializeField] PlayerOxygen player;

    public string TooltipText => "������� [E]";

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public float GetOxygen() // oxygen���� getter
    {
        return oxygen;
    }
    public void ChangeOxygen(float amount)
    {
        oxygen += amount;
    }

    public void Interact()
    {
        // TODO : �÷��̾��� ��� ����
    }
}
