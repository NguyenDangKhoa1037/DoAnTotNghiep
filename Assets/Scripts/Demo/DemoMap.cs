using Cinemachine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemoMap : MonoBehaviour
{
    [SerializeField] Player.Player player;
    [SerializeField] GameObject spirit;
    [SerializeField] MovingCamera movingCam;
    [SerializeField] GameObject loading;
    private List<Room> rooms;
    private Room currentRoom;
    public static DemoMap instance;

    public void nextLevel()
    {
        GameInfo.instance.Level++;
    }
    public int getCountEnemies() {
        return GameInfo.instance.Level + 2;
    }

    public int getCountRoom()
    {    
        return 4 + GameInfo.instance.Level;
    }
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
            
            movingCam.Target = currentRoom.transform;
            MapController.instances.beginRoom(currentRoom);

        }
    }


    private void Awake()
    {
        if (instance == null) instance = this;
        player.transform.GetChild(0).gameObject.SetActive(false);
        loading.SetActive(true);
        player.gameObject.SetActive(false);
    }

    internal void begin(List<Room> finishRooms)
    {
        print("Start game");
        loading.SetActive(false);

        player.gameObject.SetActive(true);
        player.transform.GetChild(0).gameObject.SetActive(true);
        // config danh sach phong va phong duoc chon
        Rooms = finishRooms;
        player.transform.position = CurrentRoom.transform.position;
        spirit.transform.position = CurrentRoom.transform.position+ (Vector3)Vector2.up;

        // thiet lap chi cinemachine
        movingCam.Target = player.transform;
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

        movingCam.Target = player.transform;
    }
}
