using UnityEngine;

public class EntryInteraction : MonoBehaviour, IInteractable
{
    public string TooltipText => "����(E)";

    [SerializeField] EntryInteraction pair = null;

    public Transform movePosition;

    public void Interact()
    {
        Debug.Log("���� : " + gameObject.transform.parent.gameObject.name + " / position : " + movePosition.ToString());
        Debug.Log("���� : " + pair.movePosition.parent.gameObject.name + " / position : " + pair.movePosition.ToString());

        PlayerItemHandler.Instance.GetComponent<PlayerController>().MovePosition(pair.movePosition);
    }
}
