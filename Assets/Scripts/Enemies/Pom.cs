using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Enemy;
public class Pom : Enemy.Enemy
{
    [Header("Pom infomation")]
    [SerializeField] Animator animator;
    [SerializeField] float timeCounting;
    [Tooltip("Distance with player to hide Pom size")]
    [SerializeField] float distance = 4f;
    private int status = 0;
    private int INDLE = 0;
    private int COUNTING = 1;
    private int DIE = 2;
    private int END = 3;


    private string INDLE_ANIMATION = "Pom_body_normal";
    private string DIE_ANIMATION = "Pom_body_die";
    private string COUNTING_ANIMATION = "Pom_body_counting";
    private Pom_checker checker;
    private float couting = 0;
    public int Status
    {
        get => status;
        set
        {
            if (value == INDLE)
                animator.Play(INDLE_ANIMATION);
            else if (value == COUNTING)
            {
                animator.Play(COUNTING_ANIMATION);
                checker.gameObject.SetActive(true);
            }
            else if (value == DIE) {
                animator.Play(DIE_ANIMATION);
            }
            status = value;
        }
    }

    private void Awake()
    {
        checker = GetComponentInChildren<Pom_checker>();
        checker.Pom = this;
    }

    private void Start()
    {
        animator.Play(INDLE_ANIMATION);
    }
    public void startCounting()
    {
        Status = COUNTING;
    }


    private void Update()
    {
        if (status == INDLE) {
            if (Vector2.Distance(transform.position, Player.Player.instance.transform.position) <= distance)
                checker.gameObject.SetActive(true);
            else checker.gameObject.SetActive(false);
        }
        else if (status == COUNTING) count();
        else if (status == DIE)
        {
            StartCoroutine(die());
            Status = END;
        }
    }

    void count()
    {
        couting += Time.deltaTime;
        if (couting >= timeCounting)
        {
            Status = DIE;
        }
    }
    IEnumerator die()
    {
        yield return new WaitForSeconds(1f);
        if (checker.IsTrigged)
            Player.Player.instance.getDamaged(this.Damage);
        OnDead();
    }
}
