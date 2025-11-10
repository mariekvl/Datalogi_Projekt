using NUnit.Framework;
using System;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoints : MonoBehaviour
{
    public List<GameObject> spawnPoints = new List<GameObject>();

    public List<GameObject> molecules = new List<GameObject>();

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

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        SpawnMolecules(getMoleculeName(0), 10);
    }

    // Update is called once per frame
    void Update()
    {
        
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
        }
    }
}
