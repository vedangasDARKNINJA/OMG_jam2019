using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public GameObject textToggle;
    public TextMeshProUGUI clickToText;

    public TextMeshProUGUI coinText;
    public int coins;

    public bool playerStarted;
    public bool playerDead;
    public bool playerRestart;

    public int highScore = 0;
    public int currScore = 0;

    void Awake()
    {
        if(Instance==null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        Screen.SetResolution(1024, 768, false);
    }

    void Start()
    {
        RestartGame();
    }



    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            if (!playerStarted)
            {
                playerStarted = true;
                currScore = 0;
                textToggle.SetActive(false);
            }
            else if (playerStarted && !playerDead && playerRestart)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
                RestartGame();
            }
        }

        if(playerDead)
        {
            if (currScore > highScore)
            {
                clickToText.text = "New High Score!\nClick to Restart!";
                highScore = currScore;
            }
            else
            {
                clickToText.text = "Better luck next time...\nClick to Restart!";
            }
            textToggle.SetActive(true);
            playerRestart = true;
            playerDead = false;
        }
    }

    void RestartGame()
    {
        clickToText = textToggle.GetComponent<TextMeshProUGUI>();
        playerDead = false;
        playerRestart = false;
        playerStarted = false;
        clickToText.text = "Click to Start";
        textToggle.SetActive(true);
    }
}
