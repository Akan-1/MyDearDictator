using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ConflictsMessages : MonoBehaviour
{
    [TextArea(3, 10)]
    [SerializeField] private List<string> textInSign;
    [SerializeField] private TextMeshProUGUI textMeshPro;

    public SliderValueUpdater SliderVal;

    private void Start()
    {
        SetNewPhrase();
    }

    public void SetNewPhrase()
    {
        int randomIndex = Random.Range(0, textInSign.Count);
        textMeshPro.text = textInSign[randomIndex];
    }

    public void Suppress()
    {
        SliderVal.Accept();
        SetNewPhrase();
    }

    public void Ignore()
    {
        SliderVal.Accept();
        var i = Random.Range(1, 5);
        if(i == 3)
        {
            SliderVal.GameOver();
        }
    }
}