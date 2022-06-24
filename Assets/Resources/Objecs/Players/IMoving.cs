using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    public abstract class IMoving : MonoBehaviour
    {
        //protected float speed;
        protected ChangeAnimation mangerAnimator;
        protected SpiritV2 spirit;
        protected Player player;

        virtual public void configSpeed(float sp) {
            return;
        }
        virtual public float getSpeed() {
            return 0;
        }
        protected virtual void runAwake() {
            mangerAnimator = GetComponent<ChangeAnimation>();
            if (mangerAnimator == null) mangerAnimator = gameObject.AddComponent<ChangeAnimation>();
        }
        public void setAttributes(Player player)
        {
            //speed = player.Speed;
            this.spirit = player.Spirit;
            this.player = player;
        }
        virtual public void forceStop() {
            throw new NotImplementedException();
        }
       
    }
}