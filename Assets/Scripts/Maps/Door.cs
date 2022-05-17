using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{

    [SerializeField] private STATUS_DOOR status = STATUS_DOOR.IS_HIDEN;
    [SerializeField] private int direction;
    [SerializeField] private float distanceForCheck = 1f;
    [SerializeField] private GameObject colider;
    [SerializeField] private GameObject background;
    [SerializeField] private Transform switchPos;
    private Room room;
    private LineRenderer line;
    private bool isDrawing = false;
    private void Awake()
    {
        if (colider == null)
            colider = transform.GetChild(2).gameObject;
        if (background == null)
            background = transform.GetChild(1).gameObject;
        if (SwitchPos == null)
            SwitchPos = transform.GetChild(3);
    }
    public int Direction
    {
        get => direction;
        set
        {
            if (value != 1 && value != 2 && value != 4 && value != 8)
                throw new System.Exception("Direction invalid");
            direction = value;
        }
    }

    public STATUS_DOOR Status
    {
        get => status;
        set
        {
            if (value == STATUS_DOOR.IS_HIDEN) hide();
            if (value == STATUS_DOOR.IS_OPENED) open();
            if (value == STATUS_DOOR.IS_READY) ready();
            if (value == STATUS_DOOR.IS_BLOCKED) block();
            status = value;
        }
    }

    public Room Room { get => room; set => room = value; }
    public bool IsDrawing { get => isDrawing; set => isDrawing = value; }
    public float DistanceForCheck { get => distanceForCheck; set => distanceForCheck = value; }
    public Transform SwitchPos { get => switchPos; set => switchPos = value; }

    public void hide()
    {
        //this.gameObject.SetActive(false);
        GetComponent<SpriteRenderer>().color = Color.white;
        colider.SetActive(false);
        background.SetActive(false);
    }

    public void block()
    {
        background.SetActive(false);
    }
    public void ready() {
        colider.SetActive(true);
        background.SetActive(false);
    }
    public void open()
    {
        colider.SetActive(false);
        background.SetActive(true);
    }
    public bool isActive()
    {
        return Room != null;
    }
    public Vector2 getPoint()
    {
        return transform.GetChild(0).transform.position;
    }
    public void drawLine()
    {
        if (Room == null) return;
        line.SetPosition(0, this.transform.position);
        line.SetPosition(1, this.Room.pickDoor(RoomUtils.OP_DIR(Direction)).transform.position);

    }
    public void startLine()
    {
        line = this.gameObject.AddComponent<LineRenderer>();
        line.startColor = Color.black;
        line.endColor = Color.black;
        line.startWidth = 0.5f;
        line.endWidth = 0.5f;
        isDrawing = true;
    }
    private void Update()
    {
        //if (isDrawing)
        //    drawLine();

        if (Status == STATUS_DOOR.IS_OPENED) {
            bool checker = Physics2D.OverlapCircle(SwitchPos.position, DistanceForCheck, Player.Player.instance.PlayerMask);

            if (checker) {
                DemoMap.instance.switchRoom(Room, this);
            }
        }
    }
    //private void OnDrawGizmos()
    //{
    //    if (Status == STATUS_DOOR.IS_OPENED)
    //    {
    //        Gizmos.DrawSphere(SwitchPos.position, DistanceForCheck);
    //    }
    //}
}


public class DOOR_DIRECTION
{
    static public int LEFT = 1;
    static public int DOWN = 2;
    static public int RIGHT = 4;
    static public int TOP = 8;


    static public int[] BASIC_DIRECTION = { TOP, RIGHT, DOWN, LEFT };

}

public enum STATUS_DOOR
{
    IS_HIDEN,
    IS_BLOCKED,
    IS_READY,
    IS_OPENED
}

