using UnityEngine;

public class DestroyTransition : MonoBehaviour
{
    private void Awake() => Invoke("DestroyImage", 1.5f);
    private void DestroyImage() => Destroy(gameObject);
}
