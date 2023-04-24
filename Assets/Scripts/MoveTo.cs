using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class MoveTo : MonoBehaviour
{


    public Vector3 goal;
    public float time = 1;
    public float distance = 0.1f;
    public UnityEvent arrived;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 currentPosition = transform.position;
        transform.position = Vector3.Lerp(currentPosition, goal, Time.deltaTime / time);
        if (Vector3.Distance(transform.position, goal) < distance)
            arrived.Invoke();
    }
    public void SetGoal(Vector3 NewGoal)
    {
        goal = NewGoal;
    }
}

