using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectDestroyer : MonoBehaviour
{

    public float timer;

    void Start()
    {
        Destroy(gameObject, timer);
    }

    void Update()
    {
        
    }
}


//DragonCubeGames