using UnityEngine;

public class Planet : MonoBehaviour
{
    [HideInInspector] public EllipticalOrbit orbit;
    [HideInInspector] public LineRenderer lineRenderer;
    [HideInInspector] public Camera cam;
    [HideInInspector] public Transform body;

    public float zRotation;
    public float rotationPeriodInHours;

    private void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
        orbit = GetComponent<EllipticalOrbit>();
        cam = GetComponentInChildren<Camera>();
        cam.enabled = false;
        body = GetComponentInChildren<MeshRenderer>().transform;
    }

    private void Update()
    {
        // Time Scale | 0.1s = 1 day
        if (rotationPeriodInHours != 0)
        {
            float rotation = 360f / (rotationPeriodInHours / 24) * 0.1f;

            Vector3 v = body.localEulerAngles;
            v.z = zRotation;
            v.y += rotation;

            body.localEulerAngles = v;
        }
        
    }
}
