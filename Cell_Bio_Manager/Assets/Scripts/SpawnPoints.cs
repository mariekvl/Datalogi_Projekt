using NUnit.Framework;
using System;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoints : MonoBehaviour
{
    public List<GameObject> spawnPoints = new List<GameObject>();

    public List<GameObject> molecules = new List<GameObject>();

    public int currentLevel = 0;

    //maybe move this later?
    public Array moleculeNames = new string[]
    {
        "Glucose",
        "Glucose 6-phosphate",
        "Fructose 6-phosphate",
        "Fructose 1,6-biphosphate",
        "Glyceraldehyde 3-phosphate",
        "1,3-bisphosphoglycerate",
        "3-phosphoglycerate",
        "2-phosphoglycerate",
        "Phosphoenolpyruvate"
    };

    public int moleculeCount = 0;

    public float waitTimer = 5.0f;
    private float currentTime = 0.0f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        SpawnMolecules(getMoleculeName(currentLevel), 10);
    }

    // Update is called once per frame
    void Update()
    {
        currentTime += Time.deltaTime;
        if (currentTime >= waitTimer && moleculeCount < 20)
        {
            SpawnMolecules(getMoleculeName(currentLevel), 5);
            currentTime = 0.0f;
        }



    }



    public String getMoleculeName(int index)
    {
        return (String)moleculeNames.GetValue(index);
    }

    public Vector3 GetRandomSpawnPoint()
    {
        int randomIndex = UnityEngine.Random.Range(0, spawnPoints.Count);
        return spawnPoints[randomIndex].transform.position;
    }

    public GameObject GetMolecule(string name)
    {
        foreach (GameObject molecule in molecules)
        {
            if (molecule.name == name)
            {
                return molecule;
            }
        }
        Assert.Fail("Molecule with name " + name + " not found.");
        return null;
    }

    public void SpawnMolecules(string name, int count)
    {
        GameObject moleculePrefab = GetMolecule(name);
        for (int i = 0; i < count; i++)
        {
            Vector3 spawnPosition = GetRandomSpawnPoint();
            Instantiate(moleculePrefab, spawnPosition, Quaternion.identity);
            moleculeCount++;
            print(moleculeCount);
        }
    }

    // Public API to safely change molecule count (prevents negative values).
    public void ModifyMoleculeCount(int delta)
    {
        moleculeCount = Mathf.Max(0, moleculeCount + delta);
        print(moleculeCount);
    }
}
