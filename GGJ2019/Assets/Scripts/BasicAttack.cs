using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicAttack : MonoBehaviour
{
    public GameObject hitbox;
    public float cooldown = 1;
    private float collisionBuffer = 0;
    private PlayerController controller1;
    int direction;

    void Start()
    {
        controller1 = GetComponent<PlayerController>();
    }
    
    void Update()
    {
        if (cooldown <= 0 && Input.GetKey("q"))
        {
            hitbox.SetActive(true);
            collisionBuffer = 0.5f;
            cooldown = 1;
        }
        else if (collisionBuffer <= 0)
            hitbox.SetActive(false);

        if(cooldown != 0)
            if (cooldown >= 0)
            cooldown -= Time.deltaTime;
            else if (cooldown <= 0)
            cooldown = 0;
        collisionBuffer-= Time.deltaTime;

        switch (controller1.direction)
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
