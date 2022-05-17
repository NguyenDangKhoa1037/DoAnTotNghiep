using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RandomGenerationMap_V2
{
    public class GeneratorMainPath_V1 : MonoBehaviour, GeneratorMainPath
    {
        List<Room> rooms = new List<Room>();
        private ConfigLevel config;
        public void generate(ConfigLevel config)
        {
            this.config = config;
            rooms = config.Rooms;
            gen();
        }
        Room createNewRoom(Room template, Vector2 pos, string name)
        {
            Room room = Instantiate(template, pos, Quaternion.identity);
            room.name = name;
            return room;
        }
        void initStartedRoom()
        {
            Room room = createNewRoom(config.ListRoomTemplate[0], Vector2.zero, "Room 0");
            rooms.Add(room);
        }
        Room pickRoom()
        {
            return config.ListRoomTemplate[Random.Range(0, config.ListRoomTemplate.Length)];
        }
        private void gen() {
            initStartedRoom();
            Room currentRoom = rooms[0];
            Room newRoom = null;
            Door currentDoor = null;
            Door newDoor = null;
            int direction = -1;

            for (int i = 0; i < config.NumberOfRoom / 3; i++)
            {
                // B1
                int[] basicDirections = DOOR_DIRECTION.BASIC_DIRECTION;
                direction = basicDirections[Random.Range(0, basicDirections.Length)];

                // B2
                if (currentRoom.pickDoor(direction).Status != STATUS_DOOR.IS_HIDEN) { i--; continue; };

                // B3
                newRoom = pickRoom();
                currentDoor = currentRoom.pickDoor(direction);
                newRoom = createNewRoom(newRoom, newRoom.getPostionRoom(currentDoor), "Room " + (i + 1));
                RoomChecker checkerRoom = newRoom.GetComponent<RoomChecker>();

                // B4
                bool fit = checkerRoom.isFitSpace(newRoom.getPostionRoom(currentDoor));
                if (!fit) { Destroy(newRoom.gameObject); i--; continue; };
                checkerRoom.CanCheck = true;
                // B5
                newRoom.transform.position = newRoom.getPostionRoom(currentDoor);
                newRoom.Index = currentRoom.Index + 1;
                newDoor = newRoom.pickDoor(RoomUtils.OP_DIR(direction));
                rooms.Add(newRoom);


                currentDoor.Room = newRoom;
                newDoor.Room = currentRoom;

                currentDoor.Status = STATUS_DOOR.IS_BLOCKED;
                newDoor.Status = STATUS_DOOR.IS_BLOCKED;

                currentDoor.startLine();

                currentRoom = newRoom;
            }
        }
    }
}
