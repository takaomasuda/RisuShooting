using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeeGenerator : MonoBehaviour
{
    public GameObject beePrefab;
    float interval = 3.0f;
    int BeeCount;

    float time = 0;

    List<Vector3> attackPosList;

    int level = 1;

    public void BeeEscaped(Vector3 attackPos)
    {
        attackPosList.Add(attackPos);
    }

    public void BeeKilled()
    {
        this.BeeCount--;
    }

    public int GetRemainBeeCount()
    {
        return this.BeeCount;
    }

    // Start is called before the first frame update
    void Start()
    {
        this.BeeCount = 0;
        attackPosList = new List<Vector3>();

        this.level = PlayerPrefs.GetInt("LEVEL");
        float distance = 3 / this.level;
        for (float x = 6; x <= 8; x += distance)
        {
            for(float y = -3; y <= 3; y += distance)
            {
                this.attackPosList.Add(new Vector3(x, y, 0));
                BeeCount++;
            }
        }
        this.interval /= level;
    }

    // Update is called once per frame
    void Update()
    {
        this.time += Time.deltaTime;
        if(time > this.interval)
        {
            this.time = 0;
            if (this.attackPosList.Count > 0)
            {
                GameObject bee = Instantiate(beePrefab) as GameObject;
                int index = Random.Range(0, this.attackPosList.Count - 1);
                Vector3 attackPos = this.attackPosList[index];
                bee.GetComponent<BeeController>().SetAtackPosition(attackPos);
                bee.GetComponent<BeeController>().SetLevel(this.level);
                this.attackPosList.RemoveAt(index);
            }
        }
    }
}
