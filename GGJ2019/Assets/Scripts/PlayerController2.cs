using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController2 : MonoBehaviour
{
    private Rigidbody2D rb2d;
    private SpriteRenderer spriteRenderer;

    public float moveSpeed;

    //shooting
    private float axisXtemp;
    private float axisYtemp;
    public int direction;

    //health 
    public Image[] healthImages;
    public Sprite[] healthSprites;
    private int startHearts = 5;
    public int currHealth = 5;

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        TakeDemange(0);
    }

    private void Update()
    {
        Move();
    }

    void Move()
    {
        float axisX = 0f;
        float axisY = 0f;

        axisX = Input.GetAxis("Horizontal2");
        axisY = Input.GetAxis("Vertical2");

        if (axisX != 0 || axisY != 0)
        {
            axisXtemp = axisX;
            axisYtemp = axisY;
        }

        Directions(axisX, axisY, direction);

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

    void updateSkulls()
    {
        for (int i = 0; i < healthImages.Length; i++)
        {
            if (i < currHealth)
            {
                healthImages[i].sprite = healthSprites[0];
            }
            else
            {
                healthImages[i].sprite = healthSprites[1];
            }
        }
    }

    void TakeDemange(int amount)
    {
        currHealth += amount;
        currHealth = Mathf.Clamp(currHealth, 0, startHearts);
        updateSkulls();
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player1" && this.gameObject.tag == "Player2")
        {
            StartCoroutine("Blink");
            this.TakeDemange(-1);
            if (currHealth <= 0)
            {
                StartCoroutine("Wait");
            }
        }

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

