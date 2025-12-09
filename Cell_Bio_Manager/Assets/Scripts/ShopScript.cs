using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class ShopScript : MonoBehaviour
{
    public UIDocument uIDocument;
    public PointManager pointManager;
    public List<Molecule> moleculedata;
    public int maxLevel = 8;

    private Label atpScore;
    private Label pyruvateScore;


    private Button backButton;

    private Button winButton;
    private Label winPrice;

    private Button buyUpgrade;

    private Button buyEnzyme1;
    private Button buyEnzyme2;
    private Button buyEnzyme3;
    private Button buyEnzyme4;
    private Button buyEnzyme5;
    private Button buyEnzyme6;
    private Button buyEnzyme7;
    private Button buyEnzyme8;
    private Button buyEnzyme9;


    private Label upgradePrice;

    private Label priceEnzyme1;
    private Label priceEnzyme2;
    private Label priceEnzyme3;
    private Label priceEnzyme4;
    private Label priceEnzyme5;
    private Label priceEnzyme6;
    private Label priceEnzyme7;
    private Label priceEnzyme8;
    private Label priceEnzyme9;
    

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
        buyEnzyme1.clicked += () => BuyEnzyme(0,priceEnzyme1);
        buyEnzyme2 = uIDocument.rootVisualElement.Q<Button>("BuyEnzyme2");
        buyEnzyme2.clicked += () => BuyEnzyme(1, priceEnzyme2);
        buyEnzyme3 = uIDocument.rootVisualElement.Q<Button>("BuyEnzyme3");
        buyEnzyme3.clicked += () => BuyEnzyme(2, priceEnzyme3);
        buyEnzyme4 = uIDocument.rootVisualElement.Q<Button>("BuyEnzyme4");
        buyEnzyme4.clicked += () => BuyEnzyme(3, priceEnzyme4);
        buyEnzyme5 = uIDocument.rootVisualElement.Q<Button>("BuyEnzyme5");
        buyEnzyme5.clicked += () => BuyEnzyme(4, priceEnzyme5);
        buyEnzyme6 = uIDocument.rootVisualElement.Q<Button>("BuyEnzyme6");
        buyEnzyme6.clicked += () => BuyEnzyme(5, priceEnzyme6);
        buyEnzyme7 = uIDocument.rootVisualElement.Q<Button>("BuyEnzyme7");
        buyEnzyme7.clicked += () => BuyEnzyme(6, priceEnzyme7);
        buyEnzyme8 = uIDocument.rootVisualElement.Q<Button>("BuyEnzyme8");
        buyEnzyme8.clicked += () => BuyEnzyme(7, priceEnzyme8);
        buyEnzyme9 = uIDocument.rootVisualElement.Q<Button>("BuyEnzyme9");
        buyEnzyme9.clicked += () => BuyEnzyme(8, priceEnzyme9);



        upgradePrice = uIDocument.rootVisualElement.Q<Label>("UpgradePrice");
        upgradePrice.text = pointManager.upgradePrice.ToString();

        priceEnzyme1 = uIDocument.rootVisualElement.Q<Label>("PriceEnzyme1");
        priceEnzyme1.text = setPrice(0).ToString();
        priceEnzyme2 = uIDocument.rootVisualElement.Q<Label>("PriceEnzyme2");
        priceEnzyme2.text = setPrice(1).ToString();
        priceEnzyme3 = uIDocument.rootVisualElement.Q<Label>("PriceEnzyme3");
        priceEnzyme3.text = setPrice(2).ToString();
        priceEnzyme4 = uIDocument.rootVisualElement.Q<Label>("PriceEnzyme4");
        priceEnzyme4.text = setPrice(3).ToString();
        priceEnzyme5 = uIDocument.rootVisualElement.Q<Label>("PriceEnzyme5");
        priceEnzyme5.text = setPrice(4).ToString();
        priceEnzyme6 = uIDocument.rootVisualElement.Q<Label>("PriceEnzyme6");
        priceEnzyme6.text = setPrice(5).ToString();
        priceEnzyme7 = uIDocument.rootVisualElement.Q<Label>("PriceEnzyme7");
        priceEnzyme7.text = setPrice(6).ToString();
        priceEnzyme8 = uIDocument.rootVisualElement.Q<Label>("PriceEnzyme8");
        priceEnzyme8.text = setPrice(7).ToString();
        priceEnzyme9 = uIDocument.rootVisualElement.Q<Label>("PriceEnzyme9");
        priceEnzyme9.text = setPrice(8).ToString();
    }

    int setPrice(int index)
    {
        return moleculedata[index].ATPValue * 10;
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

    void BuyEnzyme(int level, Label price)
    {
        if (int.Parse(price.text) <= int.Parse(atpScore.text))
        {
            pointManager.workerLevel = level;
            pointManager.spawnNewWorker = true;
            int newAtpScore = int.Parse(atpScore.text) - int.Parse(price.text);
            atpScore.text = newAtpScore.ToString();
            pointManager.atpScore = newAtpScore;
        }
    }
}
