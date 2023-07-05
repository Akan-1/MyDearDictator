using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SliderValueUpdater : MonoBehaviour
{
    public Slider slider;
    public float variableValue = 50f;
    public Color redColor;
    public Color greenColor;

    public Image sliderFillImage;

    public GameObject sign;

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

    public GameObject GameOverSliderValue;
    public GameObject WinSliderValue;
    public AudioSource BreakSound;
    public AudioSource WinSound;
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

        if (slider.value <= 0)
        {
            GameOverSliderValue.SetActive(true);
            BreakSound.Play();
            Invoke("ReloadScene", 5.1f);
        }

        if(slider.value >= 100)
        {
            WinSliderValue.SetActive(true);
            WinSound.Play();
        }

    }


    int acceptCount = 0;
    int rejectCount = 0;
    public GameObject acceptBreak;
    public GameObject rejectBreak; 
    public AudioSource signAppearSound;

    public void Accept()
    {
        var i = Random.Range(1, 5);
        if (i == 1 || i == 2)
        {
            var plus = Random.Range(5, 10);
            variableValue += plus;
        }
        if (i == 3)
        {
            var minus = Random.Range(5, 13);
            variableValue -= minus;
        }
        if (i == 4)
        {
            sign.SetActive(true);
            signAppearSound.Play();
        }
        acceptCount++;
        
        if (acceptCount >= 20)
        {
            acceptBreak.SetActive(true);
            Invoke("ReloadScene", 5.1f);
            BreakSound.Play();
        }

        rejectCount--;
        
        if (rejectCount <= 0)
            rejectCount = 0;
    }

   
    public void Reject()
    {
        var i = Random.Range(1, 5);
        if (i == 1)
        {
            sign.SetActive(true);
            signAppearSound.Play();
        }
        if (i == 2)
        {
            var plus = Random.Range(1, 10);
            variableValue += plus;
        }
        if (i == 3 || i == 4)
        {
            var minus = Random.Range(5, 20);
            variableValue -= minus;
        }
        rejectCount++;

        if(rejectCount >= 10)
        {
            rejectBreak.SetActive(true);
            BreakSound.Play();
            Invoke("ReloadScene", 5.1f);
        }

        acceptCount--;
        if(acceptCount <= 0)
            acceptCount = 0;
        
    }

    public GameObject gameoverPanel;

    public void GameOver()
    {
        gameoverPanel.SetActive(true);
        Invoke("ReloadScene", 5.1f);
    }
    
    private void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

}
