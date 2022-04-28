using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player {
    public class Shooting_PC_V1 : IShoot
    {
        private float countdown;
        public bool isShooting = false;
        private void LateUpdate()
        {
            handleShooting();
            if (isShooting) { 
                rotatePoint();
                countdown -= Time.deltaTime;
                if(countdown <= 0)
                {
                    isShooting = false;
                    spirit.State = Spirit.END_SHOT;
                    countdown = spirit.WaitingShootTime;
                }
            }
        }

        private void handleShooting() {
            if (Input.GetMouseButtonDown(0))
            {
                if (isShooting)
                {
                    spirit.shoot();
                }
                else if (!isShooting)
                {
                    this.spirit.State = Spirit.TELEPORT;
                }
                countdown = spirit.WaitingShootTime;
                isShooting = true;
            }
        }

        private void rotatePoint() {
            Vector2 pos = Input.mousePosition - Camera.main.WorldToScreenPoint(transform.position);
            float deg = Mathf.Atan2(pos.y, pos.x) * Mathf.Rad2Deg;
            PointSpiritInPlayer.transform.rotation = Quaternion.Euler(0, 0, deg);
        }
    }
}