using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject papers;

    private void OnEnable()
    {
        Papers._onAccept += Spawn;
        Papers._onReject += Spawn;
    }
    private void OnDisable()
    {
        Papers._onAccept += Spawn;
        Papers._onReject += Spawn;
    }

    private void Spawn()
    {
        var spawnPoint = GameObject.FindWithTag("Papers");
        GameObject obj = Instantiate(papers, transform.position, Quaternion.identity);
        obj.gameObject.transform.SetParent(spawnPoint.transform);
    }
}
