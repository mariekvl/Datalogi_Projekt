using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class InterfaceActions : MonoBehaviour
{

    public UIDocument uIDocument;
    private Button shopButton;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        shopButton = uIDocument.rootVisualElement.Q<Button>("ShopButton");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void openShop()
    {
        SceneManager.LoadScene("ShopScene");
    }
}
