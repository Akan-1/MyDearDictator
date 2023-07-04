using UnityEngine;
using UnityEngine.UI;

public class SliderValueUpdater : MonoBehaviour
{
    public Slider slider;
    public float variableValue = 50f;
    public Color redColor;
    public Color greenColor;

    public Image sliderFillImage;

    private void OnEnable()
    {
        Papers._onAccept += Accept;
        Papers._onReject += Reject;
    }

    private void OnDisable()
    {
        Papers._onAccept -= Accept;
        Papers._onReject -= Reject;
    }

    private void Start()
    {
        slider = GetComponent<Slider>();
        // Установка начального значения слайдера
        slider.value = variableValue;
    }

    private void Update()
    {
        // Обновление значения слайдера при изменении переменной
        if (slider.value != variableValue)
        {
            slider.value = variableValue;
        }

        // Обновление цвета слайдера в зависимости от значения
        if (slider.value < 30)
        {
            sliderFillImage.color = redColor;
        }
        else
        {
            sliderFillImage.color = greenColor;
        }
    }

    private void Accept()
    {
        var i = Random.Range(1, 3);
        if (i == 1)
        {
            var plus = Random.Range(5, 15);
            variableValue += plus;
        }
        if(i == 3 || i == 2)
        {
            var minus = Random.Range(10, 20);
            variableValue -= minus;
        }
    }
    private void Reject()
    {
        
    }
}
