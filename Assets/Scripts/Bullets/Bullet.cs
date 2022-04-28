using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Bullet : MonoBehaviour
{
    [SerializeField] GameObject prefab;
    protected BulletInfo info;
    private void Awake()
    {
        info = loadInfo();
    }
    protected abstract BulletInfo loadInfo();
    public void startMove(Vector2 target) {
        if (info == null) info = loadInfo();
        target.Normalize();
        move(target);
    }
    protected abstract void move(Vector2 target); 
    public GameObject Prefab { get => prefab; }
}


public class BulletInfo {
    private float speed;
    private float damge;
    private float timeCount;

    public BulletInfo(float speed, float damge, float timeCount)
    {
        this.speed = speed;
        this.damge = damge;
        this.timeCount = timeCount;
    }

    public float Speed { get => speed; set => speed = value; }
    public float Damge { get => damge; set => damge = value; }
    public float TimeCount { get => timeCount; set => timeCount = value; }
}

namespace Player {
    public abstract class Bullet:MonoBehaviour {
        [Header("Information")]
        [SerializeField] protected float speed;
        [SerializeField] protected int damage;


        protected Player player;
        protected Vector2 target;
        protected bool isMoving = false;
        public void config(Player player) {
            this.player = player;
        }

        public abstract void move();

    }
}