using UnityEngine;
using UnityEngine.UIElements;

public class ActiveRegion : MonoBehaviour
{

    public UIDocument uIDocument;

    private Label atpScore;
    private Label pyruvateScore;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
        atpScore = uIDocument.rootVisualElement.Q<Label>("ATPValue");
        pyruvateScore = uIDocument.rootVisualElement.Q<Label>("PyruvateValue");

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Molecule"))
        {
            Destroy(other.gameObject);
            atpScore.text = (int.Parse(atpScore.text) + 10).ToString();
        }
    }
}
