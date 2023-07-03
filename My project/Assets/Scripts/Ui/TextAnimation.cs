using System.Collections;
using TMPro;
using UnityEngine;

public class TextAnimation : MonoBehaviour
{
    [SerializeField] private float letterDelay; // Задержка между появлением букв
    [SerializeField] private float punctuationDelay;

    [TextArea(3, 10)]
    [SerializeField] private string[] phrases; // Массив фраз для отображения
    private int currentPhraseIndex = 0; // Текущий индекс фразы
    private string currentPhrase = ""; // Текущая отображаемая фраза
    private TextMeshProUGUI textMeshPro;

    private void Start()
    {
        textMeshPro = GetComponent<TextMeshProUGUI>();
        ShowNextPhrase();
    }

    private void ShowNextPhrase()
    {
        if (currentPhraseIndex >= phrases.Length)
        {
            Debug.Log("Фразы закончились");
            return;
        }

        currentPhrase = phrases[currentPhraseIndex];
        currentPhraseIndex++;
        StartCoroutine(TypeText(currentPhrase));
    }

    private IEnumerator TypeText(string phrase)
    {
        textMeshPro.text = "";

        for (int i = 0; i <= phrase.Length; i++)
        {
            char currentChar = phrase[i];
            textMeshPro.text += currentChar;

            // Проверка на специальные знаки
            if (IsPunctuation(currentChar))
            {
                yield return new WaitForSeconds(punctuationDelay);
            }
            else
            {
                yield return new WaitForSeconds(letterDelay);
            }
        }
    }

    private bool IsPunctuation(char character)
    {
        return character == ',' || character == '.' || character == '!' || character == '?' || character == '—';
    }
    public void OnNextButtonClicked()
    {
        ShowNextPhrase();   
    }
}
