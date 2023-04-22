using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelBuilder : MonoBehaviour
{
    public GameObject[] SafePrefabs;
    public GameObject[] DangerPrefabs;

    public GameObject[] backGrounds;

    public AnimationCurve SafeChance;
    public float MaxHeight = 100f;

    public float minStep = 2f, maxStep = 8f;

    public float maxShift = 12f;

    [SerializeField] GameObject lastPlaced;
    [SerializeField] float levelWidth = 5.0f;

    List<GameObject> clouds = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        GetWidth();   
    }

    // Update is called once per frame
    void Update()
    {   
        if (lastPlaced.transform.position.y < Camera.main.transform.position.y + Camera.main.orthographicSize + maxStep)
            clouds.Add(PlaceNewCloud());
        foreach( GameObject c in clouds)
        {
            if (c.transform.position.y < Camera.main.transform.position.y - Camera.main.orthographicSize - 10)
            {
                clouds.Remove(c);
                Destroy(c);
            }
        }
    }   

    void GetWidth(){

        levelWidth = Camera.main.orthographicSize * Camera.main.aspect;

    }

    GameObject GetNextCloud()
    {
        float chance = Random.value;

        int index = Mathf.RoundToInt((SafeChance.Evaluate(transform.position.y / MaxHeight) + Random.value) * SafePrefabs.Length) % SafePrefabs.Length;

        if (chance > SafeChance.Evaluate(transform.position.y / MaxHeight))
            return SafePrefabs[index];
        else
            return DangerPrefabs[index];
    }

    GameObject PlaceNewCloud()
    {
        GameObject newCloud = Instantiate(GetNextCloud());
        Vector2 NewPos = (Vector2)lastPlaced.transform.position 
                + (Vector2.up * Random.Range(minStep, maxStep)) 
                + (Vector2.right * Random.Range(-1 *(maxShift + (lastPlaced.transform.localScale.x * 0.5f)), (maxShift + (lastPlaced.transform.localScale.x * 0.5f))));
        NewPos.x = Mathf.Clamp(NewPos.x, -1 * (levelWidth + (newCloud.transform.localScale.x *0.25f)), levelWidth + + (newCloud.transform.localScale.x *0.25f));
        newCloud.transform.position = NewPos;
        
        lastPlaced = newCloud;
        return newCloud;
    }
}
