using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoints : MonoBehaviour
{
    public List<GameObject> spawnPoints = new List<GameObject>();

    public List<GameObject> molecules = new List<GameObject>();

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        SpawnMolecules("Molecule2", 10);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public Vector3 GetRandomSpawnPoint()
    {
        int randomIndex = Random.Range(0, spawnPoints.Count);
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
