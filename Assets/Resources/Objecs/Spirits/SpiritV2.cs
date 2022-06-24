using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiritV2 : MonoBehaviour
{
    [Header("List Speed")]
    [SerializeField] float speedIndle;
    [SerializeField] float speedFlowPlayer;
    [SerializeField] float speedTeleport;

    [Header("Other Information")]
    [SerializeField] float sleepOnIndle = 1f;
    [SerializeField] float waitingShootTime = 4f;
    [SerializeField] GameObject teleport_effect;

    [Header("Bullet config")]
    [SerializeField] Player.Bullet[] bullets;
    [SerializeField] BulletType type;
    private Player.Bullet currentBullet;

    private Vector2 target;
    public int state = 0;
    private float sleepTime = 1f;
    private bool isSwitchRoom = false;

    static public int INDLE = 0;
    static public int INDLE_WAITING = 1;
    static public int FOLLOW_PLAYER = 2;
    static public int TELEPORT = 3;
    static public int WAITING_SHOT = 4;
    static public int END_SHOT = 5;

    #region properties need config by Player instance
    // properties need config by Player instance
    private Player.Player player;
    #endregion

    // INDLE_WAITING > INDLE > FLOW_PLAYER > TELEPORT > WAITING_SHOOT > END_SHOOT
    public int State
    {
        get => state;
        set
        {

            if (value == INDLE) Target = Vector2.zero;
            if (value == TELEPORT) Instantiate(teleport_effect, transform.position, Quaternion.identity);
            if (!(state == WAITING_SHOT) || (state == WAITING_SHOT && value == END_SHOT))
            {
                state = value;
            }

        }
    }

     public float WaitingShootTime { get => waitingShootTime; }
    public Vector2 Target { get => target; set => target = value; }

    public void config(Player.Player player, Transform pointSpiritInPlayer)
    {
        this.player = player;
    }
    private void Awake()
    {
        // config
        //config bullet
        configBullet();
    }
    private void Update()
    {
        if (State == INDLE_WAITING)
        {
            sleep();
        }
        else if (State == INDLE)
        {
            indleRandomMove();
        }
        else if (State == FOLLOW_PLAYER)
        {
            flowPlayer();
        }
        else if (State == TELEPORT)
        {
            teleport();
        }
        else if (State == WAITING_SHOT)
        {
           

        }
        else if (State == END_SHOT)
        {
            State = INDLE_WAITING;
        }
    }

    #region Sleep
    private void sleep()
    {
        sleepTime -= Time.deltaTime;
        if (sleepTime <= 0)
        {
            State = INDLE;
            sleepTime = sleepOnIndle;
        }

    }
    #endregion

    #region INDLE
    private Vector2 createTarget()
    {
        Vector2 result = transform.position;
        result.x += Random.Range(-0.4f, 0.4f);
        result.y += Random.Range(-0.4f, 0.4f);
        return result;
    }
    private void indleRandomMove()
    {
        if (Target == Vector2.zero)
            Target = createTarget();
        Vector2 direction = Target - (Vector2)transform.position;
        direction.Normalize();
        transform.Translate(speedIndle * direction * Time.deltaTime);
        if (Vector2.Distance(transform.position, Target) <= 0.01f)
            State = INDLE_WAITING;
    }
    #endregion

    #region FLLOW PLAYER
    private void flowPlayer()
    {
        Vector2 dir = (Vector3)Target - transform.position;
        if (Vector2.SqrMagnitude(dir) >= .7f)
            dir.Normalize();
        transform.Translate(dir * speedFlowPlayer * Time.deltaTime);
        if (Vector2.Distance(transform.position, Target) <= 0.01f)
        {
            State = INDLE_WAITING;
        }
    }
    #endregion

    #region Shooting
    private void teleport()
    {
        Vector2 dir = (Vector3)Target - transform.position;
        if (Vector2.Distance(Target, transform.position) <= 1f)
            dir.Normalize();
        transform.Translate(dir * speedTeleport * Time.deltaTime);
        if (Vector2.Distance(transform.position, Target) <= 0.1f)
        {
            transform.position = Target;
            State = WAITING_SHOT;
            // transform.SetParent(PointSpiritInPlayer.parent);
        }
        //transform.position = target;
        //State = WAITING_SHOT;
    }
    #endregion
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) return;
        State = INDLE_WAITING;
    }
     public void shoot(Player.IShoot shooting, Vector2 direction)
    {
        Player.Bullet bulletObj = Instantiate(currentBullet, transform.position, Quaternion.identity);
        bulletObj.config(player, shooting);
        bulletObj.startMove(direction);
    }

    private void configBullet()
    {
        switch (type)
        {
            case BulletType.NORMAL:
                currentBullet = bullets[0];
                break;
            case BulletType.SPIRIT_POWER:
                currentBullet = bullets[1];
                break;
            default: throw new System.Exception("TYPE INVALID");
        }
    }
     public void switchRoom(Vector2 pos)
    {
        State = INDLE_WAITING;
        isSwitchRoom = true;
        transform.position = pos;
        isSwitchRoom = false;
    }
     public int getDamage()
    {
        return currentBullet.Damage;
    }
}
