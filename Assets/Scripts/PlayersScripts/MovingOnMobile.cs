using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingOnMobile : MonoBehaviour
{
    public float speed = 5f;

    [SerializeField] bl_Joystick joyStickInput;

    private void Update()
    {
        float ver = joyStickInput.Vertical;
        float hor = joyStickInput.Horizontal;
        if (!joyStickInput.IsHoliding) return;
        direction(ver, hor);
    }


    private void direction(float v, float h) {
        Vector2 dir = new Vector2(h, v);
        dir.Normalize();
        this.transform.Translate(dir * speed * Time.deltaTime);
    }

    public bl_Joystick JoyTick { get => joyStickInput; }
}
