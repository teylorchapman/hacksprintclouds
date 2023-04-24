using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleWiggler : MonoBehaviour
{
    Vector3 startScale;
    public Vector2 bounds;
    public float speed = 1;
    Vector2 currMod;
    // Start is called before the first frame update
    void Start()
    {
        startScale = transform.localScale;
    }

    // Update is called once per frame
    void Update()
    {
        currMod = new Vector2(
                        Mathf.Sin(Time.time * speed) * bounds.x,
                        Mathf.Cos(Time.time * speed) * bounds.y);
        transform.localScale = startScale + (Vector3)currMod;
    }
}
