using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class ShopScript : MonoBehaviour
{
    public UIDocument uIDocument;
    private Button backButton;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        backButton = uIDocument.rootVisualElement.Q<Button>("BackButton");
        backButton.clicked += CloseShop;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

   void CloseShop()
   {
        
        SceneManager.UnloadSceneAsync("ShopScene");
        SceneManager.SetActiveScene(SceneManager.GetSceneByName("MainScene"));

    }
}
