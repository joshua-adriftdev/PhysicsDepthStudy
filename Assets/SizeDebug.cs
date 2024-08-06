using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SizeDebug : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(GetComponent<MeshRenderer>().bounds);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
