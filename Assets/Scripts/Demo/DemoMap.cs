using Cinemachine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemoMap : MonoBehaviour
{
    [SerializeField] Player.Player player;
    [SerializeField] Spirit spirit;
    [SerializeField] Cinemachine.CinemachineVirtualCamera cinemachine;
    private List<Room> rooms;
    private Room currentRoom;
    public static DemoMap instance;

    public List<Room> Rooms
    {
        get => rooms; set
        {
            CurrentRoom = value[0];
            rooms = value;
        }
    }

    public Room CurrentRoom { get => currentRoom; 
        set {
            if (currentRoom != null) currentRoom.gameObject.SetActive(false);
            currentRoom = value;
            if (currentRoom != null) currentRoom.gameObject.SetActive(true);
            
            Cinemachine.Follow = currentRoom.transform;
            Cinemachine.CinemachineConfiner confiner = Cinemachine.GetComponent<Cinemachine.CinemachineConfiner>();
            confiner.m_BoundingShape2D = currentRoom.CinemachineBound;
            MapController.instances.beginRoom(currentRoom);

        }
    }

    public CinemachineVirtualCamera Cinemachine { get => cinemachine; set => cinemachine = value; }

    private void Awake()
    {
        if (instance == null) instance = this;
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            CurrentRoom.Status = STATUS_ROOM.IS_CLEAN;
        }
    }

    internal void begin(List<Room> finishRooms)
    {
        print("Start game");
        // config danh sach phong va phong duoc chon
        Rooms = finishRooms;
        player.transform.position = CurrentRoom.transform.position;
        spirit.transform.position = CurrentRoom.transform.position+ (Vector3)Vector2.up;

        // thiet lap chi cinemachine
        Cinemachine.Follow = player.transform;
    }
    public void switchRoom(Room nextRoom, Door currentDoor) {
        Door nextDoor = nextRoom.pickDoor(RoomUtils.OP_DIR(currentDoor.Direction));
        Vector2 pos = nextDoor.SwitchPos.position;
        if (currentDoor.Direction == DOOR_DIRECTION.DOWN) pos.y -= (nextDoor.DistanceForCheck + 0.7f); 
        if (currentDoor.Direction == DOOR_DIRECTION.TOP) pos.y += (nextDoor.DistanceForCheck + 0.7f); 
        if (currentDoor.Direction == DOOR_DIRECTION.LEFT) pos.x -= (nextDoor.DistanceForCheck + 0.7f); 
        if (currentDoor.Direction == DOOR_DIRECTION.RIGHT) pos.x += (nextDoor.DistanceForCheck + 0.7f);

        nextDoor.Status = STATUS_DOOR.IS_OPENED;


        // di chuyen camera
        CurrentRoom = nextRoom;


        // dic huyen player;
        Player.Player.instance.Spirit.switchRoom(pos + new Vector2(0,0.2f));
        Player.Player.instance.Spirit.switchRoom(pos + new Vector2(0,0.2f));
        Player.Player.instance.transform.position = pos;

        Cinemachine.Follow = player.transform;
    }
}
