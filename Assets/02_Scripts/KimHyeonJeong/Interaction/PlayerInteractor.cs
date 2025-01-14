using UnityEngine;

public class PlayerInteractor : MonoBehaviour
{
    public Transform cameraTransform;

    // public LayerMask layerMask;

    public float range = 1f;

    Ray ray = new Ray();

    IInteractable interactable = null;

    private void Update()
    {
        ray.origin = cameraTransform.position;
        ray.direction = cameraTransform.forward;

        // �浹 O
        if (Physics.Raycast(ray, out RaycastHit hit, range/*, layerMask*/)) // ���̾� ����ũ ���� ���� X
        {
            IInteractable interactable = hit.collider.GetComponent<IInteractable>();

            if (interactable != null)
            {
                if (this.interactable == null)
                {
                    this.interactable = interactable;

                    // UI Tooltip ������
                    UIManager.Instance.OnShowInteractText(interactable.TooltipText);
                }

                else if (this.interactable != interactable)
                {
                    this.interactable = interactable;

                    // UI Tooltip ������
                    UIManager.Instance.OnShowInteractText(interactable.TooltipText);
                }
            }
            else
            {
                UIManager.Instance.OnHideInteractText();
            }
        }

        // �浹 X
        else
        {
            if (interactable != null)
            {
                UIManager.Instance.OnHideInteractText();
            }
        }
    }
}
