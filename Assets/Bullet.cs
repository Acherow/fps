using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    float lifetime = 5;

    private void Update()
    {
        lifetime -= Time.deltaTime;

        if(lifetime < 0)
            Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        Destroy(gameObject);
    }
}
