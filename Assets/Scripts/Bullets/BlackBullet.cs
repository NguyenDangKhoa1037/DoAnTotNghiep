using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackBullet : Player.Bullet
{
    [Header("Black Bullet Information")]
    [SerializeField] Vector2 offsetWhenFly;
    [SerializeField] Vector2 defaultOffset;
    [SerializeField] Transform shadow;
    [SerializeField] GameObject ParentDot;
    [SerializeField] float timeExists;
    [SerializeField] float speedRotate;
    [SerializeField] float detailSpeedShadowDescrease;
    [SerializeField] GameObject effectExplosion;

    private float count;
    private Rigidbody2D myBody;
    private Vector2 offset;
    private float speedDescreaseShadow;
    private bool descreaseShadow = true;
    private void Awake()
    {
        myBody = GetComponent<Rigidbody2D>();
        count = timeExists;
        speedDescreaseShadow = Mathf.Abs(offsetWhenFly.y - defaultOffset.y) / (timeExists-0.1f);
    }
    public override void startMove(Vector2 direction)
    {
        offset = offsetWhenFly;
        this.target = direction;
        myBody.AddForce(direction * speed,ForceMode2D.Impulse);
    }
    private float timer = 0;
    private void Update()
    {
        timer += Time.deltaTime;
        if (timer >= timeExists) {
            Instantiate(effectExplosion, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }

    private void LateUpdate()
    {
        offset.y += speedDescreaseShadow*Time.deltaTime* detailSpeedShadowDescrease * (descreaseShadow ? 1: -1);
        if (descreaseShadow && offset.y >= defaultOffset.y)
        {
            offset.y = defaultOffset.y;
            myBody.drag = 1f;
        }
        else if (!descreaseShadow && offset.y <= 2 * offsetWhenFly.y/3) {
            offset.y = 2 * offsetWhenFly.y / 3;
            detailSpeedShadowDescrease /= 3f;
            descreaseShadow = true;
        }
        shadow.localPosition = (Vector3)offset; 

        count -= Time.deltaTime;
        if (count <= 0) count = 0;
        ParentDot.transform.Rotate(new Vector3(0, 0,speedRotate));

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player")) return;
        detailSpeedShadowDescrease *= 3f;
        descreaseShadow = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            Instantiate(effectExplosion, transform.position, Quaternion.identity);
            Destroy(gameObject);
            return;
        }
    }
}
