using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomEnemies : MonoBehaviour
{
    [SerializeField] private Enemy.Enemy[] prefabs;
    [SerializeField] private Demon demon;
    [SerializeField] private BlueFire blueFire;
    [SerializeField] private GameObject genEnemyEffect;
    [SerializeField] private float timeWarningGenEnemies;
    [SerializeField] private GameObject FIX;
    [SerializeField] private GameObject MONSTER;
    private GameObject[] effects;
    private Room currentRoom;
    private float timeCountWarning;
    private bool isCounting = false;
    private List<Enemy.Enemy> roomEnemies;
    private List<Enemy.Enemy> bosses = new List<Enemy.Enemy>();
    public Enemy.Enemy[] Enemies { get => prefabs; set => prefabs = value; }

    private void Awake()
    {
        timeCountWarning = timeWarningGenEnemies;
        effects = new GameObject[30];
        for (int i = 0; i < 30; i++)
        {
            effects[i] = Instantiate(genEnemyEffect, Vector2.zero, Quaternion.identity);
            effects[i].name = "Effect - Gen Enemies - " + i;
            effects[i].SetActive(false);
            effects[i].transform.SetParent(FIX.transform);
        }

        bosses.Add(blueFire);
        bosses.Add(demon);
    }


    GameObject  pickEffect() {
        for (int i = 0; i < effects.Length; i++) {
            if (effects[i].activeSelf == false) {
                return effects[i];
            }
        }
        throw new System.Exception("No effects active");
    }
    public void genEnemies(Room currentRoom)
    {
        if (currentRoom.Type == RoomType.NORMOAL)
        {
            if (currentRoom.Enemies.Count <= 0) return;
            this.currentRoom = currentRoom;
            List<Enemy.Enemy> enemies = new List<Enemy.Enemy>();

            for (int i = 0; i < currentRoom.Enemies.Count; i++)
            {
                Vector2 pos = currentRoom.transform.position;
                // Random trong khoang -5,-5 den 5,5
                pos.x += Random.Range(-5, 5f);
                pos.y += Random.Range(-5, 5f);
                var enemy = Instantiate(currentRoom.Enemies[i], pos, Quaternion.identity);
                enemy.gameObject.SetActive(false);
                enemy.transform.SetParent(MONSTER.transform);
                enemies.Add(enemy);
                enemy.OnInited(currentRoom);
                GameObject effect = pickEffect();
                effect.transform.position = pos;
                effect.SetActive(true);
            }
            roomEnemies = enemies;
            currentRoom.Enemies = enemies;
            isCounting = true;
        }
        else {
            int index = GameInfo.instance.Level - 1;
            Enemy.Enemy boss=  Instantiate(bosses[index], currentRoom.transform.position, Quaternion.identity);
            boss.OnInited(currentRoom);
            currentRoom.Enemies.Add(boss);
        }
    }

    private void Update()
    {
        if (!isCounting) return;
        timeCountWarning -= Time.deltaTime;
        if (timeCountWarning <= 0)
        {
            timeCountWarning = timeWarningGenEnemies;
            activeAllEnemies();
            isCounting = false;
        }
    }
    

    void activeAllEnemies() {
        for (int i = 0; i < effects.Length; i++)
            effects[i].gameObject.SetActive(false);

        roomEnemies.ForEach(e => e.gameObject.SetActive(true));
    }
    //private bool check()
    //{
    //}
}
