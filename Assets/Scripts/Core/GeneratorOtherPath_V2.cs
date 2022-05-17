using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace RandomGenerationMap_V2
{
    public class GeneratorOtherPath_V2 : MonoBehaviour, GeneratorOtherPath
    {
        private ConfigLevel config;
        private List<Room> rooms = new List<Room>();
        private int numberOtherRooms;
        private Room bossRoom = null;

        public void generate(ConfigLevel config)
        {
            this.config = config;
            config.Rooms.ForEach(e =>
            {
                rooms.Add(e);
            });
            numberOtherRooms = 2 * config.NumberOfRoom / 3;
            removeBossRoom();
            gen();
            config.Rooms = rooms;
            config.Rooms.Add(bossRoom);
        }
        private void removeBossRoom()
        {

            rooms[rooms.Count - 1].Type = RoomType.BOSS_ROOM;
            bossRoom = rooms[rooms.Count - 1];
            rooms.RemoveAt(rooms.Count - 1);
        }
        Room pickRoom()
        {
            return config.ListRoomTemplate[Random.Range(0, config.ListRoomTemplate.Length)];
        }
        Room createNewRoom(Room template, Vector2 pos, int index)
        {
            Room room = null;
            room = Instantiate(template, pos, Quaternion.identity);
            room.name = "Room " + index;
            return room;
        }
        private void gen() {
            Room currentRoom = rooms[Random.Range(0, rooms.Count)];
            Room newRoom = null;
            Door currentDoor = null;
            Door newDoor = null;
            int direction = -1;
            int countOtherPath = Random.Range(2, 5);

            for (int i = 0; i < numberOtherRooms; i++)
            {
                // B2
                int[] basicDirections = DOOR_DIRECTION.BASIC_DIRECTION;
                direction = basicDirections[Random.Range(0, basicDirections.Length)];

                // Kiem tra huong nay da duoc dung hay chua
                currentDoor = currentRoom.pickDoor(direction);
                if (currentDoor.Status != STATUS_DOOR.IS_HIDEN) { i--; continue; }

                // B3
                newRoom = pickRoom();
                newRoom = createNewRoom(newRoom, newRoom.getPostionRoom(currentDoor), rooms.Count);
                RoomChecker checker = newRoom.GetComponent<RoomChecker>();

                // B4
                Collider2D colider = checker.getColiderOnSpace(newRoom.getPostionRoom(currentDoor));

                // co ton tai phong o huong dang xet
                // Tien hanh gop nhanh
                if (colider != null)
                {
                    
                    Room room = colider.GetComponent<Room>();
                    Door door = room.pickDoor(RoomUtils.OP_DIR(direction));
                    if (door.Status == STATUS_DOOR.IS_HIDEN)
                    {
                        setRelationship(currentRoom, currentDoor, room, door);
                        rooms.Add(room);
                        currentRoom = rooms[Random.Range(0, rooms.Count)];
                    }
                    Destroy(newRoom.gameObject); i--; continue;
                }

               
                // B5
                if (!checker.isFitSpace(newRoom.getPostionRoom(currentDoor)))
                {
                    Destroy(newRoom.gameObject); i--; continue;
                };
                // B6
                checker.CanCheck = true;
                newRoom.transform.position = newRoom.getPostionRoom(currentDoor);
                newDoor = newRoom.pickDoor(RoomUtils.OP_DIR(direction));
                rooms.Add(newRoom);

                setRelationship(currentRoom, currentDoor, newRoom, newDoor);
                currentRoom = newRoom;
                countOtherPath--;

            }
            rooms.Add(config.Rooms[config.Rooms.Count - 1]);
        }
        private void setRelationship(Room room1, Door door1, Room room2, Door door2)
        {

            door1.Room = room2;
            door2.Room = room1;

            door1.Status = STATUS_DOOR.IS_BLOCKED;
            door2.Status = STATUS_DOOR.IS_BLOCKED;

            //door1.startLine();

        }
    }
}