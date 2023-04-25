using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CameraControl : MonoBehaviour
{
    // Exposed Variables
    public GameObject playerObject;
    [SerializeField] float bounds = 0.8f;
    [SerializeField] float climbedDistance = 0.0f;
    [SerializeField] float climbSpeed = 0.1f;
    [SerializeField] bool needClimb = false;

    // Events
    public UnityEvent PlayerFallen;

    // Properties
    float range; // We can't use bounds directly, So we'll set Range in start
    float bottomY { get=> transform.position.y - Camera.main.orthographicSize;}
    float topY { get=>transform.position.y + range;}


    // Start is called before the first frame update
    void Start()
    {
        // Check for a player, since we're human
        if (!playerObject)
            Debug.LogError("No player object assigned to camera");
        // Used Error there since it will be more allerting than a log

        // Set the range
        range = bounds * Camera.main.orthographicSize; // bounds * half of the screen height
        //Setting range here is better than using a real property as it will be called only once
        // and accessing Camera.main can be expensive to do every frame
    }

    // Update is called once per visual frame
    void Update()
    {
        if (playerObject && playerObject.transform.position.y > topY)
            needClimb = true;

        if (playerObject && playerObject.transform.position.y < bottomY)
            PlayerFallen.Invoke();
    }

    // FixedUpdate is called once per physics frame
    void FixedUpdate()
    {
        if (needClimb)
            Climb();
    }

    // Draw debug gizmos when Selected
    void OnDrawGizmos()
    {
        float l_bottomY = transform.position.y - Camera.main.orthographicSize;
        float l_topY = transform.position.y + bounds * Camera.main.orthographicSize;
        Gizmos.color = Color.red;
        Gizmos.DrawLine(new Vector3(-100, l_bottomY), new Vector3(100, l_bottomY));
        Gizmos.color = Color.green;
        Gizmos.DrawLine(new Vector3(-100, l_topY), new Vector3(100, l_topY));
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(new Vector3(-100, transform.position.y), new Vector3(100, transform.position.y));
    }

    /**
     * Reset the camera to the starting position
     */
    public void Reset()
    {
        climbedDistance = 0;
        transform.position = Vector3.forward * -10;
    }

    /**
     * Climb the camera up to the player
     */
    void Climb()
    {
        // Increase Climed Distance
        climbedDistance += climbSpeed * Time.deltaTime;
        // Move the camera up, and keep  10 unit offset from the game plane
        transform.position = Vector3.up * climbedDistance + (Vector3.forward * -10);
        // Stop climbing if the player is in frame
        if (playerObject && playerObject.transform.position.y < transform.position.y)
        needClimb = false;
    }
}

