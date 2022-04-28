using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Player
{
    public class Player : MonoBehaviour
    {
        [Header("Information")]
        [SerializeField] private string _name;
        [SerializeField] private int hp;
        [SerializeField] private float speed;
        [SerializeField] MovingPlayerClassName movingType;
        [SerializeField] ShootingVersion shootingVersion;
        [SerializeField] Spirit spirit;
        private IMoving moving;
        private IShoot shooting;

        public string Name { get => _name; set => _name = value; }
        public int Hp { get => hp; set => hp = value; }
        public float Speed { get => speed; set => speed = value; }
        public Spirit Spirit { get => spirit; set => spirit = value; }

        private void Awake()
        {
            config();
        }
        #region Config for player
        void config() {

            // config moving
            configMoving();

            //config shooting manager
            configShooting();

            // config spirit
            configSpirit();

        }
        void configMoving() {
            if (movingType == MovingPlayerClassName.Moving_PC_V1)
                moving = gameObject.AddComponent<Moving_PC_V1>();

            moving.setAttributes(this);
        }

        void configShooting()
        {
            if (shootingVersion == ShootingVersion.Shooting_PC_V1)
                shooting = gameObject.AddComponent<Shooting_PC_V1>();

            shooting.setAttributes(this);
        }
        #endregion

        private void configSpirit() {
            spirit.config(this, transform.GetChild(1).GetChild(0).transform);
        }
    }

    public enum MovingPlayerClassName
    {
        Moving_PC_V1
    } 
    
    public enum ShootingVersion
    {
        Shooting_PC_V1
    }
}