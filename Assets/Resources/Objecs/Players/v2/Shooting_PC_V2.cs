using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Player
{
    public class Shooting_PC_V2 : IShoot
    {
        [SerializeField]
        private GameObject arrow;
        [SerializeField]
        private BlackBullet bulletObj;
        [SerializeField]
        private ChangeAnimation animationManager;

        private int status = 0;

        private const int NONE = 0;
        private const int BEGIN_SHOOT = 1;
        private const int SHOOT = 2;
        private const int WAITING = 4;
        private const int END_SHOOT = 5;


        private Vector2 directionShoot = Vector2.zero;
        private GameObject arrowInstance;
        private ZoomCamera zoomCamera;
        private void Awake()
        {
            runAwake();
            zoomCamera =  Camera.main.GetComponent<ZoomCamera>();
        }

        public override void onBeginShoot(ShootingMassage massage)
        {
        }

        public override void onEndShoot(ShootingMassage massage)
        {
        }

        void handleLeftMouseEvent()
        {
            if (Input.GetMouseButtonDown(0)) {
                if (status == NONE) {
                    status = BEGIN_SHOOT;
                }else if (status == WAITING)
                {
                    status = SHOOT;
                    shooting();
                }
            } 
        }

        void handleEndShooting()
        {
            if (status == NONE) return;

            if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.S))
                status = END_SHOOT;
        }



        private void Update()
        {
            handleEndShooting();
            handleLeftMouseEvent();
            stateManager();
        }


        private void stateManager()
        {
            if (status == BEGIN_SHOOT) beginShoot();
            else if (status == WAITING) waiting();
            else if (status == END_SHOOT) endShoot();
        }


        private void beginShoot() {
            if (arrowInstance == null) {
                arrowInstance = Instantiate(arrow, transform.position, Quaternion.identity);
                arrowInstance.SetActive(false);
                Vector2 scale = arrowInstance.transform.localScale;
                arrowInstance.transform.SetParent(transform);
                arrowInstance.transform.localScale = scale;
            }
            arrowInstance.SetActive(true);
            player.setForceStopMoving();
            zoomCamera.zoom();
            status = WAITING;
        }


        private void waiting() {
            Vector2 mousePostion = Camera.main.ScreenToWorldPoint(Input.mousePosition); //+ DemoMap.instance.Cinemachine.transform.position;
            directionShoot = (Vector3)mousePostion - transform.position;
            float deg = Mathf.Atan2(directionShoot.y, directionShoot.x) * Mathf.Rad2Deg;
            arrowInstance.transform.rotation = Quaternion.Euler(0, 0, deg);
        }

        private void shooting()
        {
            bool up = directionShoot.y >= 0;
            //bool down = directionShoot.y < 0;
            bool left = directionShoot.x <= 0;
            //bool right = directionShoot.x < 0;
            string animationName = "Player_body_";
            animationName += up ? "top" : "down";
            animationName += left ? "left" : "right";
            animationName += "_attack";
            player.enableIMoving(false);
            animationManager.play(animationName);
        }
        void shooted() {

            Vector2 mousePostion = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            directionShoot = (Vector3)mousePostion - transform.position;
            directionShoot.Normalize();
            Bullet bullet = Instantiate(bulletObj, arrowInstance.transform.GetChild(0).position, Quaternion.identity);
            bullet.startMove(directionShoot);
            status = WAITING ;
            player.enableIMoving(true);

        }
        private void endShoot() {
            arrowInstance.SetActive(false);
            zoomCamera.stopZoom();
            status = NONE;
        }

        public override int getDame()
        {
            return bulletObj.Damage;
        }

    }
}