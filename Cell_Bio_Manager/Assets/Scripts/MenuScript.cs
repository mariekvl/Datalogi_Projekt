using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class MenuScript : MonoBehaviour
{

    public UIDocument uIDocument;
    

    private Button startButton;
    private Button exitButton;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        startButton = uIDocument.rootVisualElement.Q<Button>("StartButton");
        startButton.clicked += startGame;
        exitButton = uIDocument.rootVisualElement.Q<Button>("ExitButton");
        exitButton.clicked += exitGame;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void startGame()
    {
        SceneManager.LoadScene("MainScene");
        SceneManager.SetActiveScene(SceneManager.GetSceneByName("MainScene"));
    }

    void exitGame()
    {
        print("Exiting game...");
        Application.Quit();
    }
}
