using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    public Vector3 startPosition;
    public float speed = 30;
    [SerializeField] int servingPlayer = 1;
    
    [SerializeField] int gameOverScore = 1;

    void Start()
    {
        startPosition = transform.position;
        //ServingLeft();
        //ServingRight();
        //Launch();
    }
    // if the left side is serving, the ball should go to the right
    void ServingLeft()
    {
        // Initial Velocity
        float y = Random.Range(-1,2);
        //GetComponent<Rigidbody2D>().velocity = Vector2.right * speed;
        GetComponent<Rigidbody2D>().velocity = new Vector2(1,y)*speed;
    }

    //if the right side is serving, the ball should go to the left
    void ServingRight()
    {
        // Initial Velocity
        float y = Random.Range(-1,2);
        //GetComponent<Rigidbody2D>().velocity = Vector2.left * speed;
        GetComponent<Rigidbody2D>().velocity = new Vector2(-1, y) * speed;
    }

    public void Reset()
    {
        GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        transform.position = startPosition;
        //Launch();
    }

    public void Launch()
    {
        GameObject.Find("GameManager").GetComponent<GameManager>().gameState = "play";
        if (servingPlayer == 1)
        {
            ServingLeft();
            //Debug.Log("servingleft");
        }
        else
        {
            ServingRight();
            //Debug.Log("servingright");
        }
    }
    //void RandomLaunch()
    //{
    //    float x = Random.Range(0, 2) == 0 ? -1 : 1;
    //    float y = Random.Range(0, 2) == 0 ? -1 : 1;
    //    GetComponent<Rigidbody2D>().velocity = new Vector2(x, y) * speed;
    //}
    

    float hitFactor(Vector2 ballPos, Vector2 racketPos,
                float racketHeight)
    {
        // ascii art:
        // ||  1 <- at the top of the racket
        // ||
        // ||  0 <- at the middle of the racket
        // ||
        // || -1 <- at the bottom of the racket
        return (ballPos.y - racketPos.y) / racketHeight;
    }
    void OnCollisionEnter2D(Collision2D col)
    {
        // Hit the left Racket?
        if (col.gameObject.name == "RacketLeft")
        {
            FindObjectOfType<SoundControl>().PaddleSound();
            // Calculate hit Factor
            float y = hitFactor(transform.position,
                                col.transform.position,
                                col.collider.bounds.size.y);

            // Calculate direction, make length=1 via .normalized
            Vector2 dir = new Vector2(1, y).normalized;

            // Set Velocity with dir * speed
            GetComponent<Rigidbody2D>().velocity = dir * speed;
        }

        // Hit the right Racket?
        if (col.gameObject.name == "RacketRight")
        {
            FindObjectOfType<SoundControl>().PaddleSound();
            // Calculate hit Factor
            float y = hitFactor(transform.position,
                                col.transform.position,
                                col.collider.bounds.size.y);

            // Calculate direction, make length=1 via .normalized
            Vector2 dir = new Vector2(-1, y).normalized;

            // Set Velocity with dir * speed
            GetComponent<Rigidbody2D>().velocity = dir * speed;
        }

        // Hit the wall ?  
        if (col.gameObject.tag == "wall")
        {
            FindObjectOfType<SoundControl>().WallSound();
        }

        // Hit the scoreleft wall ? 
        if (col.gameObject.tag == "scoreleft")
        {

             FindObjectOfType<SoundControl>().ScoreSound();
             servingPlayer = 1;
             GameObject.Find("GameManager").GetComponent<GameManager>().Player2Scored();
             //Debug.Log("Player 2 scored..");
             if(GameObject.Find("GameManager").GetComponent<GameManager>().Player2Score == gameOverScore)
             {
              WinnerP2();
             }
             else
             {
                GameObject.Find("GameManager").GetComponent<GameManager>().gameState = "serve1";
                GameObject.Find("GameManager").GetComponent<GameManager>().P1ServeText.enabled = true;
                GameObject.Find("GameManager").GetComponent<GameManager>().PressEnterServeText.enabled = true;
             }
        }

        // Hit the scoreright wall ? 
        if (col.gameObject.tag == "scoreright")
        {
            FindObjectOfType<SoundControl>().ScoreSound();
            servingPlayer = 2;
            GameObject.Find("GameManager").GetComponent<GameManager>().Player1Scored();
            //Debug.Log("Player 1 scored..");
            if(GameObject.Find("GameManager").GetComponent<GameManager>().Player1Score == gameOverScore)
            {
                WinnerP1();
            }
            else
            {
                GameObject.Find("GameManager").GetComponent<GameManager>().gameState = "serve2";
                GameObject.Find("GameManager").GetComponent<GameManager>().P2ServeText.enabled = true;
                GameObject.Find("GameManager").GetComponent<GameManager>().PressEnterServeText.enabled = true;
            }
        }
       
    }

    void WinnerP1()
    {
        servingPlayer = 2;
        GameObject.Find("GameManager").GetComponent<GameManager>().winnerPlayer = 1;
        GameObject.Find("GameManager").GetComponent<GameManager>().gameState = "done";
        GameObject.Find("GameManager").GetComponent<GameManager>().P1winText.enabled = true;
        GameObject.Find("GameManager").GetComponent<GameManager>().restartText.enabled = true;
    }
    void WinnerP2()
    {
        servingPlayer = 1;
        GameObject.Find("GameManager").GetComponent<GameManager>().winnerPlayer = 2;
        GameObject.Find("GameManager").GetComponent<GameManager>().gameState = "done";
        GameObject.Find("GameManager").GetComponent<GameManager>().P2winText.enabled = true;
        GameObject.Find("GameManager").GetComponent<GameManager>().restartText.enabled = true;
    }
}
