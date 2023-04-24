using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wiggler : MonoBehaviour
{   
    public Vector2 bounds;
    public float speed = 1;
    Vector2 startPos;
    Vector2 currentPos;
    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.localPosition;
        currentPos = startPos;
    }

    // Update is called once per frame
    void Update()
    {
        currentPos = new Vector2( Mathf.Sin(Time.time * speed) * bounds.x, Mathf.Cos(Time.time * speed) * bounds.y) + startPos;

        //currentPos.x = Mathf.Clamp(currentPos.x, startPos.x - bounds.x, startPos.x + bounds.x);
        //currentPos.y = Mathf.Clamp(currentPos.y, startPos.y - bounds.y, startPos.y + bounds.y);
        transform.localPosition = currentPos;    
    }
}
