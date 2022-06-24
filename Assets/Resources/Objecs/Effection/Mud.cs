using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mud : MonoBehaviour
{
    [SerializeField] float timeExists = 4.5f;
    private void Start()
    {
        Destroy(gameObject, timeExists);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) {
            if(EffectionManager.instance.canPushed("Mud_effection"))
            EffectionManager.instance.addEffect(new EffectionManager.CreateEffection(createMudEffection));
        }
    }

    void createMudEffection(EffectionManager effectionManager) {
        Mud_effection mud= effectionManager.gameObject.AddComponent<Mud_effection>();
    }
}
