using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    int state;
    readonly int idleState     = 0;
    readonly int jumpUpState   = 1;
    readonly int jumpDownState = 2;

    Rigidbody2D body;
    float maxSpeed = 2.0f;
    float jumpForce = 20.0f;

    int seedCount;
    public GameObject seedPrefab;
    float seedSpeed = 3.0f;

    public AudioClip seedShotSE;
    public AudioClip seedGetSE;

    GameObject gameDirector;

    // Start is called before the first frame update
    void Start()
    {
        this.body = GetComponent<Rigidbody2D>();
        this.state = this.idleState;
        this.seedCount = 1;

        this.gameDirector = GameObject.Find("GameDirector");
    }

    void seedShot()
    {
        GameObject seed = Instantiate(this.seedPrefab) as GameObject;
        Vector3 pos = transform.position;
        seed.transform.position = new Vector3(pos.x + 0.6f, pos.y - 0.05f, pos.z);
        seed.GetComponent<Rigidbody2D>().velocity = new Vector3(this.seedSpeed, 0, 0);
        seed.GetComponent<SeedController>().Shot();
        this.seedCount--;
    }

    public int GetSeedCount()
    {
        return this.seedCount;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.Space))
        {
            if(this.body.velocity.y < this.maxSpeed)
            {
                this.body.AddForce(new Vector2(0, this.jumpForce));
            }
        }
        if(Input.GetKeyDown(KeyCode.RightArrow))
        {
            if (this.seedCount > 0)
            {
                seedShot();
                GetComponent<AudioSource>().PlayOneShot(this.seedShotSE);
            }
        }

        if (this.body.velocity.y == 0)
        {
            if (this.state != this.idleState)
            {
                this.state = this.idleState;
                GetComponent<ParticleSystem>().Stop();
            }
        }
        if (this.body.velocity.y > 0)
        {
            if (this.state != this.jumpUpState)
            {
                this.state = this.jumpUpState;
                GetComponent<ParticleSystem>().Play();
            }
        }
        else if(this.body.velocity.y < 0)
        {
            this.state = this.jumpDownState;
        }
        else
        {
            this.state = this.idleState;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Seed")
        {
            this.seedCount++;
            Destroy(collision.gameObject);
            GetComponent<AudioSource>().PlayOneShot(this.seedGetSE);
        }
        else if(collision.gameObject.tag == "Needle")
        {
            this.gameDirector.GetComponent<GameDirector>().GameOver();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        this.gameDirector.GetComponent<GameDirector>().GameOver();
    }
}
