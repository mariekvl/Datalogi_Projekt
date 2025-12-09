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
        if (pointManager.spawnNewWorker)
        {
            pointManager.spawnNewWorker = false;
            spawn(pointManager.workerLevel);
        }
        

    }



    public void spawn(int level)
    {

        if (workerPrefab == null)

            return;
        
        
        Vector2 position = nodeLocations.getRandomNode().transform.position;
        GameObject newWorker = Instantiate(workerPrefab, position, Quaternion.identity);
        newWorker.GetComponentInChildren<ActiveRegion>().setLevel(level);
        


    }
   
}
