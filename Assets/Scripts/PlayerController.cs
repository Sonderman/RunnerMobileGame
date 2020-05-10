using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    delegate void TurnDelegate(); // Delegate : Fonksiyon pointeri
    TurnDelegate turnDelegate;
    public float moveSpeed=1;
    private bool lookingRight=true;
    GameManager gameManager;
    Animator anim;
    public Transform rayOrigin;
    public ParticleSystem effect;
    public Text scoreText, hScoreText;

    public int Score { get; private set; }
    public int HScore { get; private set; }

    private void Start()
    {
    #if UNITY_EDITOR
        turnDelegate = TurnPlayerUsingKeyboard;
    #endif
    #if UNITY_ANDROID
        turnDelegate = TurnPlayerUsingTouch;
    #endif
        gameManager = GameObject.FindObjectOfType<GameManager>();
        anim = gameObject.GetComponent<Animator>();
        
        LoadHscore();
    }

    private void LoadHscore()
    {
        HScore = PlayerPrefs.GetInt("hiscore");
        hScoreText.text = HScore.ToString();
    }

    void Update()
    {
        moveSpeed *= 1.0001f;
        if(!gameManager.gameStarted)return;
        
        anim.SetTrigger("GameStarted");
        //transform.position += transform.forward * Time.deltaTime*moveSpeed;
        transform.Translate(new Vector3(0, 0, 1) * Time.deltaTime * moveSpeed);


        turnDelegate();

        CheckFalling();
    }
    private void TurnPlayerUsingTouch()
    {
        if (Input.touchCount >0 && Input.GetTouch(0).phase==TouchPhase.Began)
        {
            Turn();
        }
    }
    private void TurnPlayerUsingKeyboard()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Turn();
        }
    }

    float elapsedTime = 0;
    float freq = 1f / 5f;
    private void CheckFalling()
    {
        if((elapsedTime += Time.deltaTime)> freq)
        {
            if (!Physics.Raycast(rayOrigin.position,new Vector3(0,-1,0)) )
            {
            anim.SetTrigger("Falling");
            gameManager.RestartGame();
                elapsedTime = 0;
            }
        }
        
    }

    private void Turn()
    {
        if (lookingRight)
        {
            transform.Rotate(new Vector3(0, 1, 0),-90);
        }
        else
        {
            transform.Rotate(new Vector3(0, 1, 0), 90);
        }
        lookingRight = !lookingRight;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("Crystal"))
        {
            CreateEffect();
            MakeScore();
            Destroy(other.gameObject);
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        Destroy(collision.gameObject, 2f);
    }

    private void CreateEffect()
    {
       var vfx = Instantiate(effect,transform);
        Destroy(vfx, 1f);
    }

    private void MakeScore()
    {
        Score++;
        scoreText.text = Score.ToString();
        if (Score > HScore)
        {
            HScore = Score;
            hScoreText.text = HScore.ToString();
            PlayerPrefs.SetInt("hiscore",HScore);
        }
    }
}
