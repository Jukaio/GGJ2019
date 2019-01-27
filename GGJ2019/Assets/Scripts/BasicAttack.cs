using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicAttack : MonoBehaviour
{
    public GameObject hitbox;
    public float cooldown = 1;
    private float collisionBuffer = 0;
    private PlayerController controller1;
    private PlayerController2 controller2;
    int direction;
    private Animator parentAnimator;

    void Start()
    {
        controller1 = GetComponent<PlayerController>();
        controller2 = GetComponent<PlayerController2>();
        parentAnimator = GetComponent<Animator>();
    }
    
    void Update()
    {
        if (tag == "Player1")
        {
            if (cooldown <= 0 && Input.GetKey("q"))
            {
                hitbox.SetActive(true);
                //parentAnimator.SetBool("Attack", true);

                collisionBuffer = 0.5f;
                cooldown = 1;
            }
            else if (collisionBuffer <= 0)
            {
                //parentAnimator.SetBool("Attack", false);
                
                hitbox.SetActive(false);
            }

            if (cooldown != 0)
                if (cooldown >= 0)
                    cooldown -= Time.deltaTime;
                else if (cooldown <= 0)
                    cooldown = 0;
            collisionBuffer -= Time.deltaTime;



            switch (controller1.direction)
            {
                case 1:
                    hitbox.transform.position = gameObject.transform.position + (Vector3.left * 3.15f);
                    hitbox.transform.eulerAngles = new Vector3(0, 0, 90);
                    break;
                case 2:
                    hitbox.transform.position = gameObject.transform.position + (Vector3.right * 3.15f);
                    hitbox.transform.eulerAngles = new Vector3(0, 0, 270);
                    break;
                case 3:
                    hitbox.transform.position = gameObject.transform.position + (Vector3.down * 3f);
                    hitbox.transform.eulerAngles = new Vector3(0, 0, 180);
                    break;
                case 4:
                    hitbox.transform.position = gameObject.transform.position + (Vector3.up * 3.15f);
                    hitbox.transform.eulerAngles = new Vector3(0, 0, 0);
                    break;
            }
        }

        else if(tag == "Player2")
        {
            if (cooldown <= 0 && Input.GetKey("p"))
            {
                hitbox.SetActive(true);
                collisionBuffer = 0.5f;
                cooldown = 1;
            }
            else if (collisionBuffer <= 0)
                hitbox.SetActive(false);

            if (cooldown != 0)
                if (cooldown >= 0)
                    cooldown -= Time.deltaTime;
                else if (cooldown <= 0)
                    cooldown = 0;
            collisionBuffer -= Time.deltaTime;


            switch (controller2.direction)
            {
                case 1:
                    hitbox.transform.position = gameObject.transform.position + (Vector3.left * 0.75f);
                    hitbox.transform.eulerAngles = new Vector3(0, 0, 0);
                    break;
                case 2:
                    hitbox.transform.position = gameObject.transform.position + (Vector3.right * 0.75f);
                    hitbox.transform.eulerAngles = new Vector3(0, 0, 0);
                    break;
                case 3:
                    hitbox.transform.position = gameObject.transform.position + (Vector3.down * 0.75f);
                    hitbox.transform.eulerAngles = new Vector3(0, 0, 90);
                    break;
                case 4:
                    hitbox.transform.position = gameObject.transform.position + (Vector3.up * 0.75f);
                    hitbox.transform.eulerAngles = new Vector3(0, 0, 90);
                    break;
            }
        }
    }

}
