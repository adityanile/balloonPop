using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalloonBehavior : MonoBehaviour
{
    public int serialNumber;
    public Color balloonColor;
    private OnCollisionNewBalloonSpawner spawner;
    private PlayerController player;    
    private void Awake()
    {
        spawner = FindObjectOfType<OnCollisionNewBalloonSpawner>();
    }
    private void Start()
    {
        player = FindObjectOfType<PlayerController>();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Dart"))
        {
            Destroy(gameObject);
        }
        if (collision.gameObject.CompareTag("Wave"))
        {
            Destroy(gameObject);
        }
        // When Ballon collide with the Astonaut
        if (collision.gameObject.CompareTag("Astonaut"))
        {
            Destroy(gameObject);
        }

        if(collision.gameObject.CompareTag("Balloon"))
        {
            if(collision.gameObject.GetComponent<BalloonBehavior>().serialNumber == serialNumber)
            {
                if(serialNumber == 5)
                {
                    player.shouldStartSpecialAttack1 = true;
                }
                else if(serialNumber == 0)
                {
                    player.shouldStartSpecialAttack2 = true;
                }
                // For Balloon having serialNumber 1 starting the Fairy PowerUp
                else if(serialNumber == 1)
                {
                    player.bringFairy = true;
                }
                // For Ballon having serailNumber 2 start the astaunaut PowerUp
                else if(serialNumber == 2)
                {
                    player.bringAstonaut = true;
                }

                spawner.balloonNumber = serialNumber;
                spawner.transform.position = transform.position;
                spawner.shouldSpawnBalloon = true;
                
                spawner.charMask.fillAmount -= .05f;
                if(spawner.charMask.fillAmount <= 0)
                {
                    spawner.StartNewCharacterPanel();
                    spawner.charMask.fillAmount = 1;
                }
                Destroy(gameObject);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Dart"))
        {
            Destroy(gameObject);
        }
        if (collision.CompareTag("Line"))
        {
            if(GetComponent<Rigidbody2D>().velocity.y >= 0.005f)
                Destroy(gameObject);
        }
    }

    // Managing particle collision with the ballons
    private void OnParticleCollision(GameObject other)
    {
        Destroy(gameObject);
    }
}
