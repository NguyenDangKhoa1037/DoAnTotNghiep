using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlueFire : Enemy.Enemy
{
    [Header("Information for Demon: Timer")]
    [SerializeField] private float timeShoot = 3f;
    [SerializeField] private float timePoison = 6f;

    [Header("Bullet")]
    [SerializeField] private Bullet_bluefire[] bullets;
    [SerializeField] private float speedBullet = 3f;
    [SerializeField] private Demon_bullet demon_Bullet;
    private float timeCount = 0;


    private int state = 0;
    private void Start()
    {
        shootState();
        StartCoroutine(shootState());
        StartCoroutine(shootPoison());
    }

    IEnumerator shootState() {
        yield return new WaitForSeconds(timeShoot);
        if(state == 0)
        setShootState();
        StartCoroutine(shootState());
    }
    IEnumerator shootPoison() {
        yield return new WaitForSeconds(timePoison);
        if (state == 0)
            setPoison();
        StartCoroutine(shootPoison());

    }
    void setPoison() {
        state = 2;
        Invoke("resetState", 4f);

    }
    void setShootState() {

        int index = Random.Range(0, bullets.Length);
        Bullet_bluefire bullet_Bluefire = Instantiate(bullets[index], transform.position + new Vector3(0, -0.6f, 0), Quaternion.identity);
        bullet_Bluefire.config(speedBullet, 2.7f, this);
        Invoke("resetState", 2.7f);
    }

    void resetState() {
        timeCount = 0;
        state = 0;
    }


    private void Update()
    {
        if (state == 2) {
            timeCount += Time.deltaTime;
            if (timeCount >= 1f) {
                timeCount = 0;
                Vector2 dir = player.transform.position - transform.position + new Vector3(0, -0.6f, 0);
                dir.Normalize();
                Demon_bullet demon_Bullet_ = Instantiate(demon_Bullet, transform.position + new Vector3(0, -0.6f, 0), Quaternion.identity);
                demon_Bullet_.configDame(this.Damage);
                demon_Bullet_.startMove(dir);
                state = 0;
            }
        }
    }



    private void Awake()
    {
        Canvas canvas = GetComponentInChildren<Canvas>();
        canvas.renderMode = RenderMode.ScreenSpaceOverlay;
        canvas.worldCamera = Camera.main;
        this.player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player.Player>();
        this.hpBar = GetComponent<EnemyHpBar>();
        hpBar.configMaxHP(hp);
    }

}
