using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Demon : Enemy.Enemy
{

    [Header("Information for Demon: Status")]
    //[SerializeField] private int hp = 100;
    [Header("Information for Demon: Timer")]
    [SerializeField] private float timeIndle = 0.6f;
    [SerializeField] private float timeCreatePoison = 2.4f;
    [Header("Information for Demon: Speed")]
    [SerializeField] private float speedWalking;
    
    [Header("Information for Demon: Other")]
    [SerializeField] private float distanceWithPlayer = 2f;
    private int state = 0;
    private const int INDLE = 0;
    private const int RUN = 1;
    private const int ATTACK_1 = 2;
    private const int ATTACK_2 = 3;
    private const int JUMP = 4;
    private const int NONE = 5;

    private bool isFacingRight = false;
    private Transform playerPivot;
    private ChangeAnimation animationManager;
    private Vector2 target;

    private Create_effection_state create_Effection;

    private float timeCount = 0;
    
    public int State
    {
        get => state; set
        {
            state = value;
            setAnimation();
        }
    }

    //public int Hp { get => hp; set {
    //        hp = value;
    //        hpBar.setHp(hp);
    //    } }

    private void Awake()
    {
        Canvas canvas = GetComponentInChildren<Canvas>();
        canvas.renderMode = RenderMode.ScreenSpaceOverlay;
        canvas.worldCamera = Camera.main;
        playerPivot = GameObject.FindGameObjectWithTag("Player_pivot").transform;
        this.player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player.Player>();
        this.hpBar = GetComponent<EnemyHpBar>();
        animationManager = GetComponent<ChangeAnimation>();
        create_Effection = GetComponent<Create_effection_state>();
        create_Effection.config(this);
        hpBar.configMaxHP(hp);
    }
    private void Start()
    {
        StartCoroutine(createPoisonEffion());
    }
    private void Update()
    {
        ditectState();
        stateManager();
    }

    private void stateManager()
    {
        if (state == INDLE) indle();
        else if (state == RUN) run();
    }

    private void indle()
    {
        timeCount += Time.deltaTime;
        if (timeCount >= timeIndle)
        {
            timeCount = 0;
            State = RUN;
        }
    }

    private void run()
    {
        target = playerPivot.position.x > transform.position.x ?
            new Vector3(playerPivot.position.x - distanceWithPlayer, playerPivot.position.y) :
            new Vector3(playerPivot.position.x + distanceWithPlayer, playerPivot.position.y);
        Vector2 dir = target - (Vector2)transform.position;
        dir.Normalize();
        transform.Translate(dir * Time.deltaTime * speedWalking);
        split(dir);
    }

    private void endAttack_1()
    {
        State = INDLE;
    }

    void ditectState()
    {
        target = playerPivot.position.x > transform.position.x ?
           new Vector3(playerPivot.position.x - distanceWithPlayer, playerPivot.position.y) :
           new Vector3(playerPivot.position.x + distanceWithPlayer, playerPivot.position.y);
        if (State == RUN && Vector2.Distance(transform.position, target) <= 0.05f)
        {
            Vector2 dir = player.transform.position - transform.position;
            split(dir);
            State = ATTACK_1;
        }
    }

    IEnumerator createPoisonEffion() {
        yield return new WaitForSeconds(timeCreatePoison);
        if (State == RUN || State == INDLE)
        {
            State = JUMP;
            create_Effection.run();
        }
            StartCoroutine(createPoisonEffion());
    }

    public void endCreatePoisonEffion() {
        State = INDLE;
    }


    void setAnimation()
    {
        const string indle_animation = "demon_indle";
        const string run_animation = "demon_run";
        const string attack_1_animation = "demon_attack";
        const string attack_2_animation = "demon_attacK_2";
        const string jump_animation = "demon_jump";

        string animtion_name = "";
        if (State == INDLE) animtion_name = indle_animation;
        else if (State == RUN) animtion_name = run_animation;
        else if (State == ATTACK_1) animtion_name = attack_1_animation;
        else if (State == ATTACK_2) animtion_name = attack_2_animation;
        else if (State == JUMP) animtion_name = jump_animation;

        if (animtion_name != "") animationManager.play(animtion_name);
    }

    void split(Vector2 dir) {
        if ((isFacingRight && dir.x < 0) || (!isFacingRight && dir.x > 0))
        {
            Vector2 scale = transform.localScale;
            scale.x *= -1;
            transform.localScale = scale;
            isFacingRight = !isFacingRight;
        }
    }
    protected override void OnDead()
    {
        base.OnDead();
        CompleteGame.instance.complete();
    }
    void setDamage() {
        bool isCheck = Physics2D.OverlapCircle(transform.GetChild(transform.childCount - 1).position, 0.2f, player.PlayerMask);
        if (isCheck) {
            player.getDamaged(Damage);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("MyBullet"))
        {
          
            Hp -= Player.Player.instance.dame();
        }
    }
}
