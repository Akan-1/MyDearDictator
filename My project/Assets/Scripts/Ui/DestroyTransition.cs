using UnityEngine;

public class DestroyTransition : MonoBehaviour
{
    [SerializeField] private GameObject obj;
    private void Awake()
    {
        obj.SetActive(true);
        Invoke("DestroyImage", 1.5f);
    }
    private void DestroyImage() => Destroy(gameObject);
}
