using UnityEngine;

public class DestroyObjects : MonoBehaviour
{
    public SpawnPoints spawnPointsRef;

    private Collider2D[] myColliders;

    void Awake()
    {
        myColliders = GetComponentsInChildren<Collider2D>();
        
            
    }

    


    void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject other = collision.gameObject;

        
        bool isMolecule = other.CompareTag("Molecule") || other.GetComponent<Movement>() != null;

        if (isMolecule && spawnPointsRef != null)
        {
            
            spawnPointsRef.ModifyMoleculeCount(-1);
        }

        Destroy(other);

    }
 }
