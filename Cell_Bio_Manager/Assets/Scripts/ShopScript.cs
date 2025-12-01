using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class ShopScript : MonoBehaviour
{
    public UIDocument uIDocument;
    public PointManager pointManager;
    public int maxLevel = 8;

    private Label atpScore;
    private Label pyruvateScore;


    private Button backButton;

    private Button winButton;
    private Label winPrice;

    private Button buyUpgrade;

    private Button buyEnzyme1;



    private Label upgradePrice;

    private Label priceEnzyme1;
    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        atpScore = uIDocument.rootVisualElement.Q<Label>("ATPValue");
        atpScore.text = pointManager.atpScore.ToString("D3");
        pyruvateScore = uIDocument.rootVisualElement.Q<Label>("PyruvateValue");
        pyruvateScore.text = pointManager.pyruvateScore.ToString("D3");

        backButton = uIDocument.rootVisualElement.Q<Button>("BackButton");
        backButton.clicked += CloseShop;

        winButton = uIDocument.rootVisualElement.Q<Button>("BuyWin");
        winButton.clicked += winTheGame;
        winPrice = uIDocument.rootVisualElement.Q<Label>("WinPrice");

        buyUpgrade = uIDocument.rootVisualElement.Q<Button>("BuyUpgrade");
        buyUpgrade.clicked += BuyUpgrade;
        buyEnzyme1 = uIDocument.rootVisualElement.Q<Button>("BuyEnzyme1");
        buyEnzyme1.clicked += BuyEnzyme;


        upgradePrice = uIDocument.rootVisualElement.Q<Label>("UpgradePrice");
        upgradePrice.text = pointManager.upgradePrice.ToString();
        priceEnzyme1 = uIDocument.rootVisualElement.Q<Label>("PriceEnzyme1");
    }

    

   void CloseShop()
   {
        SceneManager.SetActiveScene(SceneManager.GetSceneByName("MainScene"));
        SceneManager.UnloadSceneAsync("ShopScene");
        
    }

    void winTheGame()
    {
        if (int.Parse(winPrice.text) <= int.Parse(pyruvateScore.text))
            Application.Quit();
    }

    void BuyUpgrade()
    {  

        if (int.Parse(upgradePrice.text) <= int.Parse(atpScore.text))
        {
            print("Upgrade Purchased");
            int newAtpScore = int.Parse(atpScore.text) - int.Parse(upgradePrice.text);
            atpScore.text = newAtpScore.ToString();
            pointManager.atpScore = newAtpScore;

            pointManager.level += 1;
            upgradePrice.text = (int.Parse(upgradePrice.text) * 1.5).ToString();
            pointManager.upgradePrice = int.Parse(upgradePrice.text);

            if (pointManager.level >= maxLevel)
            {
                buyUpgrade.SetEnabled(false);
                upgradePrice.text = "MAX";
            }
        }
        
    }

    void BuyEnzyme()
    {
        
            
    }
}
