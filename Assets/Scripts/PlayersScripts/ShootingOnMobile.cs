using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingOnMobile : IShooting
{
    [SerializeField] bl_Joystick joyStickInput;
    [SerializeField] GameObject gunCore;
    // testing
    [SerializeField] SimpleBullet simpleBullet;
    public GameObject GunCore { get => gunCore; }
    public bl_Joystick JoyStickInput { get => joyStickInput;  }
    public SimpleBullet SimpleBullet { get => simpleBullet;}

    private void Update()
    {
        if (!JoyStickInput.IsHoliding) return;
        shoot();
    }

    private void shoot() {
        float v = JoyStickInput.Vertical;
        float h = JoyStickInput.Horizontal;

        float deg = Mathf.Atan2(v, h) * Mathf.Rad2Deg;

        GunCore.transform.rotation = Quaternion.Euler(new Vector3(0,0,deg));
    }

    protected override Bullet config()
    {
        //gameObject.GetComponent<SimpleBullet>();
        return SimpleBullet;
    }
}

public class ShootingInfo : MonoBehaviour { 
   
} 