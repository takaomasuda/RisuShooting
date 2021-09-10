using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class needleController : MonoBehaviour
{
    float needleSpeed = 1.5f;

    public void SetNeedleSpeed(float speed)
    {
        this.needleSpeed = speed;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(transform.right * this.needleSpeed * Time.deltaTime * -1);

        Vector3 localPos = transform.position;
        if (Mathf.Abs(localPos.x) > 11 || Mathf.Abs(localPos.y) > 5.5)
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag != "Bee" &&
            collision.gameObject.tag != "Needle")
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag != "Bee" &&
            collision.gameObject.tag != "Needle")
        {
            Destroy(gameObject);
        }
    }
}
