using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    private delegate void TurnDelegate(); // Delegate : Fonksiyon pointeri

    private TurnDelegate _turnDelegate;
    public float moveSpeed=1;
    private bool _lookingRight=true;
    private GameManager _gameManager;
    private Animator _anim;
    public Transform rayOrigin;
    public ParticleSystem effect;
    public Text scoreText, hScoreText;

    private int Score { get; set; }
    private int HScore { get; set; }

    private void Start()
    {
    #if UNITY_EDITOR
        _turnDelegate = TurnPlayerUsingKeyboard;
    #endif
    #if UNITY_ANDROID
        turnDelegate = TurnPlayerUsingTouch;
    #endif
        _gameManager = FindObjectOfType<GameManager>();
        _anim = gameObject.GetComponent<Animator>();
        
        LoadHscore();
    }

    private void LoadHscore()
    {
        HScore = PlayerPrefs.GetInt("hiscore");
        hScoreText.text = HScore.ToString();
    }

    private void Update()
    {
        moveSpeed *= 1.0001f;
        if(!_gameManager.GameStarted)return;
        
        _anim.SetTrigger("GameStarted");
        //transform.position += transform.forward * Time.deltaTime*moveSpeed;
        transform.Translate(new Vector3(0, 0, 1) * Time.deltaTime * moveSpeed);


        _turnDelegate();

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

    private float _elapsedTime = 0;
    private const float Freq = 1f / 5f;

    private void CheckFalling()
    {
        if((_elapsedTime += Time.deltaTime)> Freq)
        {
            if (!Physics.Raycast(rayOrigin.position,new Vector3(0,-1,0)) )
            {
            _anim.SetTrigger("Falling");
            _gameManager.RestartGame();
                _elapsedTime = 0;
            }
        }
        
    }

    private void Turn()
    {
        if (_lookingRight)
        {
            transform.Rotate(new Vector3(0, 1, 0),-90);
        }
        else
        {
            transform.Rotate(new Vector3(0, 1, 0), 90);
        }
        _lookingRight = !_lookingRight;
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
