using UnityEngine;

public class DestroyObjects : MonoBehaviour
{
    public SpawnPoints spawnPointsRef;

    private Collider2D[] myColliders;

    void Awake()
    {
        myColliders = GetComponentsInChildren<Collider2D>();
        
            
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject other = collision.gameObject;

        // Determine whether the collided object is a molecule.
        // Recommended: tag molecule prefabs as "Molecule" OR ensure they have a Movement component.
        bool isMolecule = other.CompareTag("Molecule") || other.GetComponent<Movement>() != null;

        if (isMolecule && spawnPointsRef != null)
        {
            // Decrement molecule count using the SpawnPoints API.
            spawnPointsRef.ModifyMoleculeCount(-1);
        }

        Destroy(other);

    }
 }
