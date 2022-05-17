using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    public class NormalBullet : Bullet
    {
        private Vector2 direction;
        public override void startMove(Vector2 mousePostion)
        {
            this.isMoving = true;
            this.direction = mousePostion - (Vector2)player.transform.position ;
            this.direction.Normalize();
            Destroy(gameObject, 5f);
            
        }
        private void Update()
        {
            transform.Translate(direction * speed * Time.deltaTime);
        }
        //private void OnTriggerEnter2D(Collider2D collision)
        //{
        //    if (collision.CompareTag("Spirit")) return;
        //    Destroy(gameObject);
        //}
    }
}
