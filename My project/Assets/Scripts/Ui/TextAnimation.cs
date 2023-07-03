using System.Collections;
using TMPro;
using UnityEngine;

public class TextAnimation : MonoBehaviour
{
    [SerializeField] private float letterDelay; // �������� ����� ���������� ����
    [SerializeField] private float punctuationDelay;

    [TextArea(3, 10)]
    [SerializeField] private string[] phrases; // ������ ���� ��� �����������
    private int currentPhraseIndex = 0; // ������� ������ �����
    private string currentPhrase = ""; // ������� ������������ �����
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
            Debug.Log("����� �����������");
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
