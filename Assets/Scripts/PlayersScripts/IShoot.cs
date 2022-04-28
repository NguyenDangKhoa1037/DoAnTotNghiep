using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    public abstract class IShoot : MonoBehaviour
    {
        protected Spirit spirit;
        protected Transform PointSpiritInPlayer;
        protected void runAwake()
        {
        }
        public void setAttributes(Player player)
        {
            this.spirit = player.Spirit;
            PointSpiritInPlayer = player.transform.GetChild(1).transform;
        }

        
    }
}
