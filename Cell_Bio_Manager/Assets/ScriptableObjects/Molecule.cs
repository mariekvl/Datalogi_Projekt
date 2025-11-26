using UnityEngine;

[CreateAssetMenu(fileName = "Molecule", menuName = "Scriptable Objects/Molecule")]
public class Molecule : ScriptableObject
{
    [field: SerializeField]
    public string Name { get; set; }
    [field: SerializeField]
    public Color Color { get; set; }

    [field: SerializeField]
    public int Level { get; set; }


    [field: SerializeField]
    public int ATPValue { get; set; }

    

}
