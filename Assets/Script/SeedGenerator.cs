using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeedGenerator : MonoBehaviour
{
    public GameObject seedPrefab;

    float time;
    float interval = 3.0f;

    GameObject ground;

    // Start is called before the first frame update
    void Start()
    {
        this.time = 0;
        this.ground = GameObject.Find("ground1");
    }

    // Update is called once per frame
    void Update()
    {
        this.time += Time.deltaTime;
        if(this.time > this.interval)
        {
            this.time = 0;
            GameObject seed = Instantiate(this.seedPrefab);
            seed.transform.position = new Vector3(11, -4.25f, 0);
            seed.GetComponent<Rigidbody2D>().velocity = new Vector3(-3.0f, 0, 0);
        }
    }
}
