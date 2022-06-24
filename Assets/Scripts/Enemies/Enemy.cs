using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Enemy
{
    public abstract class Enemy : MonoBehaviour
    {
        [Header("Main information")]
        [SerializeField] protected float speed;
        [SerializeField] protected int hp;
        [SerializeField] protected string _name;
        [SerializeField] protected int level;
        [SerializeField] private int damage;

        [SerializeField] protected GameObject effectOnDead;


        protected Player.Player player;
        protected EnemyHpBar hpBar;
        protected Room myRoom;
        #region Setter nad getter
        protected int Hp
        {
            get => hp;
            set
            {
                if (value <= 0)
                {
                    OnDead();
                }
                else
                {
                    hp = value;
                    hpBar.setHp(hp);
                }
            }
        }

        public int Damage { get => damage; set => damage = value; }

        #endregion
        private void Awake()
        {
            runAwake();
        }

        protected virtual void runAwake()
        {
            findPlayer();
            hpBar = GetComponent<EnemyHpBar>();
            if (hpBar == null) hpBar = gameObject.AddComponent<EnemyHpBar>();
            hpBar.configMaxHP(hp);
        }

        protected void runStart() { }

        protected void findPlayer()
        {
            player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player.Player>();
        }

        private void configColorEffect(GameObject effect)
        {
            SpriteRenderer sprite = transform.GetChild(0).GetComponent<SpriteRenderer>();
            Color myColor = sprite != null ? sprite.color : GetComponent<SpriteRenderer>().color;
            int childs = effect.transform.GetChild(0).childCount;
            for (int i = 0; i < childs; i++) {
                SpriteRenderer render = effect.transform.GetChild(0).GetChild(i).GetComponent<SpriteRenderer>();
                render.color = myColor;
            }
            ParticleSystem particleSystem = effect.GetComponentInChildren<ParticleSystem>();
            var main = particleSystem.main;
            main.startColor = myColor;
        }

        public void getDamaged(int damage)
        {
            int HpClone = Hp;
            HpClone -= damage;
            if (HpClone < 0) HpClone = 0;
            Hp = HpClone;
        }

       virtual protected void OnDead() {
            GameObject effect = Instantiate(effectOnDead,transform.position,Quaternion.identity);
            configColorEffect(effect);
            ShakeCamera.instance.shake();
            myRoom.Enemies.Remove(this);
            myRoom.destroyEnemy(this);
            Destroy(gameObject);
        }

        protected virtual void handleTriggerEnter(Collider2D collision) {
           
            if (collision.CompareTag("MyBullet"))
            {
                OnGetDamaged(collision.gameObject);
            }
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            handleTriggerEnter(collision);
        }

        virtual protected void OnGetDamaged(GameObject bullet) {
            int damage = player.attack();
            getDamaged(damage);
            Destroy(bullet);
        }
        virtual public void OnInited(Room room) {
            myRoom = room;
        }
    }
}