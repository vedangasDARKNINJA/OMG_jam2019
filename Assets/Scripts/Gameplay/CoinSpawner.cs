using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinSpawner : MonoBehaviour
{
    public Vector2 verticalLimts;
    public Vector2Int spawnSizeLimits;
    public Vector2 offsets;
    public GameObject spawn;


    float timer;
    public float timeBtwSpawns;
    public Vector2 randomSpawnRange;

    // Start is called before the first frame update
    void Start()
    {
        timer = -10;    
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance.playerStarted && !GameManager.Instance.playerDead && !GameManager.Instance.playerRestart)
        {
            timer += Time.deltaTime;
            if (timer >= timeBtwSpawns)
            {
                timer = 0;
                timeBtwSpawns = Random.Range(randomSpawnRange.x, randomSpawnRange.y);
                SpawnCoins();
            }
        }
    }

    void SpawnCoins()
    {
        int length = Random.Range(5, 10);
        int height = Random.Range(spawnSizeLimits.x, spawnSizeLimits.y);
        float  startY = Random.Range(verticalLimts.x, verticalLimts.y);
        for(int i=0;i<height;i++)
        {
            for(int j=0;j<length;j++)
            {
                Instantiate(spawn, new Vector3(transform.position.x + j*offsets.x, startY- i*offsets.y, transform.position.z), Quaternion.identity,transform);
            }
            if(startY - i * offsets.y < verticalLimts.x)
            {
                break;
            }
        }
    }
}
