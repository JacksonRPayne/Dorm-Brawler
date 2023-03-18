using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class LightScript : MonoBehaviour
{
    Light2D dormLight;
    public float speed = 0.5f;
    public float saturation = 0.1f;

    // Start is called before the first frame update
    void Start()
    {
        dormLight = GetComponent<Light2D>();
    }

    // Update is called once per frame
    void Update()
    {
        dormLight.color = Color.HSVToRGB((speed * Time.time)%1, saturation, 1);
    }
}
