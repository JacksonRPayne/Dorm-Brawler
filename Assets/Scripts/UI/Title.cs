using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Title : MonoBehaviour
{
    public float speed = 1f;
    public float amplitude = 0.2f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 posUpdate = new Vector3(0, amplitude * Mathf.Sin(Time.time * speed), 0);
        transform.position += posUpdate;
    }
}
