using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mushroom : Enemy.Enemy
{
    [Header("Mushroom information")]
    [SerializeField] private float distanceToAttack;
    [SerializeField] private Animator animator;
    [SerializeField] private float deplay;
    [SerializeField] private float deplayRun;
    [SerializeField] private Transform check;
    private string indle_animation = "Mushroom_body_indle";
    private string run_animation = "Mushroom_body_run";
    private string die_animation = "Mushroom_body_die";
    private string attack_animation = "Mushroom_body_attack";


    private int status = 0;
    private int INDLE = 0;
    private int RUN = 1;
    private int DIE = 2;
    private int ATTACK = 3;
    private int END = 4;

    private float couting = 0;
    private bool isFacingRight = true;
    public int Status
    {
        get => status;
        set
        {
            status = value;
            if (value == INDLE)
            {
                animator.Play(indle_animation);
            }
            else if (value == RUN)
            {
                animator.Play(run_animation);
            }
            else if (value == ATTACK)
            {
                animator.Play(attack_animation);
            }
            else if (value == DIE)
            {
                animator.Play(die_animation);
                Invoke("die", 1f);
                Status = END;
            }
        }
    }

    private void Awake()
    {
        speed += Random.Range(-0.8f, 0.8f);
        deplay += Random.Range(-0.8f, 0.8f);
        deplayRun += Random.Range(-0.8f, 0.8f);
        runAwake();
    }

    private void Update()
    {
        if (status == INDLE)
        {
            indle();
        }
        else if (status == RUN)
        {
            run();
        }
        else if (status == ATTACK)
        {
            Invoke("attack", .6f);
            Status = END;
        }
    }

    private void indle()
    {

        couting += Time.deltaTime;
        if (couting >= deplay)
        {
            couting = 0;
            Status = RUN;
        }


    }

    private void run()
    {

        if (Vector2.Distance(transform.position, player.transform.position) <= distanceToAttack)
        {
            Status = ATTACK;
            couting = 0;
        }
        else
        {
            couting += Time.deltaTime;
            if (couting >= deplayRun)
            {
                couting = 0;
                Status = INDLE;
            }
        }
        moving(speed);
    }

    private void die()
    {

    }

    private void attack()
    {
        bool isPlayer = Physics2D.OverlapCircle(check.position, 0.3f, player.PlayerMask);
        if (isPlayer)
        {
            print(Damage);
            player.getDamaged(Damage);
        }
        Status = INDLE;
    }

    private void moving(float _speed)
    {
        Vector2 dir = player.transform.position - transform.position;
        dir.Normalize();
        transform.Translate(_speed * dir * Time.deltaTime);
        split(dir);
    }


    private void split(Vector2 dir)
    {
        if ((isFacingRight && dir.x < 0) || (!isFacingRight && dir.x > 0))
        {
            Vector2 scale = transform.localScale;
            scale.x *= -1;
            transform.localScale = scale;
            isFacingRight = !isFacingRight;
        }
    }
}
