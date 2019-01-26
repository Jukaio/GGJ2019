using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb2d;
    private SpriteRenderer spriteRenderer;
    private PlayerShootController shootingController;

    public float moveSpeed;

    //shooting
    private float axisXtemp;
    private float axisYtemp;
    public int direction1;
    public int direction2;

    //health 
    public Image[] healthImages;
    public Sprite[] healthSprites;
    private int startHearts = 5;
    public int currHealth = 5;

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        shootingController = GetComponent<PlayerShootController>();
        TakeDemange(0);
    }

    private void Update()
    {
        Move();
        shootingController.Shoot();
    }

    void Move()
    {
        float axisX = 0f;
        float axisY = 0f;
        if (this.gameObject.tag == "Player1")
        {
            axisX = Input.GetAxis("Horizontal1");
            axisY = Input.GetAxis("Vertical1");
            direction1 = Directions(axisX, axisY, direction1);
        }
        else if (this.gameObject.tag == "Player2")
        {
            axisX = Input.GetAxis("Horizontal2");
            axisY = Input.GetAxis("Vertical2");
            direction2 = Directions(axisX, axisY, direction2);
        }

        if (axisX != 0 || axisY != 0)
        {
            axisXtemp = axisX;
            axisYtemp = axisY;
        }

        
        

        transform.Translate(new Vector3(axisX, axisY) * Time.deltaTime * moveSpeed);
    }

    int Directions(float axisX, float axisY, int direction)
    {
        if (axisX < 0) //left
        {
            spriteRenderer.flipX = false;
            direction = 1;
        }
        else if (axisX > 0) //right
        {
            spriteRenderer.flipX = true;
            direction = 2;
        }
        else if (axisY < 0) //down
        {
            direction = 3;
        }
        else if (axisY > 0) //up 
        {
            direction = 4;
        }
        return direction;
    }

    //void updateSkulls()
    //{
    //    for (int i = 0; i < healthImages.Length; i++)
    //    {
    //        if (i < currHealth)
    //        {
    //            healthImages[i].sprite = healthSprites[0];
    //        }
    //        else
    //        {
    //            healthImages[i].sprite = healthSprites[1];
    //        }
    //    }
    //}

    void TakeDemange(int amount)
    {
        currHealth += amount;
        currHealth = Mathf.Clamp(currHealth, 0, startHearts);
        PlayerPrefs.SetInt("Health", currHealth);
        //updateSkulls();
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "mob" || other.gameObject.tag == "bullet")
        {
            StartCoroutine("Blink");
            TakeDemange(-1);
            if(currHealth <= 0)
            {
                StartCoroutine("Wait");
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) 
    {
        
    }

    IEnumerator Wait()
    {
        //death.SetActive(true);
        transform.position = new Vector3(transform.position.x, transform.position.y, 20f);
        yield return new WaitForSeconds(2);
        PlayerPrefs.SetInt("Health", 5);
        //Application.LoadLevel("Menu");
    }

    IEnumerator Blink()
    {
        spriteRenderer.material.color = Color.clear;
        yield return new WaitForSeconds(0.2f);
        spriteRenderer.material.color = Color.white;
    }

}

