using System.Collections.Generic;
using UnityEngine;
using RN = UnityEngine.Random;
using TMPro;
using UnityEngine.Events;
using System;

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

    public bool isHover = false;

    public static Action _onReject;
    public static Action _onAccept;

    Vector3 desiredPosition;

    public bool IsHover
    {
        get => isHover;
        set => isHover = value;
    }


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

    void Start()
    {
        moveToPoint = GameObject.FindWithTag("Point").transform;
        acceptPoint = GameObject.FindWithTag("Accept").transform;
        rejectPoint = GameObject.FindWithTag("Reject").transform;
        desiredPosition = moveToPoint.position;
        int randomIndex = RN.Range(0, textInPaper.Count);
        textMeshPro.text = textInPaper[randomIndex];
    }

    int count;
    void Update()
    {
        desiredPosition = moveToPoint.position;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, _smoothSpeed * Time.deltaTime);
        transform.position = smoothedPosition;

        if (transform.position == moveToPoint.position && count != 1)
        {
            count++;
            onReachPosition.Invoke();
        }

        if (!markIsSet)
        {
            if (Input.GetMouseButtonDown(0) && IsHover)
            {
                leftSpawnObject.SetActive(true);
                desiredPosition = acceptPoint.position;
                Invoke("DestroyPaper", 1.1f);
                markIsSet = true;
                EventAfterMarked.Invoke();
                _onAccept?.Invoke();
            }

            if (Input.GetMouseButtonDown(1) && IsHover)
            {
                rightSpawnObject.SetActive(true);
                desiredPosition = rejectPoint.position;
                Invoke("DestroyPaper", 1.1f);
                markIsSet = true;
                EventAfterMarked.Invoke();
                _onReject?.Invoke();
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

    private void DestroyPaper()
    {
        markIsSet = false;
        Destroy(gameObject);
    }
}
