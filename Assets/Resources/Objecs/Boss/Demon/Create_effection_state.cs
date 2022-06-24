using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Create_effection_state : MonoBehaviour
{
    private float speedJump = 5.3f;
    private float height = 2f;
    [SerializeField] private GameObject shadow;
    [SerializeField] private Demon_bullet bulletTemplate;
    private bool canActive = true;
    private bool isStart = false;
    private int dir = 1;
    private Vector2 originPos;
    private Vector2 orignalShadow;

    private Demon demon;
    public void config(Demon demon) {
        this.demon = demon;
    }

    public void run() {
        isStart = true;
        originPos = transform.position;
        orignalShadow = shadow.transform.position;
    }

    private void Update()
    {
        if (isStart) {

            transform.Translate(Vector2.up * dir * (dir > 0 ? speedJump : (speedJump*2  )) * Time.deltaTime);
            shadow.transform.position = orignalShadow;
            // nhay len
            if (dir > 0 && transform.position.y > originPos.y + height) {
                dir = -1;
            }
           // nhay xuong
            else if (dir < 0 && transform.position.y < originPos.y)
            {
                transform.position = originPos;
                Camera.main.GetComponent<ShakeCamera>().shake();
                isStart = false;
                dir = 1;
                demon.endCreatePoisonEffion();
                shoot();
            }
        }  
    }


    void shoot()
    {
        List<Demon_bullet> bullets = new List<Demon_bullet>();
        Vector2 [] dirs = new Vector2 [] {new Vector2(0, 0.3f), new Vector2(0.3f, 0), new Vector2(0, -0.3f), new Vector2(-0.3f, 0)};
        for (int i = 0; i < 4; i++) {
            bullets.Add(Instantiate(bulletTemplate, transform.position + (Vector3)dirs[i], Quaternion.identity));
        }

        bool _45degree = Random.Range(0, 2) == 0;
        for (int i = 0; i < 4; i++)
        {
            bullets[i].transform.rotation = Quaternion.Euler(0, 0, _45degree ? 45 : 0);
            bullets[i].startMove(dirs[i]);
        }
    }
}
