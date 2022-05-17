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
        [SerializeField] private LayerMask playerMask;
        [SerializeField] MovingPlayerClassName movingType;
        [SerializeField] ShootingVersion shootingVersion;
        [SerializeField] Spirit spirit;
        private IMoving moving;
        private IShoot shooting;
        private HealthBar hpBar;
        public static Player instance;
        public string Name { get => _name; set => _name = value; }
        public int Hp { 
            get => hp;
            set {
                hp = value;
                hpBar.setHP(value);
            }
        }
        public float Speed { get => speed; set => speed = value; }
        public Spirit Spirit { get => spirit; set => spirit = value; }
        public LayerMask PlayerMask { get => playerMask; set => playerMask = value; }

        private void Awake()
        {
            if (instance == null) instance = this;
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

            //config health bar
            configHealthBar();
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
        private void configSpirit() {
            spirit.config(this, transform.GetChild(1).GetChild(0).transform);
        }

        private void configHealthBar() {
            hpBar = gameObject.AddComponent<HealthBar>();
            hpBar.configDefault(hp);
        }
        #endregion

        #region ENVENT
        private void OnGetDamaged(int damage) {
            int hp_clone = this.Hp;
            hp_clone -= damage;
            if (hp_clone < 0) hp_clone = 0;
            ShakeCamera.instance.shake();
            Hp = hp_clone;
        }
        #endregion

        public int attack() {
            // tinh toan sat thuong
            int damage = spirit.getDamage();
            return damage;
        }

        public void getDamaged(int damage) {
            OnGetDamaged(damage);
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