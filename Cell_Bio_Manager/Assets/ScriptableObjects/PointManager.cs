using UnityEngine;

[CreateAssetMenu(fileName = "PointManager", menuName = "Scriptable Objects/PointManager")]
public class PointManager : ScriptableObject
{
    [field: SerializeField]
    public int atpScore { get; set; }
    [field: SerializeField]
    public int pyruvateScore { get; set; }
    [field: SerializeField]
    public int level { get; set; }
    [field: SerializeField]
    public int upgradePrice { get; set; }

    [field: SerializeField]
    public int workerLevel { get; set; }
    [field: SerializeField]
    public bool spawnNewWorker { get; set; }

    //added after hand-in
    [field: SerializeField]
    public int numberOfWorkers { get; set; } = 0;

}
