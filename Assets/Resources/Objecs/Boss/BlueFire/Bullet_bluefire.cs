using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet_bluefire : MonoBehaviour
{
    [SerializeField] Simple_Boss_Bullet bullet;
    

    float countTime = 0.15f;
    float count = 0;
    private bool start = false;
    private float speed;
    private Enemy.Enemy enemy;
    public void config( float speed, float timeExist, Enemy.Enemy enemy) {
        this.speed = speed;
        this.enemy = enemy;
        start = true;
        Destroy(gameObject, timeExist);
        
    }
    private void Update()
    {
        transform.Rotate(new Vector3(0, 0, 10f));
        if (start) {
            count += Time.deltaTime;
            if (count >= countTime) {
                count = 0;
                for (int i = 0; i < transform.childCount; i++) {
                    Simple_Boss_Bullet boss_Bullet = Instantiate(bullet, transform.GetChild(i).position, Quaternion.identity);
                    boss_Bullet.config((Vector2)transform.GetChild(i).position - (Vector2)transform.position, speed, 6f,enemy);
                }
            } 
        }
    }
}
