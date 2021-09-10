using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeedController : MonoBehaviour
{
    public void Shot()
    {
        GetComponent<Animator>().SetInteger("SeedState", 1);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 localPos = transform.position;
        if (localPos.x > 11 || localPos.x < -11)
        {
            Destroy(gameObject);
        }
    }
}
