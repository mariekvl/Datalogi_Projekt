using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class ActiveRegion : MonoBehaviour
{
    public SpawnPoints spawnPointsRef;

    public UIDocument uIDocument;
    public PointManager pointManager;

    

    private Label atpScore;
    private Label pyruvateScore;

    

    private int level = 0;
   // private int maxLevel = 8;

    private AudioSource audioSource;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
        atpScore = uIDocument.rootVisualElement.Q<Label>("ATPValue");
        pyruvateScore = uIDocument.rootVisualElement.Q<Label>("PyruvateValue");
        audioSource = GetComponent<AudioSource>();
    }

   


    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Molecule"))
        {
            string moleculeName = other.gameObject.name.Replace("(Clone)", "").Trim();
            if (moleculeName == level.ToString())
            {
                audioSource.Play();
                Destroy(other.gameObject);
                spawnPointsRef.SpawnNextMolecule(other.transform.position, 0, level+1);
                int moleculeATP = spawnPointsRef.GetMoleculeATPValue(level);
                int newATP = pointManager.atpScore + moleculeATP;
                atpScore.text = newATP.ToString();

            }
            
        }
        
    }
}
