using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class InterfaceActions : MonoBehaviour
{
    
    public UIDocument uIDocument; // Reference to the UIDocument component
    public PointManager pointManager;
    private Button shopButton;
    private Label atpScore;
    private Label pyruvateScore;
    public int startATP = 100;
    public int startPyruvate = 0;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        shopButton = uIDocument.rootVisualElement.Q<Button>("ShopButton");
        shopButton.clicked += openShop;

        atpScore = uIDocument.rootVisualElement.Q<Label>("ATPValue");
        pointManager.atpScore = startATP;
        atpScore.text = pointManager.atpScore.ToString("D3"); // Display ATP score with leading zeros

        pyruvateScore = uIDocument.rootVisualElement.Q<Label>("PyruvateValue");
        pointManager.pyruvateScore = startPyruvate;
        pyruvateScore.text = pointManager.pyruvateScore.ToString("D3");

        
    }

    // Update is called once per frame
    void Update()
    {
        if (atpScore != null)
        {
            atpScore.text = pointManager.atpScore.ToString("D3");
        }
    }

    void openShop()
    {
        if (SceneManager.GetSceneByName("ShopScene").isLoaded)
        {
            Debug.Log("Shop scene is already loaded.");
            return;
        }
        

        SceneManager.LoadScene("ShopScene",LoadSceneMode.Additive);
        //SceneManager.SetActiveScene(SceneManager.GetSceneByName("ShopScene"));
        

    }
}
