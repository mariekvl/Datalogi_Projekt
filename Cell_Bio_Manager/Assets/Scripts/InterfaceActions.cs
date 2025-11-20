using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class InterfaceActions : MonoBehaviour
{
    
    public UIDocument uIDocument;
    private Button shopButton;
    private Scene savedScene;

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
        if (SceneManager.GetSceneByName("ShopScene").isLoaded)
        {
            Debug.Log("Shop scene is already loaded.");
            return;
        }
        SceneManager.LoadScene("ShopScene",LoadSceneMode.Additive);
        SceneManager.SetActiveScene(SceneManager.GetSceneByName("ShopScene"));

    }
}
