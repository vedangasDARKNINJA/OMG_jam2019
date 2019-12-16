using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class CoinScript : MonoBehaviour
{

    public GameObject ParticleEffectObject;
    AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if(audioSource == null)
        {
            Debug.Log("Audio null");
        }
    }

    private void Update()
    {
        
        if(transform.position.x<-7)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            //AudioManager.Instance.Play("Coin");
            GameManager.Instance.coins++;
            GameManager.Instance.coinText.text = GameManager.Instance.coins.ToString();
            GameObject go = Instantiate(ParticleEffectObject, transform.position, Quaternion.identity);
            Destroy(gameObject);
            Destroy(go, 1);
        }
    }
}
