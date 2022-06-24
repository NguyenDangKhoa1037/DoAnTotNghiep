using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mud_effection : Effection
{
    public override bool canPushed(EffectionManager manager)
    {
        for (int i = 0; i < manager.Effections.Count; i++) 
            if (manager.Effections[i].GetType() == typeof(Mud_effection)) return false; 
        
        return true;
    }
    Player.Player player;
    private float speed;
    [SerializeField] float timeEnd = 2f;
    string _name = "Mud_effection";
    private void Start()
    {
        player = GetComponent<Player.Player>();
        speed = player.getSpeed();
        player.configSpeed ( speed * 40f / 100);
        EffectionManager.instance.Effections.Add(_name);
        StartCoroutine(endEffect());
    }

    IEnumerator endEffect() {
        yield return new WaitForSeconds(timeEnd);
        print(speed);
        player.configSpeed(speed);
        EffectionManager.instance.Effections.Remove(_name);
        Destroy(this);
    }
}
