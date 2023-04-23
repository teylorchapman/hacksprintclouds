using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteSwap : MonoBehaviour
{

    [SerializeField] SpriteRenderer target;

    [SerializeField] Sprite[] sprites;
    // Start is called before the first frame update

    public void SetSprite(int i)
    {
        if (!target)
            return;
        target.sprite = sprites[i];
    }
}
