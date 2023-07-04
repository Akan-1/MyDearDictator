using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class TextAnimation : MonoBehaviour
{
    [SerializeField] private float letterDelay; // �������� ����� ���������� ����
    [SerializeField] private float punctuationDelay;

    [TextArea(3, 10)]
    public string[] phrases; // ������ ���� ��� �����������
    public int currentPhraseIndex = 0; // ������� ������ �����
    private string currentPhrase = ""; // ������� ������������ �����
    private TextMeshProUGUI textMeshPro;

    public UnityEvent EventAfterCompliting;

    private void Start()
    {
        textMeshPro = GetComponent<TextMeshProUGUI>();
        ShowNextPhrase();
    }

    public void ShowNextPhrase()
    {
        if (currentPhraseIndex >= phrases.Length)
        {
            EventAfterCompliting.Invoke();
            EventAfterCompliting = null;
        }

        currentPhrase = phrases[currentPhraseIndex];
        currentPhraseIndex++;
        StartCoroutine(TypeText(currentPhrase));
    }

    public IEnumerator TypeText(string phrase)
    {
        textMeshPro.text = "";

        for (int i = 0; i <= phrase.Length; i++)
        {
            char currentChar = phrase[i];
            textMeshPro.text += currentChar;

            // �������� �� ����������� �����
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
        return character == ',' || character == '.' || character == '!' || character == '?' || character == '�';
    }
    public void OnNextButtonClicked()
    {
        ShowNextPhrase();   
    }
}
