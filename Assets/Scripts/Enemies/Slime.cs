using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Enemy;
public class Slime : Enemy.Enemy
{
    [Header("Information for Slime")]
    [SerializeField] private float distanceWithPlayer = 4f;
    [SerializeField] float distancePlayerForJump = 1f;
    [SerializeField] float speedJump;
    [SerializeField] float speedWalking;
    [SerializeField] float sleep = 0.7f;
    private float countSleep;
    private Vector2 target;
    public int state = 0;  // 
    private Animator ani;
    private bool isJump = false;
    private float maxTimeForState = 1f;
    private float countTimeState = 1f;
    public int State
    {
        get => state;
        set
        {
            if (value == 0)
            {
                ani.SetBool("isMoving", false);
                target = Vector2.zero;
                countTimeState = maxTimeForState;
            }

            else ani.SetBool("isMoving", true);
            state = value;
        }
    }

    private void Awake()
    {
        this.runAwake();
        countSleep = sleep;
        speed += UnityEngine.Random.Range(-0.5f, 0.5f);
        speedJump += UnityEngine.Random.Range(0f, 0.3f);
        speedWalking += UnityEngine.Random.Range(0f, 0.3f);
        distancePlayerForJump += UnityEngine.Random.Range(-0.3f, 0.3f);
        distanceWithPlayer += UnityEngine.Random.Range(0f, 0.5f);
        ani = GetComponent<Animator>();
    }


    private void Update()
    {
        float distanceTarget = Vector2.Distance(transform.position, target);
        float distancePlayer = Vector2.Distance(transform.position, player.transform.position);
        if (State != 3 && State == 1 && distanceTarget <= 0.1f)
        {
            State = 0;
            target = Vector2.zero;
        }
        //else if (State != 3 && countTimeState == 0 && distancePlayer <= 0.1)
        //{
        //    State = 0;
        //    target = Vector2.zero;
        //}
        else if (State == 2 && countTimeState == 0 && distancePlayer <= distancePlayerForJump)
        {
            State = 3;
            ani.Play("Slime_jump");

        }
        else if (State != 3 && countTimeState == 0 &&  distancePlayer <= distanceWithPlayer)
        {
            State = 2;
            target = Vector2.zero;

        }
        else if (State == 2 && distancePlayer > distanceWithPlayer)
        {
            State = 0;
            target = Vector2.zero;

        }



        if (State == 2)
        {
            moveToPlayer();
        }
        else if (State == 0)
        {
            indle();
        }
        else if (State == 1)
        {
            randomMMoving();
        }
        else if (State == 3 && isJump)
        {
            jump();
        }
        if(countTimeState > 0)
        countTimeState-= Time.deltaTime;
        if (countTimeState <= 0) {
            countTimeState = 0;
        }
    }

    private void jump()
    {
        Vector2 dir = target - (Vector2)transform.position;
        dir.Normalize();
        transform.Translate(speedJump * Time.deltaTime * dir);
    }
    public void startJump() {
        target = player.gameObject.transform.position;
        isJump = true;
    }
    public void endJump() {
        isJump = false;
        State = 0;
        ani.Play("Slime_indle");
    }

    private void indle()
    {
        if (target == Vector2.zero)
        {
            target = transform.position;
            target.x += UnityEngine.Random.Range(-1.3f, 1.3f);
            target.y += UnityEngine.Random.Range(-1.3f, 1.3f);
        }
        countSleep -= Time.deltaTime;
        if (countSleep <= 0)
        {
            State = 1;
            countSleep = sleep;
        }

    }

    void moveToPlayer()
    {
        Vector2 dir = player.transform.position - transform.position;
        dir.Normalize();
        transform.Translate(speed * Time.deltaTime * dir);
    }

    void randomMMoving()
    {
        Vector2 dir = target - (Vector2)transform.position;
        transform.Translate(speedWalking * Time.deltaTime * dir);
    }

    protected override void handleTriggerEnter(Collider2D collision)
    {
        base.handleTriggerEnter(collision);
        if (State != 0 && !collision.gameObject.CompareTag("Player") && !collision.gameObject.CompareTag("Spirit") && !collision.gameObject.CompareTag("Enemy"))
        {
            State = 0;
        }
        if (collision.gameObject.CompareTag("Player")) {
            player.getDamaged(this.damage);
        }
    }
}
