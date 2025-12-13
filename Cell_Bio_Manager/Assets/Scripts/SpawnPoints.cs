using NUnit.Framework;
using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SpawnPoints : MonoBehaviour
{
    public List<Molecule> MoleculeData = new List<Molecule>();// Scriptable objects for molecule data

    public List<GameObject> spawnPoints = new List<GameObject>();// list of spawn point game objects

    //public List<GameObject> molecules = new List<GameObject>();


    public GameObject basicMolecule;// prefab to instantiate
    private GameObject spawnedMolecule;

    public int currentLevel = 0;
    //private int maxLevel = 8;

    //maybe move this later?
    //public Array moleculeNames = new string[]
    //{
    //    "Glucose",
    //    "Glucose 6-phosphate",
    //    "Fructose 6-phosphate",
    //    "Fructose 1,6-biphosphate",
    //    "Glyceraldehyde 3-phosphate",
    //    "1,3-bisphosphoglycerate",
    //    "3-phosphoglycerate",
    //    "2-phosphoglycerate",
    //    "Phosphoenolpyruvate"
    //};

    public int moleculeCount = 0;// current number of molecules in the scene

    public int MaxMoleculeCount = 30;// maximum number of molecules allowed in the scene

    //public int MinMoleculeCount = 5;

    private int spawnAmount = 5;// number of molecules to spawn at a time

    // Timer variables for spawning molecules over time
    public float waitTimer = 5.0f;
    private float currentTime = 0.0f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Initial spawn of molecules
        SpawnMolecules(spawnAmount);
    }

    // Update is called once per frame
    void Update()
    {
        // Update the timer
        currentTime += Time.deltaTime;
        // Check if it's time to spawn more molecules
        if (currentTime >= waitTimer && moleculeCount < MaxMoleculeCount)
        {
            // Randomize spawn amount between 3 and 7
            spawnAmount = UnityEngine.Random.Range(3, 8);
            SpawnMolecules(spawnAmount);
            // Reset the timer
            currentTime = 0.0f;
        }



    }

    public Vector3 GetRandomSpawnPoint()
    {
        // Select a random spawn point from the list
        int randomIndex = UnityEngine.Random.Range(0, spawnPoints.Count);
        return spawnPoints[randomIndex].transform.position;
    }

    //public String getMoleculeName(int index)
    //{
    //    return (String)moleculeNames.GetValue(index);
    //}



    public GameObject GetMolecule(int level)
    {
        // Validate level
        if (level < 0 || level >= MoleculeData.Count)
        {
            Debug.LogError("Invalid molecule level: " + level);
            return null;
        }

        // Find the molecule data for the specified level
        Molecule moleculeData = ScriptableObject.CreateInstance<Molecule>();
        for (int i = 0; i<MoleculeData.Count;i++)
        {
            if (MoleculeData[i].Level == level)
            {
                moleculeData = MoleculeData[i];
                break;
            }
        }
        // Instantiate the molecule prefab and set its properties
        spawnedMolecule = Instantiate(basicMolecule);
        // If level is 0, enable IgnoreCellWall component
        if (level == 0)
        {
            spawnedMolecule.GetComponent<IgnoreCellWall>().enabled = true;
        }
        // Set molecule properties based on the ScriptableObject data
        spawnedMolecule.GetComponent<SpriteRenderer>().color = moleculeData.Color;
        spawnedMolecule.name = moleculeData.Level.ToString();
        
        spawnedMolecule.transform.GetChild(0).GetComponent<TextMeshPro>().text = moleculeData.name;
        

        return spawnedMolecule;
    }

    public void SpawnMolecules(int count)
    {
        // Spawn the specified number of molecules at random spawn points
        GameObject moleculePrefab = GetMolecule(currentLevel);
        for (int i = 0; i < count; i++)
        {
            Vector3 spawnPosition = GetRandomSpawnPoint();
            Instantiate(moleculePrefab, spawnPosition, Quaternion.identity);
            moleculeCount++;
            //print(moleculeCount);
        }
    }

    // method used when active region "changes" molecule
    public void SpawnNextMolecule(Vector3 spawnPosition, int rotation, int playerLevel)
    {
        // Spawn the next level molecule at the specified position with rotation
        GameObject moleculePrefab = GetMolecule(playerLevel);
        Instantiate(moleculePrefab, spawnPosition, Quaternion.Euler(0, 0, rotation));
    }

    // Public API to safely change molecule count (prevents negative values).
    public void ModifyMoleculeCount(int delta)
    {
        moleculeCount = Mathf.Max(0, moleculeCount + delta);
        //print(moleculeCount);
    }

    public int GetMoleculeATPValue(int level)
    {
        // Find and return the ATP value for the specified molecule level
        for (int i = 0; i < MoleculeData.Count; i++)
        {
            if (MoleculeData[i].Level == level)
            {
                return MoleculeData[i].ATPValue;
            }
        }
        Debug.LogError("Invalid molecule level: " + level);
        return 0;
    }
}
