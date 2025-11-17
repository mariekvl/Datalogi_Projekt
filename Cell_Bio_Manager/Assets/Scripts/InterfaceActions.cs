using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class InterfaceActions : MonoBehaviour
{
    public Scene shopScene;
    public UIDocument uIDocument;
    private Button shopButton;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        shopButton = uIDocument.rootVisualElement.Q<Button>("ShopButton");
        shopButton.clicked += openShop;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void openShop()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        shopScene = SceneManager.GetSceneByName("ShopScene");
        


        SceneManager.LoadScene("ShopScene");
    }
}
