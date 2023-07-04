using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Events;
using UnityEngine.UI;

public class Papers : MonoBehaviour
{
    [SerializeField] private Transform moveToPoint;
    [SerializeField] private float _smoothSpeed = 0.125f;
    [SerializeField] private TextMeshProUGUI textMeshPro;

    [TextArea(3,10)]
    [SerializeField] private List<string> textInPaper;

    [SerializeField] private UnityEvent onReachPosition;

    [TextArea(3, 10)]
    [SerializeField] private string[] newPhrases;
    [SerializeField] private UnityEvent newEvents;
    public bool isCanMove = false;

    [SerializeField] private UnityEvent EventAfterMarked;

    public bool IsCanMove
    {
        get => isCanMove;
        set => isCanMove = value;
    }

    public GameObject leftSpawnObject;
    public GameObject rightSpawnObject;

    public Transform acceptPoint;
    public Transform rejectPoint;

    private bool markIsSet = false;


    private void Awake()
    {
        moveToPoint = GameObject.FindWithTag("Point").transform;
        acceptPoint = GameObject.FindWithTag("Accept").transform;
        rejectPoint = GameObject.FindWithTag("Reject").transform;
    }

    void Start()
    {
        int randomIndex = Random.Range(0, textInPaper.Count);
        textMeshPro.text = textInPaper[randomIndex];
    }

    int count;
    void Update()
    {
        Vector3 desiredPosition = moveToPoint.position;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, _smoothSpeed * Time.deltaTime);
        transform.position = smoothedPosition;

        if (transform.position == moveToPoint.position && count != 1)
        {
            count++;
            onReachPosition.Invoke();
        }

        if (!markIsSet)
        {
            if (Input.GetMouseButton(0))
            {
                leftSpawnObject.SetActive(true);
                moveToPoint.position = acceptPoint.position;
                Invoke("DestroyPaper", 1.5f);
                markIsSet = true;
                EventAfterMarked.Invoke();
            }

            if (Input.GetMouseButton(1))
            {
                rightSpawnObject.SetActive(true);
                moveToPoint.position = rejectPoint.position;
                Invoke("DestroyPaper", 1.5f);
                markIsSet = true;
                EventAfterMarked.Invoke();
            }
        }
    }

    public void SetNewPhrasesForPier()
    {
        var oldPhrases = GameObject.FindGameObjectWithTag("TextArea");
        oldPhrases.GetComponent<TextAnimation>().phrases = newPhrases;
        oldPhrases.GetComponent<TextAnimation>().EventAfterCompliting = newEvents;
        oldPhrases.GetComponent<TextAnimation>().currentPhraseIndex = 0;
    }

    private void DestroyPaper() => Destroy(gameObject);
}
