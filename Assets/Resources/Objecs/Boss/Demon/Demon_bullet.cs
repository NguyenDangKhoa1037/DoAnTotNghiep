using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Demon_bullet : Player.Bullet
{
    [SerializeField] private Mud mud;
    [SerializeField] private float deltaTimeGenMud = 0.2f;
    [SerializeField] private float timeExists = 3.2f;
    private float count = 0;
    private Vector2 dir;
    private bool isStart;
    public void configDame(int dame) {
        if (this.damage != 0) throw new System.Exception("NOT SET Dame");
        this.damage = dame;
    }
    public void startMove(Vector2 dir ,float speed = 12f) {
        this.speed = speed;
        this.dir = dir;
        isStart = true;
        Destroy(gameObject, timeExists);
    }
    private void Update()
    {
        if(isStart)
        transform.Translate(speed * dir * Time.deltaTime);
        count += Time.deltaTime;
        if(count > deltaTimeGenMud)
        {
            Instantiate(mud, transform.position, Quaternion.identity);
            count = 0;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy") 
            || collision.CompareTag("Spirit") 
            || collision.CompareTag("EnemyBullet") 
            || collision.CompareTag("Effection")
             || collision.CompareTag("MyBullet"))
           return;
        if (collision.CompareTag("Player")) Player.Player.instance.getDamaged(this.damage);
        Destroy(gameObject);
    }

    public override void startMove(Vector2 direction)
    {
    }
}
