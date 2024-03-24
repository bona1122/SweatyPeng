using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombAim : MonoBehaviour
{
    public float howLong;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        DestroyMethod();
    }

    void DestroyMethod()
    {
        Destroy(gameObject, howLong);
    }
}
