using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

    [Header("Ball")]
    public GameObject ball;

    [Header("Player1")]
    public GameObject player1Paddle;
    public GameObject player1Goal;

    [Header("Player2")]
    public GameObject player2Paddle;
    public GameObject player2Goal;

    [Header("Score UI")]
    public Text Player1Text;
    public Text Player2Text;

    [Header("Welcome UI")]
    public Text WelcomeGameText;
    public Text PressEnterText;

    [Header("Player1 serveUI")]
    public Text P1ServeText;
    public Text PressEnterServeText;
    
    [Header("Player2 serveUI")]
    public Text P2ServeText;

    public Text P1winText;
    public Text P2winText;
    public Text restartText;
    public int winnerPlayer;

    public int Player1Score { get; private set;}
    public int Player2Score { get; private set;}

    FPSDisplay fpsdisplay;
    private bool fps;

    public string gameState = "start";

	void Update () {

        if (Input.GetKeyDown(KeyCode.Return))
        {
            if(gameState == "start")
            {
                gameState = "serve1";
                WelcomeText();
                ball.GetComponent<Ball>().Launch();
            }
            else if(gameState == "serve1"){
                Player1ServeText();
                ball.GetComponent<Ball>().Launch();
            }else if (gameState == "serve2")
            {
                Player2ServeText();
                ball.GetComponent<Ball>().Launch();
            }else if (gameState == "done")
            {
                PlayerWon();
                if(winnerPlayer == 1)
                {
                    gameState = "serve2";
                }
                else
                {
                    gameState = "serve1";
                }
                ball.GetComponent<Ball>().Launch();
            }    
        }
    }

    public void WelcomeText()
    {
        WelcomeGameText.enabled = false;
        PressEnterText.enabled = false;
    }
    public void Player1ServeText()
    {
        P1ServeText.enabled = false;
        PressEnterServeText.enabled = false;
    }
    public void Player2ServeText()
    {
        P2ServeText.enabled = false;
        PressEnterServeText.enabled = false;
    }
    public void PlayerWon()
    {
        P1winText.enabled = false;
        P2winText.enabled = false;
        restartText.enabled = false;
        Player1Score = 0;
        Player2Score = 0;
        Player1Text.text = Player1Score.ToString();
        Player2Text.text = Player2Score.ToString();
    }

    public void Player1Scored()
    {
        Player1Score++;
        Player1Text.text = Player1Score.ToString();
        ResetPosition();
    }

    public void Player2Scored()
    {
        Player2Score++;
        Player2Text.text = Player2Score.ToString();
        ResetPosition();
    }

    private void ResetPosition()
    {
        ball.GetComponent<Ball>().Reset();
        player1Paddle.GetComponent<MoveRacket>().Reset();
        player2Paddle.GetComponent<MoveRacket>().Reset();
    }
}
