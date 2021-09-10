using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameDirector : MonoBehaviour
{
    GameObject levelText;
    GameObject seedText;
    GameObject beeCountText;
    GameObject player;
    GameObject beeGenerator;

    GameObject timeText;

    int level = 0;

    float playTime = 0;

    public void GameOver()
    {
        PlayerPrefs.SetFloat("TIME", this.playTime);
        PlayerPrefs.Save();
        SceneManager.LoadScene("GameOverScene");
    }

    // Start is called before the first frame update
    void Start()
    {
        this.levelText = GameObject.Find("Level");
        this.seedText = GameObject.Find("Seed");
        this.beeCountText = GameObject.Find("BeeCount");
        this.player = GameObject.Find("player");
        this.beeGenerator = GameObject.Find("BeeGenerator");

        this.level = PlayerPrefs.GetInt("LEVEL");

        this.playTime = PlayerPrefs.GetFloat("TIME");
        this.timeText = GameObject.Find("Time");
    }

    // Update is called once per frame
    void Update()
    {
        this.levelText.GetComponent<Text>().text = "Level " + this.level.ToString();

        int seed = this.player.GetComponent<PlayerController>().GetSeedCount();
        this.seedText.GetComponent<Text>().text = seed.ToString() + " Seed";

        int beeCount = this.beeGenerator.GetComponent<BeeGenerator>().GetRemainBeeCount();
        this.beeCountText.GetComponent<Text>().text = beeCount.ToString() + " Bee";

        this.playTime += Time.deltaTime;
        this.timeText.GetComponent<Text>().text = 
            "Time " + new System.TimeSpan(0, 0, 0, 0, (int)(this.playTime * 1000)).ToString(@"hh\:mm\:ss");

        if (beeCount <= 0)
        {
            PlayerPrefs.SetFloat("TIME", this.playTime);
            PlayerPrefs.Save();
            SceneManager.LoadScene("ClearScene");
        }
    }
}
