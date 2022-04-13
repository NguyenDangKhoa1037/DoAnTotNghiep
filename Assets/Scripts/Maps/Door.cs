using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
     
    private STATUS_DOOR status;
    [SerializeField]  private int direction;

    private void Start()
    {
        status = STATUS_DOOR.IS_HIDEN;
    }
    public int Direction { 
        get => direction; 
        set {
            if (value != 1 && value != 2 && value != 4 && value != 8)
                throw new System.Exception("Direction invalid");
            direction = value;
        }
    }

    public STATUS_DOOR Status { 
        get => status;
        set {
            if (value == STATUS_DOOR.IS_HIDEN) hide();
            if (value == STATUS_DOOR.IS_OPENED) open();
            if (value == STATUS_DOOR.IS_BLOCKED) block();
        } }

    public void hide() {
        this.gameObject.SetActive(false);
    }

    public void block() {
        GetComponent<SpriteRenderer>().color = Color.black;
    }

    public void open() {
        GetComponent<SpriteRenderer>().color = Color.green;
    }
}


public class DOOR_DIRECTION
{
    static public int LEFT = 1;
    static public int DOWN = 2;
    static public int RIGHT = 4;
    static public int TOP = 8;


    static public int[] BASIC_DIRECTION = { TOP, RIGHT, DOWN, LEFT };

}

public enum STATUS_DOOR { 
    IS_HIDEN,
    IS_BLOCKED,
    IS_OPENED
}

