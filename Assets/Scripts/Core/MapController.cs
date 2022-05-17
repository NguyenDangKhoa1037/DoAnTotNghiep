using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapController : MonoBehaviour
{
    public static MapController instances;

    [SerializeField] private RandomEnemies randomEnemies;


    public RandomEnemies RandomEnemies { get => randomEnemies; set => randomEnemies = value; }
    private Room currentRoom;
    private void Awake()
    {
        if(instances == null)
        instances = this;
        setup();
    }


    void setup() {
        if (RandomEnemies == null)
            RandomEnemies = gameObject.AddComponent<RandomEnemies>();
         
    }

    public void beginRoom(Room room) {
        currentRoom = room;
        RandomEnemies.genEnemies(currentRoom);
    }
}
