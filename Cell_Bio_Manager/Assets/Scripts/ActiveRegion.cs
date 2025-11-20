using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class ActiveRegion : MonoBehaviour
{
    public SpawnPoints spawnPointsRef;

    public UIDocument uIDocument;

    private Label atpScore;
    private Label pyruvateScore;

    private int level = 0;
    private int maxLevel = 10;

    private AudioSource audioSource;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
        atpScore = uIDocument.rootVisualElement.Q<Label>("ATPValue");
        pyruvateScore = uIDocument.rootVisualElement.Q<Label>("PyruvateValue");
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Molecule"))
        {
            
            string moleculeName = other.gameObject.name.Replace("(Clone)", "").Trim();
            if (moleculeName == spawnPointsRef.getMoleculeName(level))
            {
                audioSource.Play();
                Destroy(other.gameObject);
                spawnPointsRef.SpawnNextMolecule(other.transform.position, 0, level+1);

                atpScore.text = (int.Parse(atpScore.text) + 10).ToString();

            }
            
        }
        
    }
}
