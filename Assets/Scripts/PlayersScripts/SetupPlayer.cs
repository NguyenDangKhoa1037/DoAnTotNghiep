using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetupPlayer : MonoBehaviour
{
    private bl_Joystick joyStickMoving;
    private bl_Joystick joyStickShooting;
    private void Awake()
    {

#if UNITY_EDITOR
        setupOnDesktop();
#endif

    }
    private void setupOnDesktop()
    {
        MovingOnMobile movingController = GetComponent<MovingOnMobile>();
        ShootingOnMobile directingGun = GetComponent<ShootingOnMobile>();

        joyStickMoving = movingController.JoyTick;
        joyStickShooting = directingGun.JoyStickInput;

        // setup for moving
        MovingOnDesktop moving = gameObject.AddComponent<MovingOnDesktop>();
        moving.config(movingController.speed);
        movingController.enabled = false;
        joyStickMoving.gameObject.SetActive(false);

        // setup for shooting
        ShootingOnDesktop shootingOnDesktop = gameObject.AddComponent<ShootingOnDesktop>();
        shootingOnDesktop.GunCore = directingGun.GunCore;
        shootingOnDesktop.SimpleBullet = directingGun.SimpleBullet;
        directingGun.enabled = false;
        joyStickShooting.gameObject.SetActive(false);
    }
}
