using UnityEngine;
using UnityEngine.UI;

public class InteractionProp : MonoBehaviour
{
    [SerializeField]
    GameObject interactionText;
    [SerializeField]
    GameObject shopUI;

    private bool isNearObject;


    void Start()
    {
        isNearObject=false;
    }

    void Update()
    {
        if(isNearObject && Input.GetKeyDown(KeyCode.E))
        {
            // ���� UI ����
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("��ȣ�ۿ� ����");
            isNearObject = true;
            interactionText.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("��ȣ�ۿ� ��������");
            isNearObject = false;
            interactionText.SetActive(false);
        }
    }
}
