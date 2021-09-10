using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeeController : MonoBehaviour
{
    int state;
    readonly int setupState   = 0;
    readonly int attackState = 1;
    readonly int moveState = 2;

    Vector3 attackPos;
    Vector3 attackPosRoute;
    float setupTime = 2.0f;

    float time;

    public GameObject needlePrefab;

    float attackInterval = 0.6f;
    float attackCount;
    float needleDegreeInterval = 6.0f;
    float minNeedleDegree = -15.0f;

    float attackTime = 3.0f;

    float moveSpeed = 1.5f;
    float needleSpeed = 1.5f;

    GameObject generator;

    public AudioClip destroySE;

    public void SetLevel(int level)
    {
        this.attackInterval /= level;
        this.needleDegreeInterval /= level;
        this.moveSpeed *= level;
        this.needleSpeed *= level;
    }

    public void SetAtackPosition(Vector3 attackPos)
    {
        this.attackPos = attackPos;
        this.attackPosRoute = attackPos - transform.position;
    }

    // Start is called before the first frame update
    void Start()
    {
        this.state = this.setupState;
        this.time = 0;

        this.generator = GameObject.Find("BeeGenerator");
    }

    // Update is called once per frame
    void Update()
    {
        this.time += Time.deltaTime;
        if(this.state == this.setupState)
        {
            transform.Translate(this.attackPosRoute / this.setupTime * Time.deltaTime);
            if (this.time >= this.setupTime)
            {
                this.state = this.attackState;
                this.time = 0;
            }
        }
        else if(this.state == this.attackState)
        {
            if(this.time > this.attackCount * this.attackInterval)
            {
                float needleDegree = this.minNeedleDegree + (this.attackCount * this.needleDegreeInterval);
                GameObject needle = Instantiate(this.needlePrefab) as GameObject;
                needle.GetComponent<needleController>().SetNeedleSpeed(this.needleSpeed);
                needle.transform.position = transform.position;
                needle.transform.Rotate(0, 0, needleDegree);
                this.attackCount++;
            }
            if(this.time >= this.attackTime)
            {
                this.state = this.moveState;
                this.time = 0;
            }
        }
        else
        {
            transform.Translate(new Vector3(-1 * this.moveSpeed * Time.deltaTime, 0, 0));
            if (transform.position.x < -11)
            {
                this.generator.GetComponent<BeeGenerator>().BeeEscaped(this.attackPos);
                Destroy(gameObject);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag != "Needle")
        {
            AudioSource.PlayClipAtPoint(this.destroySE, transform.position);
            this.generator.GetComponent<BeeGenerator>().BeeKilled();
            Destroy(gameObject);
        }
    }
}
