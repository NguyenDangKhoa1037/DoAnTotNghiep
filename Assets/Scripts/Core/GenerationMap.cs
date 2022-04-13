using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerationMap : MonoBehaviour
{
    [SerializeField] private Room roomTemplate;
    private Map myMap;
    private List<Room> rooms = new List<Room>();

    // Tao path mac dinh tu diem khoi dau den room boss
    int numberOfPath = 6;
    void genMainPath()
    {
        Room nextRoom = null;
        Room room = null;
        for (int i = 0; i < numberOfPath; i++)
        {
            room = i == 0 ? Instantiate(roomTemplate, this.transform.position,Quaternion.identity) : nextRoom;
            int dir = 0;
            do
            {
                int[] basics = DOOR_DIRECTION.BASIC_DIRECTION;
                dir = basics[Random.Range(0, basics.Length)];
            } while (room.addDirection(dir, true) ==null);
            rooms.Add(room);
            nextRoom = room.initRoom(roomTemplate, room.transform, dir);
            nextRoom.addDirection(RoomUtils.OP_DIR(dir));
            print(dir);
        }
    }


    // Tao cac path phu
    public void generateMap()
    {
        genMainPath();
    }

    private void Start()
    {
        generateMap();
    }
}
