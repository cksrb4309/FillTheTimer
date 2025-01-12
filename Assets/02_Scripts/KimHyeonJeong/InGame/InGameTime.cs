using UnityEngine;
using UnityEngine.UI;

public class InGameTime : MonoBehaviour
{
    [SerializeField]
    Light directionalLight;
    [SerializeField]
    Text timeText;

    public float inGameTime;
    float updateInterval = 60f;
    public float timeCounter;

    
    void Start()
    {
        inGameTime = 7 * 3600; // 07:00
        timeCounter = 0;
        UpdateTimeText();
    }

    void Update()
    {
        timeCounter += Time.deltaTime;
        inGameTime += Time.deltaTime * 10; // 10�� ������ �ð��帧
        if (timeCounter>= updateInterval)
        {
            timeCounter = 0;
            UpdateTimeText();
        }

        if(inGameTime >= 86400) // 24�ð� ������
        {
            inGameTime = 0f;
        }

        UpdateLightPosition();
    }

    void UpdateTimeText()
    {
        int hours = Mathf.FloorToInt(inGameTime / 3600);
        int minutes = Mathf.FloorToInt((inGameTime % 3600) / 60);
        timeText.text = string.Format("{0:D2}:{1:D2}", hours, minutes);
    }

    void UpdateLightPosition()
    {
        if (inGameTime >= 330*60 && inGameTime < 1170*60) // 05:30 (330��)���� 19:30 (1170��)����
        {
            directionalLight.enabled = true;

            float t = (inGameTime - 330 * 60) / (1170*60 - 330 * 60); // 0���� 1 ������ ��
            float angle = t * 360; // ��ü ���� �׸��� ���� ���� ���
            float radius = 10f; // ���� ������
            float x = radius * Mathf.Cos(angle * Mathf.Deg2Rad); // x��ǥ
            float z = radius * Mathf.Sin(angle * Mathf.Deg2Rad); // z��ǥ

            // ���� ��ġ�� ���� �׸��� �̵�
            directionalLight.transform.position = new Vector3(x, 10, z);
        }
        else
        {
            directionalLight.enabled = false; 
        }
    }
}
