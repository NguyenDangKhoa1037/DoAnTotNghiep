using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boong : Enemy
{
    private Rigidbody2D myBody;
    private void Awake()
    {
        myBody = GetComponent<Rigidbody2D>();
        if (player == null) findPlayer();
        this.speed += Random.Range(-0.1f, 0.1f);
    }
    private void Start()
    {
        float seconds = Random.Range(6f, 8f);
        Invoke("explosion", seconds);
    }
    private void Update()
    {
        Vector2 dir =  this.player.position - this.transform.position ;
        dir.Normalize();
        myBody.velocity = dir * this.speed;
    }


    void explosion() {
        Destroy(gameObject);
    }
}
