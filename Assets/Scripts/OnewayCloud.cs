using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnewayCloud : MonoBehaviour
{
    GameObject playerObject;
    [SerializeField] Collider col;
    // Start is called before the first frame update
    void Start()
    {
        if (!col)
            col = GetComponent<Collider>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!playerObject)
            getPlayer();

        col.enabled = (playerObject && playerObject.transform.position.y - 1 > transform.position.y ) ;

    }       

    GameObject getPlayer()
    {
        if (playerObject)
            return playerObject;
        
        playerObject = Camera.main.GetComponent<CameraControl>().playerObject;
        return playerObject;
    }
}
