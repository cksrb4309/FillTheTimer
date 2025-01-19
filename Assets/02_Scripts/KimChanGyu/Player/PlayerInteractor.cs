using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInteractor : MonoBehaviour
{
    public InteractionGuideTextUI interactionGuideTextUI;

    public InputActionReference interactionInputAction;

    public Transform cameraTransform;

    public LayerMask layerMask;

    public float range = 1f;

    Ray ray = new Ray();

    IInteractable interactable = null;

    private void OnEnable()
    {
        interactionInputAction.action.Enable();
    }
    private void OnDisable()
    {
        interactionInputAction.action.Disable();
    }

    private void Update()
    {
        Debug.Log("A : " + this.interactable);

        ray.origin = cameraTransform.position;
        ray.direction = cameraTransform.forward;
        
        // TODO ��ȣ�ۿ� ���� ���� (����)
        if (Physics.Raycast(ray, out RaycastHit hit, range, layerMask))
        {
            IInteractable interactable = hit.collider.GetComponent<IInteractable>();

            if (this.interactable == null)
            {
                this.interactable = interactable;

                interactionGuideTextUI.ShowInteractText(this.interactable.TooltipText);
            }
            else if (this.interactable != null && this.interactable != interactable)
            {
                this.interactable = interactable;

                interactionGuideTextUI.ShowInteractText(this.interactable.TooltipText);
            }

            Debug.Log("B : " + interactable);
        }
        else
        {
            if (this.interactable != null)
            {
                this.interactable = null;

                interactionGuideTextUI.HideInteractText();
            }
        }

        
        if (interactionInputAction.action.WasPressedThisFrame() && this.interactable != null)
        {
            Debug.Log("C : " + this.interactable);

            interactionGuideTextUI.HideInteractText();

            this.interactable.Interact();

            this.interactable = null;
        }
    }
}
