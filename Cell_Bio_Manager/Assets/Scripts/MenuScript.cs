using System;
using UnityEngine;
using UnityEngine.InputSystem;
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

    

    void startGame()
    {
        
        
        

        SceneManager.LoadScene("MainScene", LoadSceneMode.Single);

        
        
    }

    void exitGame()
    {
        print("Exiting game...");
        Application.Quit();
    }
}
