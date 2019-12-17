using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DistanceCalculator : MonoBehaviour
{
    public TextMeshProUGUI distanceText;
    int distance;

    public float speed;
    public float thresh = 0.5f;
    float timer;
    
    // Start is called before the first frame update
    void Start()
    {
        distance = 0;
        distanceText.text = "Score: " + distance.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        if(!GameManager.Instance.playerStarted)
        {
            distance = 0;
            distanceText.text = "Score: " + distance.ToString();
        }
        if (GameManager.Instance.playerStarted && !GameManager.Instance.playerDead && !GameManager.Instance.playerRestart)
        {
            timer += speed * Time.deltaTime;
            if (timer > thresh)
            {
                timer = 0;
                distance++;
                distanceText.text = "Score: "+distance.ToString();
            }
        }
        else
        {
            GameManager.Instance.currScore = distance;
        }
    }
}
