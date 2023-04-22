using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorSwap : MonoBehaviour
{

    [SerializeField] SpriteRenderer[] target;

    [SerializeField] Color[] colors;
    // Start is called before the first frame update

    public void swapColor(int i)
    {
        foreach( SpriteRenderer t in target)
        t.color = colors[i];
    }
}
