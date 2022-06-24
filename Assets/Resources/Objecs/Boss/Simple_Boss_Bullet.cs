using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Simple_Boss_Bullet : MonoBehaviour
{
    private float timeExist;
    private float speed;
    private Vector2 dir;
    private bool startMove = false;
    private Enemy.Enemy enemy;
    public void config(Vector2 dir, float speed, float timeExist, Enemy.Enemy enemy) {
        this.speed = speed;
        this.dir = dir;
        this.timeExist = timeExist;
        this.enemy = enemy;
        startMove = true;
        Destroy(gameObject, timeExist);
    }

    private void Update()
    {
        if (startMove) {
            transform.Translate(speed * Time.deltaTime * dir);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy")
            || collision.CompareTag("EnemyBullet")
            || collision.CompareTag("Effection")
            || collision.CompareTag("MyBullet")) 
            return;
        if (collision.CompareTag("Player")) collision.GetComponent<Player.Player>().getDamaged(enemy.Damage);
        Destroy(gameObject);
    }
}
