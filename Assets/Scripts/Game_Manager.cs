using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Game_Manager : MonoBehaviour
{
    public int whoTurn; //0 = x and 1 = o
    public int turnCount; // counts the number of turn played
    public GameObject[] turnIcons; //displays whos turn it is
    public Sprite[] playerIcons; //0=x icon 1=y icon
    public Button[] tictactoeSpaces; //playable grid cell for our game
    public int[] markedSpaces; //ID's which space was marked by which player.
    public Text winnerText; // hold the text of the winner text;
    public GameObject[] winningLine; //hold of the different line for show that the winner.
    public GameObject winnerPanel;
    public int xPlayerScore;
    public int oPlayerScore;
    public Text xPlayerScoreText;
    public Text oPlayerScoreText;

    public Sprite gridCell_Img;
    public Button xPlayerBtn;
    public Button oPlayerBtn;
    public GameObject catImage;
    public AudioSource buttonClickAudio;

    // Start is called before the first frame update
    void Start()
    {
        GameSetup();
    }

    void GameSetup()
    {
        whoTurn = 0;
        turnCount = 0;
        turnIcons[0].SetActive(true);
        turnIcons[1].SetActive(false);
        for(int i=0; i<tictactoeSpaces.Length; i++)
        {
            tictactoeSpaces[i].interactable = true;
            tictactoeSpaces[i].GetComponent<Image>().sprite = gridCell_Img;
        }

        for(int i=0; i < markedSpaces.Length; i++)
        {
            markedSpaces[i] = -100;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void TicTacToeButton(int WhichNumber) // this fun call on the each grid cell button.
    {
        //this is for not working the btn when this fun called
        xPlayerBtn.interactable = false;
        oPlayerBtn.interactable = false;

        //this is for when you clicked the btn then which sprite display in grid cell
        tictactoeSpaces[WhichNumber].image.sprite = playerIcons[whoTurn];
        tictactoeSpaces[WhichNumber].interactable = false;


        markedSpaces[WhichNumber] = whoTurn + 1; //this is for getting num for calculation who is winner.
        turnCount++; // in

        if (turnCount >4)
        {
            bool isWinner = WinnerCheck();

            if (turnCount == 9 && isWinner == false)
            {
                Cat();
            }

        }

        if (whoTurn == 0)// when 0 means x player's turn is finished
        {
            whoTurn = 1;
            turnIcons[0].SetActive(false);
            turnIcons[1].SetActive(true);
        }
        else // when 1 means o player's turn is finished
        {
            whoTurn = 0;
            turnIcons[0].SetActive(true);
            turnIcons[1].SetActive(false);
        }
    }

    bool WinnerCheck() //cheking winner position in grid colum and row
    {
        int s1 = markedSpaces[0] + markedSpaces[1] + markedSpaces[2];
        int s2 = markedSpaces[3] + markedSpaces[4] + markedSpaces[5];
        int s3 = markedSpaces[6] + markedSpaces[7] + markedSpaces[8];
        int s4 = markedSpaces[0] + markedSpaces[3] + markedSpaces[6];
        int s5 = markedSpaces[1] + markedSpaces[4] + markedSpaces[7];
        int s6 = markedSpaces[2] + markedSpaces[5] + markedSpaces[8];
        int s7 = markedSpaces[0] + markedSpaces[4] + markedSpaces[8];
        int s8 = markedSpaces[2] + markedSpaces[4] + markedSpaces[6];
        var solutions = new int[] { s1, s2, s3, s4, s5, s6, s7, s8 };

        for (int i = 0; i < solutions.Length; i++)
        {
            if (solutions[i] == 3 * (whoTurn + 1))
            {
                Debug.Log("Player " + whoTurn + " Won !");
                WinnerDisplay(i);
                return true;
            }
        }

        return false;
    }


    void WinnerDisplay(int indexIn) // Increament score;
    {
        winnerPanel.gameObject.SetActive(true);
        if (whoTurn == 0)
        {
            xPlayerScore++;
            xPlayerScoreText.text = xPlayerScore.ToString();
            winnerText.text = "Player X wins !";
        }
        else if (whoTurn == 1)
        {
            oPlayerScore++;
            oPlayerScoreText.text = oPlayerScore.ToString();
            winnerText.text = "Player O wins !";
        }

        winningLine[indexIn].SetActive(true);
        
    }

    public void Rematch() // this is fun start the match again but not erase your score
    {
        GameSetup();
        for(int i=0; i<winningLine.Length; i++)
        {
            winningLine[i].SetActive(false);
        }
        winnerPanel.SetActive(false);
        //this is for working the btn when this fun called
        xPlayerBtn.interactable = true;
        oPlayerBtn.interactable = true;
        catImage.SetActive(false);
    }

    public void Restart() // this is fun reload the match again.
    {
        Rematch();
        xPlayerScore = 0;
        oPlayerScore = 0;
        xPlayerScoreText.text = "0";
        oPlayerScoreText.text = "0";
    }

    public void SwitchPlayer(int whichPlayer) //this fun's purpose is when game is start who is play first.
    {
        if (whichPlayer == 0)
        {
            whoTurn = 0;
            turnIcons[0].SetActive(true);
            turnIcons[1].SetActive(false);
        }
        else if (whichPlayer == 1)
        {
            whoTurn = 1;
            turnIcons[0].SetActive(false);
            turnIcons[1].SetActive(true);
        }
    }

    void Cat() //for game is Over.
    {
        winnerPanel.SetActive(true);
        catImage.SetActive(true);
        winnerText.text = "Game Over";
    }

    public void PlayButtonClick()
    {
        buttonClickAudio.Play();
    }
}
