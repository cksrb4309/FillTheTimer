using UnityEngine;

public interface IInteractable // ��ȣ�ۿ� �������̽�
{
    public void Interact(); // ��ȣ�ۿ�
    public string TooltipText { get; } // Ŀ���� ������ ���� �� �� UI Text
}