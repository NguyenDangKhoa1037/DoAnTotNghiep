using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    [SerializeField] GameObject GameOverPanel;
    [SerializeField] Player.Player player;

    float timeExists = 0.5f;
    float count = 0f;
    bool isRun = false;
    public static GameOver instance;
    private void Awake()
    {
        if (instance == null) instance = this;
        if(player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player.Player>();
        }
        GameOverPanel.SetActive(false);
    }
    public void gameOver()
    {
        isRun = true;
    }

    private void Update()
    {
        if (isRun) {
            count += Time.deltaTime;
            if (count >= timeExists)
            {
                count = 0;
                isRun = false;
                show();
            }
        }
    }

    private void show() {
        player.transform.GetChild(0).gameObject.SetActive(false);
        player.enabled = false;
        GameOverPanel.SetActive(true);
    }
    public void loadLevel() {
        SceneManager.LoadScene("playScene");
    }
}
