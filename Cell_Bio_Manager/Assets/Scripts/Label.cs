using UnityEngine;



public class Label : MonoBehaviour
{
    public string labelText = "Hello World!";
    public int width = 100;
    public int height = 20;


    void OnGUI()
    {
        GUI.Label(new Rect(0, 0, width, height), labelText);
    }
}
