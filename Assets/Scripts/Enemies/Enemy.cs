using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    [SerializeField] protected float speed;
    [SerializeField] protected int hp;
    [SerializeField] protected string _name;
    [SerializeField] protected int level;
    [SerializeField] protected int damage;
    protected Transform player;

    private void Awake()
    {
        findPlayer();
    }

    protected void findPlayer() {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }
}
