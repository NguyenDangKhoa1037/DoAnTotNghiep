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
        [SerializeField] private LayerMask mapMask;
        [SerializeField] MovingPlayerClassName movingType;
        [SerializeField] ShootingVersion shootingVersion;
        [SerializeField] SpiritV2 spirit;
        [SerializeField] private IMoving moving;
        [SerializeField] private IShoot shooting;
        [SerializeField] private GameOver gameOver;
        private HealthBar hpBar;
        public static Player instance;
        public string Name { get => _name; set => _name = value; }
        public int Hp
        {
            get => hp;
            set
            {
                hp = value;
                hpBar.setHP(value);
            }
        }
        public float Speed { get => speed; set => speed = value; }
        public SpiritV2 Spirit { get => spirit; set => spirit = value; }
        public LayerMask PlayerMask { get => playerMask; set => playerMask = value; }
        public LayerMask MapMask { get => mapMask; set => mapMask = value; }

        private void Awake()
        {
            if (instance == null) instance = this;
            config();
        }
        #region Config for player
        void config()
        {

            // config moving
            configMoving();

            //config shooting manager
            configShooting();

            // config spirit
            configSpirit();

            //config health bar
            configHealthBar();
        }
        void configMoving()
        {
            if (moving == null && movingType == MovingPlayerClassName.Moving_PC_V1)
                moving = gameObject.AddComponent<Moving_PC_V1>();

            moving.setAttributes(this);
        }

        void configShooting()
        {
            if (shooting == null && shootingVersion == ShootingVersion.Shooting_PC_V1)
                shooting = gameObject.AddComponent<Shooting_PC_V1>();

            shooting.setAttributes(this);
        }
        private void configSpirit()
        {
            spirit.config(this, transform);
        }

        private void configHealthBar()
        {
            hpBar = gameObject.AddComponent<HealthBar>();
            hpBar.configDefault(hp);
        }
        #endregion

        #region ENVENT
        private void OnGetDamaged(int damage)
        {
            int hp_clone = this.Hp;
            hp_clone -= damage;
            if (hp_clone <= 0)
            {
                hp_clone = 0;
                OnDie();
            }
            if(ShakeCamera.instance != null)
            ShakeCamera.instance.shake();
            Hp = hp_clone;
        }

        private void OnDie()
        {
            gameOver.gameOver();
        }
        #endregion

        #region Public method

        public int attack()
        {
            // tinh toan sat thuong
            int damage = spirit.getDamage();
            return damage;
        }

        public void getDamaged(int damage)
        {
            OnGetDamaged(damage);
        }
        public void setForceStopMoving()
        {
            moving.forceStop();
        }

        public int dame()
        {
            return shooting.getDame();
        }
        public void enableIMoving(bool enabled)
        {
            moving.enabled = enabled;
        }
        public void enableIShooting(bool enabled)
        {
            shooting.enabled = enabled;
        }

        public void configSpeed(float sp)
        {
            moving.configSpeed(sp);
        }
        public float getSpeed()
        {
            return moving.getSpeed();
        }
        #endregion
    }

    public enum MovingPlayerClassName
    {
        Moving_PC_V1,
        Moving_PC_V2,
    }

    public enum ShootingVersion
    {
        Shooting_PC_V1,
        Shooting_PC_V2
    }
}