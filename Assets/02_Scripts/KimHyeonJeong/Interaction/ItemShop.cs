using UnityEngine;
using UnityEngine.UI;

public class ItemShop : MonoBehaviour
{
    [SerializeField] GameObject shopUI;
    [SerializeField] GameObject inGameUI;
    [SerializeField] PlayerController player;
    [SerializeField] GameManager gameManager;
    [SerializeField] Image oxygenTankForeground;

    void Start()
    {
        
    }

    
    void Update()
    {
        FillOxygenTank();
    }

    public void OnClickedXbutton() // ���� �ݱ� Ŭ��
    {
        shopUI.SetActive(false);
        inGameUI.SetActive(true);
        player.canMove = true;
    }

    public void OnClickedFlashlight() // ������ ������ Ŭ��
    {
        // ���ӸŴ������� ��� 0.3L(300ml)����
        gameManager.ChangeOxygen(-300f);

        // TODO: ���۱⿡�� ������ ����
    }

    public void FillOxygenTank()
    {
        // ��ҷ��� Tank�̹����� ǥ��
        oxygenTankForeground.fillAmount = gameManager.GetOxygen() / 2000f;
    }
}
