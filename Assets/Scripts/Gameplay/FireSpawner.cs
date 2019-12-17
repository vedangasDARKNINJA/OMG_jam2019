using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireSpawner : MonoBehaviour
{
    public Transform campfire;
    BackgroundScrolling campFire_scroll;
    public Vector3 spawnPosition;
    public float timeBtwSpawns;
    public float timer;
    public Vector2 timeLimits;
    public float increment;


    // Start is called before the first frame update
    void Start()
    {
        timeBtwSpawns = timeLimits.x;
        campfire.position = spawnPosition;
        campFire_scroll = campfire.gameObject.GetComponent<BackgroundScrolling>();
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance.playerStarted && !GameManager.Instance.playerDead && !GameManager.Instance.playerRestart)
        {
            if (campfire.position.x < campFire_scroll.startX)
            {
                timer += Time.deltaTime;
                if (timer >= timeBtwSpawns)
                {
                    timer = 0;
                    campfire.position = spawnPosition;
                    timeBtwSpawns += increment;
                    timeBtwSpawns = Mathf.Clamp(timeBtwSpawns, timeLimits.x, timeLimits.y);
                }
            }
        }
    }
}
