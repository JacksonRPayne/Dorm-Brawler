using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishMovement : MonoBehaviour
{
    private bool copied = false;

    public float speed = .2f;
    public float leftLimit = -3f;
    public float rightLimit = -7f;


    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.left * speed*Time.deltaTime);

        if (transform.localPosition.x <= rightLimit) Destroy(gameObject);

        if(transform.localPosition.x <= leftLimit && !copied)
        {
            Vector3 offset = new Vector3(GetComponent<Renderer>().bounds.size.x, 0, 0);
            Instantiate(gameObject, transform.position + offset, transform.rotation, transform.parent);
            copied = true;
        }
    }
}
