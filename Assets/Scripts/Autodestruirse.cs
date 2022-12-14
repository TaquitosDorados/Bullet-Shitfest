using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Autodestruirse : MonoBehaviour
{
    private float Timer;
    // Start is called before the first frame update
    void Start()
    {
        Timer = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if(Time.time - Timer > 3)
        {
            Destroy(gameObject);
        }
    }
}
