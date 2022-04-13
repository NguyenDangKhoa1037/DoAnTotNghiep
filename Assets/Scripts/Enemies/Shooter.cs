using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : Enemy
{
    [SerializeField] private GameObject gunCore;
    [SerializeField] private float seconds;
    [SerializeField] private EnemyBullet bullet;
    private Transform gun;
    private EnemyBullet bulletObj;


    IEnumerator shoot(float seconds) {
        yield return new WaitForSeconds(seconds);
        Vector2 dir = gun.position - this.transform.position;
        dir.Normalize();
        bulletObj.transform.position = this.gunCore.transform.position;
        bulletObj.gameObject.SetActive(true);
        bulletObj.startMove(dir);
        StartCoroutine(shoot(seconds + Random.Range(0, 1.5f)));
    }

    private void Start()
    {
        StartCoroutine(shoot(seconds + Random.Range(0, 1.5f)));
    }
    private void Awake()
    {
        runAwake();
        gun = gunCore.transform.GetChild(0).transform;
        bulletObj = Instantiate(bullet, this.transform.position, Quaternion.identity);
        bulletObj.gameObject.SetActive(false);
        bulletObj.config(new BulletInfo(8f, this.damage, 0));
    }
    private void Update()
    {
        Vector2 dir = player.position - this.transform.position;
        float angel = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        gunCore.transform.rotation = Quaternion.Euler(0, 0, angel);
        bulletObj = Instantiate(bullet, this.transform.position, Quaternion.identity);
        bulletObj.gameObject.SetActive(false);
        bulletObj.config(new BulletInfo(8f, this.damage, 0));
    }
}
