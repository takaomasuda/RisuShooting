using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ClearDirector : MonoBehaviour
{
    GameObject clearText;
    GameObject nextText;
    int level;
    int maxLevel = 3;
    // Start is called before the first frame update
    void Start()
    {
        clearText = GameObject.Find("Clear");
        nextText = GameObject.Find("Next");
        this.level = PlayerPrefs.GetInt("LEVEL");



        if (this.level < this.maxLevel)
        {
            this.clearText.GetComponent<Text>().text = "Clear Level " + this.level.ToString();
        }
        else
        {
            int millsec = (int)(PlayerPrefs.GetFloat("TIME") * 1000);
            System.TimeSpan time = new System.TimeSpan(0, 0, 0, 0, millsec);
            naichilab.RankingLoader.Instance.SendScoreAndShowRanking(time);

            this.clearText.GetComponent<Text>().text = "Congratulation !";
            this.nextText.GetComponent<Text>().text = "Press Enter key to return Title.";

        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            if (this.level < this.maxLevel)
            {
                this.level++;
                PlayerPrefs.SetInt("LEVEL", this.level);
                PlayerPrefs.Save();
                SceneManager.LoadScene("GameScene");
            }
            else
            {
                this.level = 1;
                PlayerPrefs.SetInt("LEVEL", this.level);
                PlayerPrefs.Save();
                SceneManager.LoadScene("TitleScene");
            }
        }
    }
}
