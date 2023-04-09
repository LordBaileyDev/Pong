using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Xml.Serialization;
using UnityEngine.Rendering.UI;

public class GameManager : MonoBehaviour
{
    [Header("Player's Paddles")]
    [SerializeField] Rigidbody2D playerRed;
    [SerializeField] Rigidbody2D playerBlue;

    [Tooltip("Speed at which the paddles move")] [SerializeField] int playerSpeed = 9;

    [SerializeField] GameObject ballPrefab;

    [Header("UI Elements")]
    [SerializeField] TMP_Text scoreRed;
    [SerializeField] TMP_Text scoreBlue;
    [SerializeField] TMP_Text countDownTimer;

    int pointRed = 0;
    int pointBlue = 0;

    [Header("Goal Effects")]
    [SerializeField] ParticleSystem redGoal;
    [SerializeField] ParticleSystem blueGoal;

    bool pandelsMove = true;

    int ballAmountMax = 1;
    [HideInInspector] public int ballAmount = 0;

    bool gameStarted = false;

    [SerializeField] GameObject Tutorial;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !gameStarted)
        {
            gameStarted = true;
            StartCoroutine(RoundCountDownTimer());
            Destroy(Tutorial);
        }

        PlayerControls();

        if (Input.GetKeyDown(KeyCode.F1))
        {
            pandelsMove = !pandelsMove;
        }

        if (Input.GetKeyDown(KeyCode.F2))
        {
            ballAmountMax++;
        }
        else if (Input.GetKeyDown(KeyCode.F3))
        {
            if(ballAmountMax <= 1) { return; }

            ballAmountMax--;
        }
    }

    void PlayerControls()
    {
        // Red Player
        if (Input.GetKey(KeyCode.LeftArrow) && playerRed.transform.position.y < 4.4f)
        {
            playerRed.velocity = Vector2.up * playerSpeed;

            if (pandelsMove)
            playerRed.transform.rotation = Quaternion.Euler(0, 0, -45f);
        }
        else if (Input.GetKey(KeyCode.RightArrow) && playerRed.transform.position.y > -4.4f)
        {
            playerRed.velocity = -Vector2.up * playerSpeed;

            if (pandelsMove)
            playerRed.transform.rotation = Quaternion.Euler(0, 0, 45f);
        }
        else
        {
            playerRed.velocity = Vector2.zero;
            
            playerRed.transform.rotation = Quaternion.identity;
        }

        // Blue Player
        if (Input.GetKey(KeyCode.Z) && playerBlue.transform.position.y < 4.4f)
        {
            playerBlue.velocity = Vector2.up * playerSpeed;

            if(pandelsMove)
            playerBlue.transform.rotation = Quaternion.Euler(0, 0, 45f);
        }
        else if(Input.GetKey(KeyCode.C) && playerBlue.transform.position.y > -4.4f)
        {
            playerBlue.velocity = -Vector2.up * playerSpeed;
            
            if (pandelsMove)
            playerBlue.transform.rotation = Quaternion.Euler(0, 0, -45f);
        }
        else
        {
            playerBlue.velocity = Vector2.zero;
            
            playerBlue.transform.rotation = Quaternion.identity;
        }
    }

    public void AddPoint(int team)
    {
        if(team == 0)
        {
            pointRed++;
            redGoal.Play();
        }
        else
        {
            pointBlue++;
            blueGoal.Play();
        }

        scoreRed.text = "" + pointRed;
        scoreBlue.text = "" + pointBlue;

        StartCoroutine(RoundCountDownTimer());
    }

    IEnumerator RoundCountDownTimer()
    {
        countDownTimer.text = "3";
        yield return new WaitForSeconds(1);
        countDownTimer.text = "2";
        yield return new WaitForSeconds(1);
        countDownTimer.text = "1";
        yield return new WaitForSeconds(1);
        countDownTimer.text = "GO";
        yield return new WaitForSeconds(0.5f);
        countDownTimer.text = "";
        SpawnBall();
    }

    void SpawnBall()
    {
        for(int i = 0; ballAmount < ballAmountMax; i++)
        {
            int choice = Random.Range(0, 2);
            GameObject ball = Instantiate(ballPrefab,Vector3.zero,Quaternion.identity);
            Rigidbody2D ballRB = ball.GetComponent<Rigidbody2D>();
            if(choice == 0)
            {
                ballRB.AddForce(new Vector3(0.33f,Random.Range(-0.33f,0.33f)) * Random.Range(400,800));
            }
            else
            {
                ballRB.AddForce(new Vector3(-0.33f, Random.Range(-0.33f, 0.33f)) * Random.Range(400, 800));
            }

            ballAmount++;
        }
    }
}