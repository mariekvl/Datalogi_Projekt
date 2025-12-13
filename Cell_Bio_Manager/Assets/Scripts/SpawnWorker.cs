using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class SpawnWorker : MonoBehaviour
{
    public GameObject workerPrefab;
    public NodeLocations nodeLocations;
    public PointManager pointManager;
    


    private void Update()
    {
        // Check if a new worker needs to be spawned
        if (pointManager.spawnNewWorker)
        {
            // Reset the flag and spawn the worker
            pointManager.spawnNewWorker = false;
            spawn(pointManager.workerLevel);
        }
        

    }



    public void spawn(int level)
    {
        // Get a random node position from NodeLocations
        Vector2 position = nodeLocations.getRandomNode().transform.position;
        // Instantiate the worker prefab at the selected position
        GameObject newWorker = Instantiate(workerPrefab, position, Quaternion.identity);
        newWorker.GetComponentInChildren<ActiveRegion>().setLevel(level);
        


    }
   
}
