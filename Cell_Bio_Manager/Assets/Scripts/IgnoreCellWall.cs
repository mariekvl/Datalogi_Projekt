using UnityEngine;

public class IgnoreCellWall : MonoBehaviour
{
    [Tooltip("Tag used for walls that Glucose should pass through.")]
    public string cellWallTag = "Wall";

    // Whether to ignore collisions (true = pass through)
    public bool ignore = true;

    private Collider2D[] myColliders;

    void Awake()
    {
        myColliders = GetComponentsInChildren<Collider2D>();
    }

    void Start()
    {
        if (myColliders == null || myColliders.Length == 0)
            Debug.LogWarning($"{name}: No Collider2D found on this GameObject or its children.");

        // Find all walls with the given tag and ignore collisions with them
        GameObject[] walls = GameObject.FindGameObjectsWithTag(cellWallTag);
        foreach (var w in walls)
        {
            var wallColliders = w.GetComponentsInChildren<Collider2D>();
            foreach (var myCol in myColliders)
            {
                foreach (var wallCol in wallColliders)
                {
                    if (myCol != null && wallCol != null)
                        Physics2D.IgnoreCollision(myCol, wallCol, ignore);
                }
            }
        }
    }
}
