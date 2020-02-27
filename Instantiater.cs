using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Instantiater : MonoBehaviour
{
    public Transform blaster;
    float x, y, z;
    Vector3 pos;
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < 100; i++)
        {
            x = Random.Range(40 + (i * 50), 70 + (i * 50));
            y = -1.55f;
            z = 0.0f;
            pos = new Vector3(-x, -y, z);
            Instantiate(blaster, pos, Quaternion.identity);
        }
    }
}
