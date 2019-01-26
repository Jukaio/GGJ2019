using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShootController : MonoBehaviour {

    public GameObject bulletPrefab;
    public float timerValue;
    private float timer;

    // Use this for initialization
    void Start () {
        timer = timerValue;
    }

    public void Shoot()
    {
        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            var bullet = (GameObject)Instantiate(
            bulletPrefab,
            transform.position,
            transform.rotation);
            timer = timerValue;
        }
    }
}
