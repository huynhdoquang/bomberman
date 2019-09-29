using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blast : MonoBehaviour
{
    [SerializeField] private float timelife;
    float timeCount;
    bool onAlive;

    public void Init()
    {
        onAlive = true;
        timeCount = 0;
    }

    void Update()
    {
        if (onAlive)
        {
            timeCount += Time.deltaTime;
            if (timeCount >= timelife)
                Explosive();
        }
    }

    void Explosive()
    {
        Destroy(gameObject);
    }
}
