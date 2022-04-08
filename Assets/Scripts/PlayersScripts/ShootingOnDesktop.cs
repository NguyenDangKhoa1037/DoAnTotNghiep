using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingOnDesktop : IShooting
{
    private GameObject gunCore;
    private SimpleBullet simpleBullet;
    public GameObject GunCore { get => gunCore; set => gunCore = value; }
    public SimpleBullet SimpleBullet { get => simpleBullet; set => simpleBullet = value; }

    private void Update()
    {
        rotationGun();
        if (Input.GetMouseButtonDown(0)) {
            this.handleShooting(Camera.main.ScreenToWorldPoint(Input.mousePosition)-transform.position);
        }
    }

    private void rotationGun() {
        Vector2 pos = Input.mousePosition - Camera.main.WorldToScreenPoint(transform.position);
        float deg = Mathf.Atan2(pos.y,pos.x) * Mathf.Rad2Deg;
        GunCore.transform.rotation = Quaternion.Euler(0,0,deg);
    }

    protected override Bullet config()
    {
        //gameObject.GetComponent<SimpleBullet>();
        return SimpleBullet;
    }
}
