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
    public GameObject currentMolecule; // reference to current molecule that workers should go after

    public bool MoleculeCaught = false;


    private int maxLevel = 8;

    private AudioSource audioSource;

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

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Molecule"))
        {
            string moleculeName = other.gameObject.name.Replace("(Clone)", "").Trim();
            if (moleculeName == level.ToString())
            {
                audioSource.Play();
                Destroy(other.gameObject);


                if (level == maxLevel)
                {
                    pointManager.pyruvateScore += 1;
                    pyruvateScore.text = pointManager.pyruvateScore.ToString();
                    MoleculeCaught = true;
                    return;
                }

                spawnPointsRef.SpawnNextMolecule(other.transform.position, 0, level+1);

                int moleculeATP = spawnPointsRef.GetMoleculeATPValue(level);
                int newATP = pointManager.atpScore + moleculeATP;
                pointManager.atpScore = newATP;


                //currentMolecule = spawnPointsRef.GetMolecule(spawnPointsRef.getMoleculeName(level + 1));

                
                atpScore.text = newATP.ToString();


                MoleculeCaught = true;
            }
        }
        
    }
}
