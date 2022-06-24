using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace RandomGenerationMap_V2
{
    public class RandomGenerationMap_V2 : MonoBehaviour
    {
        [Header("Config Level")]
        [SerializeField] private int numberOfRoom;
        [SerializeField] private Room[] templates;
        GeneratorMainPath genMainPath;
        GeneratorOtherPath genOtherPath;
        private ConfigLevel configLevel;
        public static RandomGenerationMap_V2 instance;

        private void Awake()
        {
            resetAwake();
        }
        private void resetAwake()
        {
            if (instance == null) instance = this;
            configLevel = new ConfigLevel(numberOfRoom, templates);
            genMainPath = gameObject.AddComponent<GeneratorMainPath_V1>();
            genOtherPath = gameObject.AddComponent<GeneratorOtherPath_V2>();
        }

        void Start()
        {
            genMainPath.generate(configLevel);
            genOtherPath.generate(configLevel);

            StartCoroutine(startgame(3f));
        }

        IEnumerator startgame(float timeStart) {
            yield return new WaitForSeconds(timeStart);
            configLevel.Rooms.ForEach(e =>
            {
                e.Status = STATUS_ROOM.IS_STARTED;
                e.gameObject.SetActive(false);
            });
     
            DemoMap.instance.begin(configLevel.Rooms);
            configLevel.Rooms[configLevel.Rooms.Count - 1].Type = RoomType.BOSS_ROOM;
            configLevel.Rooms.ForEach(e =>
            {
                BoxCollider2D box = e.GetComponent<BoxCollider2D>();
                Destroy(e.GetComponent<Rigidbody2D>());
                Destroy(box);

            });
            
        }
    }


    public interface GeneratorMainPath
    {
        public void generate(ConfigLevel config);
    }

    public interface GeneratorOtherPath
    {
        public void generate(ConfigLevel config);
    }

    public class ConfigLevel
    {
        [SerializeField] private int numberOfRoom;
        [SerializeField] private Room[] listRoomTemplate;
        [SerializeField] private List<Room> rooms = new List<Room>();
        public ConfigLevel(int numberOfRoom, Room[] listRoomTemplate)
        {
            this.numberOfRoom = numberOfRoom;
            this.listRoomTemplate = listRoomTemplate;
        }

        public int NumberOfRoom { get => numberOfRoom; set => numberOfRoom = value; }
        public Room[] ListRoomTemplate { get => listRoomTemplate; set => listRoomTemplate = value; }
        public List<Room> Rooms { get => rooms; set => rooms = value; }
    }

}