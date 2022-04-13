using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[SerializeField]
public class Room : MonoBehaviour
{
    [SerializeField] List<Door> listDoor = new List<Door>();
    private RoomType type;
    private float boundX;
    private float boundY;

    private void Awake()
    {
        boundX = GetComponent<SpriteRenderer>().bounds.size.x;
        boundY = GetComponent<SpriteRenderer>().bounds.size.y;

    }
    public RoomType Type
    {
        get => type;
        set => type = value;
    }
    public Door addDirection(int dir, bool checkColider = false)
    {
        for (int i = 0; i < listDoor.Count; i++)
        {
            if (listDoor[i].Direction == dir && listDoor[i].Status == STATUS_DOOR.IS_HIDEN)
            {
                Vector2 pos = listDoor[i].transform.GetChild(0).transform.position;
                Collider2D rs = Physics2D.OverlapCircle(pos, 0.05f);
                if (checkColider && rs != null)
                    continue;
                listDoor[i].Status = STATUS_DOOR.IS_BLOCKED;
                return listDoor[i];
            }
            continue;
        }
        return null;
    }

    public Room initRoom(Room roomTemplate, Transform room, int direction)
    {
        Vector2 pos = room.position;
        if (direction == DOOR_DIRECTION.LEFT) pos = getPostionLeftRoom(room);
        if (direction == DOOR_DIRECTION.RIGHT) pos = getPostionRightRoom(room);
        if (direction == DOOR_DIRECTION.TOP) pos = getPostionTopRoom(room);
        if (direction == DOOR_DIRECTION.DOWN) pos = getPostionDownRoom(room);


        Room r = Instantiate(roomTemplate, pos, Quaternion.identity);
        return r;
    }

    private Vector2 getPostionRightRoom(Transform originalRoom)
    {
        Vector2 pos = originalRoom.position;
        pos.x += boundX + 0.1f;
        return pos;
    }
    private Vector2 getPostionLeftRoom(Transform originalRoom)
    {
        Vector2 pos = originalRoom.position;
        pos.x += -(boundX + 0.1f);
        return pos;
    }
    private Vector2 getPostionTopRoom(Transform originalRoom)
    {
        Vector2 pos = originalRoom.position;
        pos.y += boundY + 0.1f;
        return pos;
    }
    private Vector2 getPostionDownRoom(Transform originalRoom)
    {
        Vector2 pos = originalRoom.position;
        pos.y += -(boundY + 0.1f);
        return pos;
    }

}


public enum RoomType
{
    NORMOAL,
    CHEST_ROOM,
    BOSS_ROOM
}