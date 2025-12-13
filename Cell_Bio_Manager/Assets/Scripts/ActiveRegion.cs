using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class ActiveRegion : MonoBehaviour
{
    public SpawnPoints spawnPointsRef; // reference to SpawnPoints script

    public UIDocument uIDocument; // reference to UI Document - for updating ATP and Pyruvate scores
    public PointManager pointManager; // reference to PointManager script



    private Label atpScore; // reference to ATP score label in UI
    private Label pyruvateScore; // reference to Pyruvate score label in UI


    private int level = 0; // current level of the active region
    //public GameObject currentMolecule; // reference to current molecule that workers should go after

    //public bool MoleculeCaught = false;


    private int maxLevel = 8; // maximum level before pyruvate is awarded

    private AudioSource audioSource; // reference to audio source component for the sound effect

    // method for getting current molecule by level
    //public GameObject GetCurrentMolecule()
    //{
    //    if (currentMolecule != null)
    //        return currentMolecule;

    //    if (spawnPointsRef != null)
    //        return spawnPointsRef.GetMolecule(spawnPointsRef.getMoleculeName(level));

    //    return null;
    //}





    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

        uIDocument = GameObject.FindGameObjectWithTag("UI").GetComponent<UIDocument>();
        spawnPointsRef = GameObject.FindGameObjectWithTag("SpawnPoints").GetComponent<SpawnPoints>();
        atpScore = uIDocument.rootVisualElement.Q<Label>("ATPValue");
        pyruvateScore = uIDocument.rootVisualElement.Q<Label>("PyruvateValue");
        audioSource = GetComponent<AudioSource>();
    }

   
    public void setLevel(int newLevel)
    {
        level = newLevel;
    }

    public int getLevel()
    {
        return level;
    }

    // Triggered when another collider enters the trigger collider attached to this object
    void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the other collider has the tag "Molecule"
        if (other.CompareTag("Molecule"))
        {
            // Get the name of the molecule by removing "(Clone)" from the GameObject's name
            string moleculeName = other.gameObject.name.Replace("(Clone)", "").Trim();
            // Check if the molecule's name matches the current level
            if (moleculeName == level.ToString())
            {
                audioSource.Play();
                Destroy(other.gameObject);

                // Check if the current level is the maximum level
                if (level == maxLevel)
                {
                    // Award pyruvate instead of spawning a new molecule
                    pointManager.pyruvateScore += 1;
                    // Update the pyruvate score in the UI
                    pyruvateScore.text = pointManager.pyruvateScore.ToString();
                    //MoleculeCaught = true;
                    return;
                }
                // Spawn the next molecule at the position of the caught molecule
                spawnPointsRef.SpawnNextMolecule(other.transform.position, 0, level+1);
                // Award ATP for catching the molecule
                int moleculeATP = spawnPointsRef.GetMoleculeATPValue(level);
                int newATP = pointManager.atpScore + moleculeATP; // add molecule ATP to current ATP score
                pointManager.atpScore = newATP; // update ATP score in PointManager


                //currentMolecule = spawnPointsRef.GetMolecule(spawnPointsRef.getMoleculeName(level + 1));

                // Update the ATP score in the UI
                atpScore.text = newATP.ToString();


                //MoleculeCaught = true;
            }
        }
        
    }
}
