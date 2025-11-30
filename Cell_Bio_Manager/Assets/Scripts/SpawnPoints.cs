using NUnit.Framework;
using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SpawnPoints : MonoBehaviour
{
    public List<Molecule> MoleculeData = new List<Molecule>();

    public List<GameObject> spawnPoints = new List<GameObject>();

    //public List<GameObject> molecules = new List<GameObject>();

    public GameObject basicMolecule;
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

    public int moleculeCount = 0;

    public int MaxMoleculeCount = 30;

    //public int MinMoleculeCount = 5;

    private int spawnAmount = 5;
    

    public float waitTimer = 5.0f;
    private float currentTime = 0.0f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        SpawnMolecules(spawnAmount);
    }

    // Update is called once per frame
    void Update()
    {
        currentTime += Time.deltaTime;
        if (currentTime >= waitTimer && moleculeCount < MaxMoleculeCount)
        {
            spawnAmount = UnityEngine.Random.Range(3, 8);
            SpawnMolecules(spawnAmount);
            currentTime = 0.0f;
        }



    }

    public Vector3 GetRandomSpawnPoint()
    {
        int randomIndex = UnityEngine.Random.Range(0, spawnPoints.Count);
        return spawnPoints[randomIndex].transform.position;
    }

    //public String getMoleculeName(int index)
    //{
    //    return (String)moleculeNames.GetValue(index);
    //}



    public GameObject GetMolecule(int level)
    {
        if (level < 0 || level >= MoleculeData.Count)
        {
            Debug.LogError("Invalid molecule level: " + level);
            return null;
        }

        Molecule moleculeData = ScriptableObject.CreateInstance<Molecule>();

        for (int i = 0; i<MoleculeData.Count;i++)
        {
            if (MoleculeData[i].Level == level)
            {
                moleculeData = MoleculeData[i];
                break;
            }
        }
        spawnedMolecule = Instantiate(basicMolecule);
        if (level == 0)
        {
            spawnedMolecule.GetComponent<IgnoreCellWall>().enabled = true;
        }

        spawnedMolecule.GetComponent<SpriteRenderer>().color = moleculeData.Color;
        spawnedMolecule.name = moleculeData.Level.ToString();
        
        spawnedMolecule.transform.GetChild(0).GetComponent<TextMeshPro>().text = moleculeData.name;
        

        return spawnedMolecule;
    }

    public void SpawnMolecules(int count)
    {
        GameObject moleculePrefab = GetMolecule(currentLevel);
        for (int i = 0; i < count; i++)
        {
            Vector3 spawnPosition = GetRandomSpawnPoint();
            Instantiate(moleculePrefab, spawnPosition, Quaternion.identity);
            moleculeCount++;
            //print(moleculeCount);
        }
    }

    public void SpawnNextMolecule(Vector3 spawnPosition, int rotation, int playerLevel)
    {
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
