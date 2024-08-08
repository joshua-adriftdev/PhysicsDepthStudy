using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour
{
    public float zRotation;
    public float rotationPeriodInHours;
    private Planet planet;
    [SerializeField] private Transform body;

    // Start is called before the first frame update
    void Start()
    {
        if (body == null)
        {
            if (GetComponent<Planet>() != null)
            {
                planet = GetComponent<Planet>();
                body = planet.body.transform;
            }
            else
            {
                body = transform;
            }
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        // Time Scale | 0.1s = 1 day
        if (rotationPeriodInHours != 0)
        {
            float rotation = (360f / (rotationPeriodInHours / 24) * 0.1f) * Time.deltaTime;
            
            Vector3 v = body.eulerAngles;
            v.z = zRotation;
            v.y += rotation;

            body.eulerAngles = v;
        }

    }
}
