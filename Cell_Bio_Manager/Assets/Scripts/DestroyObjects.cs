using UnityEngine;

public class DestroyObjects : MonoBehaviour
{
    public SpawnPoints spawnPointsRef; // Reference to SpawnPoints script

    private Collider2D[] myColliders; // Array to hold this object's colliders

    void Awake()
    {
        myColliders = GetComponentsInChildren<Collider2D>();
        
            
    }



    // Triggered when a collision occurs with another 2D collider
    void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject other = collision.gameObject;

        // Check if the other object has the tag "Molecule"
        bool isMolecule = other.CompareTag("Molecule");

        // if it's a molecule, modify the molecule count in SpawnPoints
        if (isMolecule && spawnPointsRef != null)
        {
            
            spawnPointsRef.ModifyMoleculeCount(-1);
        }
        // remove the other object from the scene
        Destroy(other);

    }
 }
