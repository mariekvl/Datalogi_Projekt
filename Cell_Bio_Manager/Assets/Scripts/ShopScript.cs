using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class ShopScript : MonoBehaviour
{
    public UIDocument uIDocument;
    public PointManager pointManager;

    private Label atpScore;
    private Label pyruvateScore;

    private Button backButton;

    private Button buyUpgrade;
    private Button buyHexokinase;

    private Label upgradePrice;
    private Label hexokinasePrice;
    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        atpScore = uIDocument.rootVisualElement.Q<Label>("ATPValue");
        atpScore.text = pointManager.atpScore.ToString("D3");
        pyruvateScore = uIDocument.rootVisualElement.Q<Label>("PyruvateValue");
        pyruvateScore.text = pointManager.pyruvateScore.ToString("D3");

        backButton = uIDocument.rootVisualElement.Q<Button>("BackButton");
        backButton.clicked += CloseShop;

        buyUpgrade = uIDocument.rootVisualElement.Q<Button>("BuyUpgrade");
        buyUpgrade.clicked += BuyUpgrade;
        buyHexokinase = uIDocument.rootVisualElement.Q<Button>("BuyHexokinase");
        buyHexokinase.clicked += BuyHexokinase;


        upgradePrice = uIDocument.rootVisualElement.Q<Label>("UpgradePrice");
        hexokinasePrice = uIDocument.rootVisualElement.Q<Label>("HexokinasePrice");
    }

    

   void CloseShop()
   {
        SceneManager.SetActiveScene(SceneManager.GetSceneByName("MainScene"));
        SceneManager.UnloadSceneAsync("ShopScene");
        
    }

    void BuyUpgrade()
    {  
        if (int.Parse(upgradePrice.text) <= int.Parse(atpScore.text))
        {
            print("Upgrade Purchased");
            int newAtpScore = int.Parse(atpScore.text) - int.Parse(upgradePrice.text);
            atpScore.text = newAtpScore.ToString();
            pointManager.atpScore = newAtpScore;
        }
        
    }

    void BuyHexokinase()
    {
        if (int.Parse(hexokinasePrice.text) <= int.Parse(atpScore.text))
        {
            print("Hexokinase Purchased");
            int newAtpScore = int.Parse(atpScore.text) - int.Parse(hexokinasePrice.text);
            atpScore.text = newAtpScore.ToString();
            pointManager.atpScore = newAtpScore;
        }
            
    }
}
