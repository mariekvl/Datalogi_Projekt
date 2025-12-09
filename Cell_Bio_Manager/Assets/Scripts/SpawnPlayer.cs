using UnityEngine;

public class SpawnPlayer : MonoBehaviour
{

    public GameObject playerPrefab;
    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
        
        spawnPlayerAtSpawnPoint();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void spawnPlayerAtSpawnPoint()
    {
        if (playerPrefab != null)
            Instantiate(playerPrefab, transform.position, Quaternion.identity);
    }
}
