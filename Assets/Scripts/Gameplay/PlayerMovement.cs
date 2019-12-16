using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    public Slider fuelSlider;

    public float gravity;
    public float upthrust;
    public float horzSpeed;
    public float horzBackSpeed;
    public float maxVelocity = 2f;

    private Vector2 speeds;

    public Vector2 horzBounds;

    public float fuelRate = 0.5f;
    public float fuelFactor = 1.2f;
    public float maxFuel;
    [SerializeField]
    private float fuel;
    [SerializeField]
    private bool extraFuelRate;
    Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("player Start caled");
        rb = GetComponent<Rigidbody2D>();

        fuel = maxFuel;
        fuelSlider = GameObject.FindGameObjectWithTag("FuelSlider").GetComponent<Slider>();
        fuelSlider.value = 1;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance.playerStarted && !GameManager.Instance.playerDead && !GameManager.Instance.playerRestart)
        {
            float v = Input.GetAxisRaw("Vertical");
            float h = Input.GetAxisRaw("Horizontal");
            if (fuel > 0)
            {
                extraFuelRate = false;
                if (v > 0)
                {
                    if (transform.position.y < 2.5f)
                    {
                        speeds.y = upthrust;
                        extraFuelRate = true;
                    }
                    else
                    {
                        speeds.y = 0;
                    }
                }
                else if(transform.position.y>=2.5)
                {
                    speeds.y = -10;
                }
                else
                {
                    speeds.y = gravity;
                }

                if (h > 0)
                {
                    speeds.x = horzSpeed;
                    extraFuelRate = true;
                }
                else if (transform.position.x > horzBounds.x + 0.5f)
                {
                    speeds.x = horzBackSpeed;
                }
                else
                {
                    speeds.x = 0;
                }
            }
            else
            {
                speeds.x = 0;
                speeds.y = gravity;
                GameManager.Instance.playerDead = true;
            }
        }
    }

    private void FixedUpdate()
    {
        if (speeds.x == 0)
        {
            Vector2 vel = rb.velocity;
            vel.x = 0;
            rb.velocity = vel;
        }
        if (speeds.y == 0)
        {
            Vector2 vel = rb.velocity;
            vel.y = 0;
            rb.velocity = vel;
        }
        rb.AddForce(speeds, ForceMode2D.Force);
        Vector2 velo = rb.velocity;
        velo.x = Mathf.Clamp(velo.x, -maxVelocity, maxVelocity);
        rb.velocity = velo;

        Vector3 current = transform.position;
        current.x = Mathf.Clamp(current.x, horzBounds.x, horzBounds.y);
        current.y = Mathf.Clamp(current.y, current.y, 2.5f);
        transform.position = current;
        if (GameManager.Instance.playerStarted && !GameManager.Instance.playerDead && !GameManager.Instance.playerRestart)
        {
            if (fuel > 0)
            {
                fuel -= fuelRate * Time.fixedDeltaTime;
                if (extraFuelRate)
                {
                    fuel -= fuelFactor * fuelRate * Time.fixedDeltaTime;
                }
                fuelSlider.value = fuel / maxFuel;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("PowerUp"))
        {
            fuel = maxFuel;
            rb.AddForce(3 * Vector2.up, ForceMode2D.Impulse);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Ground"))
        {
            AudioManager.Instance.Play("Death");
            GameManager.Instance.playerDead = true;
            speeds.x = 0;
            speeds.y = gravity;
        }
    }
}
