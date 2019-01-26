using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

    private Rigidbody2D rb2d;
    public float speed;
    private PlayerController player1;
    private PlayerController player2;

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        player1 = GameObject.FindGameObjectWithTag("Player1").GetComponent<PlayerController>();
        player2 = GameObject.FindGameObjectWithTag("Player2").GetComponent<PlayerController>();
    }

    void Update()
    {
        Shoot(player1.direction1);
        Shoot(player2.direction2);
    }

    void Shoot(int direction)
    {
        switch (direction)
        {
            case 1:
                ShootLeft();
                break;
            case 2:
                ShootRight();
                break;
            case 3:
                ShootDown();
                break;
            case 4:
                ShootUp();
                break;
            default:
                break;
        }
        Invoke("Destroy", 1f);
    }

    void ShootLeft()
    {
        transform.Translate(Vector2.left * speed);
    }
    void ShootRight()
    {
        transform.Translate(Vector2.right * speed);
    }
    void ShootDown()
    {
        transform.Translate(Vector2.down * speed);
    }
    void ShootUp()
    {
        transform.Translate(Vector2.up * speed);
    }

    void Destroy()
    {
        Destroy(gameObject);
    }
}
