using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorSwap : MonoBehaviour
{

    [SerializeField] ColorRenderer target;

    [SerializeField] Color[] colors;
    // Start is called before the first frame update

    public void swapColor(int i)
    {
        if (!target)
            return;
        target.color = colors[i];
    }
}
