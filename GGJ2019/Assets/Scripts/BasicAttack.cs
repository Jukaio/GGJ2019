using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicAttack : MonoBehaviour
{
    public GameObject hitbox;
    public float cooldown = 4;
    private float collisionBuffer = 0;
    private PlayerController controller1;
    int direction;

    void Start()
    {
        controller1 = GetComponent<PlayerController>();
    }

    
    void Update()
    {
        if (cooldown <= 0 && Input.GetKey("f"))
        {
            hitbox.SetActive(true);
            collisionBuffer = 0.5f;
            cooldown = 4;
        }
        else if (collisionBuffer <= 0)
            hitbox.SetActive(false);

        if(cooldown != 0)
            if (cooldown >= 0)
            cooldown -= Time.deltaTime;
            else if (cooldown <= 0)
            cooldown = 0;
        collisionBuffer-= Time.deltaTime;

        switch(controller1.direction)
        {
            case 1:
                break;
            case 2:
                break;
            case 3:
                break;
            case 4:
                break;
        }
    }


}
