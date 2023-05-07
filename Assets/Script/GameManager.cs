using Assets.Script;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Dropdown sizeInput;
    public Chessboard chessboard;
    public static GameManager instance;
    public GameObject endGameUI;
    public Text endGameUIText;

    bool openAI;
    int boardSize;
    int playerChooseTurn;
    int AILevel;

    private void Start()
    {
        instance = this;
    }

    public void SetAI(bool i)
    {
        openAI = i;
    }

    public void StartGame()
    {
        boardSize = sizeInput.value + 3;
        if (openAI)
            chessboard.Init(boardSize, playerChooseTurn, openAI, AILevel);
        else
            chessboard.Init(boardSize, 1, openAI, 1);
    }
    public void SetPlayerChooseTurn(int i)
    {
        playerChooseTurn = i;
    }
    public void SetAILevel(int i)
    {
        AILevel = i;
    }

    public void ResartGame()
    {
        SceneManager.LoadScene(0);
    }

    public void QuitGame()
    {
        Application.Quit();
        UnityEditor.EditorApplication.isPlaying = false;
    }

    public void EndGame()
    {

    }

}
