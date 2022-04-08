using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class IShooting : MonoBehaviour
{
    protected Bullet myBullet;
    private Transform player;
    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("EntryBullet").transform;
    }
    private void Start()
    {
        myBullet = config();   
    }
    protected abstract Bullet config();
    public void handleShooting(Vector2 target) {
        GameObject obj = Instantiate(myBullet.Prefab, player.position, Quaternion.identity) ;
        Bullet bullet = obj.GetComponent<Bullet>();
        bullet.startMove(target);
    }
}
