using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundController : MonoBehaviour
{
    float scrollSpeed = -3.0f;
    float spriteSize = 20.0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(this.scrollSpeed * Time.deltaTime, 0, 0);
        Vector3 localPos = transform.position;
        if (localPos.x < -this.spriteSize)
        {
            transform.position = new Vector3(this.spriteSize, localPos.y, localPos.z);
        }
    }
}
