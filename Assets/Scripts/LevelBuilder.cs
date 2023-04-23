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
    GameObject lastGenerated;
    [SerializeField] float levelWidth = 5.0f;

    List<GameObject> clouds = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        GetWidth();
        lastGenerated = lastPlaced;
    }

    // Update is called once per frame
    void Update()
    {   
        if (lastGenerated.transform.position.y < Camera.main.transform.position.y + Camera.main.orthographicSize + maxStep)
            clouds.Add(PlaceNewCloud());
        for(int i = clouds.Count - 1; i >= 0; i--)
        {
            GameObject c = clouds[i];
            if (c.transform.position.y < Camera.main.transform.position.y - Camera.main.orthographicSize - 10)
            {
                clouds.Remove(c);
                Destroy(c);
            }
        }
    }   

    void GetWidth()
    {
        levelWidth = Camera.main.orthographicSize * Camera.main.aspect;
    }

    GameObject GetNextCloud()
    {
        float chance = Random.value;

        float index = (SafeChance.Evaluate(transform.position.y / MaxHeight) + Random.value);

        if (chance > SafeChance.Evaluate(transform.position.y / MaxHeight))
            return SafePrefabs[Mathf.RoundToInt(index * SafePrefabs.Length ) % SafePrefabs.Length];
        else
            return DangerPrefabs[Mathf.RoundToInt(index * DangerPrefabs.Length ) % DangerPrefabs.Length];
    }

    GameObject PlaceNewCloud()
    {
        if (!lastGenerated)
            lastGenerated = lastPlaced;
        GameObject newCloud = Instantiate(GetNextCloud());
        Vector2 NewPos = (Vector2)lastGenerated.transform.position 
                + (Vector2.up * Random.Range(minStep, maxStep)) 
                + (Vector2.right * Random.Range(-1 *(maxShift + (lastGenerated.transform.localScale.x * 0.5f)), (maxShift + (lastGenerated.transform.localScale.x * 0.5f))));
        NewPos.x = Mathf.Clamp(NewPos.x, -1 * (levelWidth + (newCloud.transform.localScale.x *0.25f)), levelWidth + + (newCloud.transform.localScale.x *0.25f));
        newCloud.transform.position = NewPos;
        
        lastGenerated = newCloud;
        return newCloud;
    }

    public void Reset()
    {
        for (int i = clouds.Count - 1; i >= 0; i--)
            Destroy(clouds[i]); 
        clouds.Clear();
        lastGenerated = lastPlaced;
    }
}
