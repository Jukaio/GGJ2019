using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb2d;
    private SpriteRenderer spriteRenderer;

    public float moveSpeed;

    //shooting
    private float axisXtemp;
    private float axisYtemp;
    public int direction;
    private Animator anim;
    private BoxCollider2D theCollider;

    public GameObject Front;
    public GameObject Side;
    public GameObject Rear;
    public GameObject hitMarker;

    //health 
    public Image[] healthImages;
    public Sprite[] healthSprites;
    private int startHearts = 5;
    public int currHealth = 5;

    private float dodgeCooldown = 2;

    void Start()
    {
        //Front = GetComponent<GameObject>();
        //Side = GetComponent<GameObject>();
        //Rear = GetComponent<GameObject>();
        rb2d = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        TakeDemange(0);
        anim = GetComponent<Animator>();
        theCollider = GetComponent<BoxCollider2D>();
    }

    private void Update()
    {
        Move();
    }

    void Move()
    {
        float axisX = 0f;
        float axisY = 0f;

        axisX = Input.GetAxis("Horizontal1");
        axisY = Input.GetAxis("Vertical1");

        if (axisX != 0 || axisY != 0)
        {
            axisXtemp = axisX;
            axisYtemp = axisY;
        }
      
        direction = Directions(axisX, axisY, direction);

        if (Input.GetKeyDown("h") && dodgeCooldown <= 0)
        switch (direction)
        {
            case 1:
                    transform.position += Vector3.right * 2;
                    dodgeCooldown = 2;
                    break;
            case 2:
                    transform.position += Vector3.left * 2;
                    dodgeCooldown = 2;
                    break;
            case 3:
                    transform.position += Vector3.up * 2;
                    dodgeCooldown = 2;
                    break;
            case 4:
                    transform.position += Vector3.down * 2;
                    dodgeCooldown = 2;
                    break;
        }

        transform.Translate(new Vector3(axisX, axisY) * Time.deltaTime * moveSpeed);
        if (dodgeCooldown > 0)
            dodgeCooldown -= Time.deltaTime;
        else
            dodgeCooldown = 0;

        switch (direction)
        {
            case 1:
                anim.SetInteger("Direction", 1);
              
                    anim.SetBool("Movement", true);
                    anim.speed = 0.4f;

                
                if (axisX <= 0.3 || axisY <= 0.3)
                {
                    GetComponent<SpriteRenderer>().enabled = false;
                    Front.GetComponent<SpriteRenderer>().enabled = false;
                    Side.GetComponent<SpriteRenderer>().enabled = true;
                    Side.GetComponent<SpriteRenderer>().flipX = true;
                    Rear.GetComponent<SpriteRenderer>().enabled = false;
                }
                theCollider.size = new Vector2(4.5f, 2.5f);
                theCollider.offset = new Vector2(0f, -0.5f);
                break;
            case 2:
                anim.SetInteger("Direction", 2);
                
                    anim.SetBool("Movement", true);
                    anim.speed = 0.4f;
                
                if (axisX <= 0.3 || axisY <= 0.3)
                {
                    GetComponent<SpriteRenderer>().enabled = false;
                    Front.GetComponent<SpriteRenderer>().enabled = false;
                    Side.GetComponent<SpriteRenderer>().enabled = true;
                    Side.GetComponent<SpriteRenderer>().flipX = false;
                    Rear.GetComponent<SpriteRenderer>().enabled = false;
                }
                theCollider.size = new Vector2(4.5f, 2.5f);
                theCollider.offset = new Vector2(0f, -0.5f);
                break;
            case 3:
                anim.SetInteger("Direction", 3);
                
                    anim.SetBool("Movement", true);
                    anim.speed = 0.4f;
                
                if (axisX <= 0.3 || axisY <= 0.3)
                {
                    GetComponent<SpriteRenderer>().enabled = false;
                    Front.GetComponent<SpriteRenderer>().enabled = true;
                    Side.GetComponent<SpriteRenderer>().enabled = false;
                    Rear.GetComponent<SpriteRenderer>().enabled = false;
                }
                theCollider.size = new Vector2(2.25f, 3.75f);
                theCollider.offset = new Vector2(0f, 0.215f);
                break;
            case 4:
                anim.SetInteger("Direction", 4);
                
                    anim.SetBool("Movement", true);
                    anim.speed = 0.4f;
                
                if (axisX <= 0.3 || axisY <= 0.3)
                {
                    GetComponent<SpriteRenderer>().enabled = false;
                    Front.GetComponent<SpriteRenderer>().enabled = false;
                    Side.GetComponent<SpriteRenderer>().enabled = false;
                    Rear.GetComponent<SpriteRenderer>().enabled = true;
                }
                theCollider.size = new Vector2(2.5f, 3.25f);
                theCollider.offset = new Vector2(0.15f, 0f);
                break;
        }

        if (axisX != 0 || axisY != 0)
        {
            GetComponent<SpriteRenderer>().enabled = true;
            Front.GetComponent<SpriteRenderer>().enabled = false;
            Side.GetComponent<SpriteRenderer>().enabled = false;
            Rear.GetComponent<SpriteRenderer>().enabled = false;
            anim.SetBool("Movement", true);
        }
        else
            anim.SetBool("Movement", false);

    }

    int Directions(float axisX, float axisY, int direction)
    {
        if (axisX < 0) //left
        {
            spriteRenderer.flipX = true;
            direction = 1;
        }
        else if (axisX > 0) //right
        {
            spriteRenderer.flipX = false;
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

    private void TakeDemange(int amount)
    {
        currHealth += amount;
        currHealth = Mathf.Clamp(currHealth, 0, startHearts);
        updateSkulls();
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player2Attack")
        {
            StartCoroutine("Blink");
            this.TakeDemange(-1);
            if(currHealth <= 0)
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
        SceneManager.LoadScene("Player2win");
    }

    IEnumerator Blink()
    {
        hitMarker.SetActive(true);
        yield return new WaitForSeconds(0.2f);
        hitMarker.SetActive(false);
    }

}

