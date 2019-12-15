using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public const float maxHeight = 3.74f;

    public float gravity;
    public float upthrust;
    public float horzSpeed;
    public float horzBackSpeed;

    private Vector2 speeds;

    public Vector2 horzBounds;

    public float fuelRate = 0.5f;
    [SerializeField]
    private float fuel;

    Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        fuel = 100;
    }

    // Update is called once per frame
    void Update()
    {
        float v = Input.GetAxisRaw("Vertical");
        float h = Input.GetAxisRaw("Horizontal");
        if(fuel>0)
        {
            if(v>0)
            {
                speeds.y = upthrust;
            }
            else
            {
                speeds.y = gravity;
            }

            if(h>0)
            {
                speeds.x = horzSpeed;
            }
            else if(transform.position.x > horzBounds.x+0.5f)
            {
                speeds.x = horzBackSpeed;
            }
            else
            {
                speeds.x = 0;
            }
        }
    }

    private void FixedUpdate()
    {
        if(speeds.x == 0)
        {
            Vector2 vel = rb.velocity;
            vel.x = 0;
            rb.velocity = vel;
        }
        rb.AddForce(speeds, ForceMode2D.Force);
        Vector3 current = transform.position;
        current.x = Mathf.Clamp(current.x, horzBounds.x, horzBounds.y);
        transform.position = current;
        if (fuel > 0)
        {
            fuel -= fuelRate * Time.fixedDeltaTime;
        }
    }
}
