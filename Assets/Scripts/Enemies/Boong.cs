using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boong : Enemy
{
    private Rigidbody2D myBody;
    [SerializeField] GameObject bulletObject;
    [SerializeField] float speedBullet;
    private List<EnemyBullet> bullets = new List<EnemyBullet>();
    private void Awake()
    {
        myBody = GetComponent<Rigidbody2D>();
        if (player == null) findPlayer();
        this.speed += Random.Range(-0.1f, 0.1f);
        for (int i = 0; i < 4; i++) {
            int zAxis = Random.Range(0, 360);
            EnemyBullet b = Instantiate(bulletObject, this.transform.position, Quaternion.Euler(0,0,zAxis)).GetComponent<EnemyBullet>();
            b.gameObject.SetActive(false);
            b.config(new BulletInfo(speedBullet, this.damage, 1));
            bullets.Add(b);
        }
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
        for (int i = 0; i < 4; i++) {
            bullets[i].transform.position = this.transform.position;
            bullets[i].gameObject.SetActive(true);
            bullets[i].startMove(Vector2.right);
        }
        Destroy(gameObject);
    }
}
