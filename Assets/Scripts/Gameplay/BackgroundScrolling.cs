using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundScrolling : MonoBehaviour
{
    public float speed;

    public float endX;
    public float startX;
    
    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance.playerStarted && !GameManager.Instance.playerDead && !GameManager.Instance.playerRestart)
        {
            transform.Translate(Vector2.left * speed * Time.deltaTime);

            if (transform.position.x <= endX)
            {
                Vector3 pos = new Vector3(startX, transform.position.y, transform.position.z);
                transform.position = pos;
            }
        }
    }
}
