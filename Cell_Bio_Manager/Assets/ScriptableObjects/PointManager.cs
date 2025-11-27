using UnityEngine;

[CreateAssetMenu(fileName = "PointManager", menuName = "Scriptable Objects/PointManager")]
public class PointManager : ScriptableObject
{
    [field: SerializeField]
    public int atpScore { get; set; }
    [field: SerializeField]
    public int pyruvateScore { get; set; }

}
