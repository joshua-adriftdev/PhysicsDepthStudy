using UnityEngine;

public class Planet : MonoBehaviour
{
    [HideInInspector] public EllipticalOrbit orbit;
    [HideInInspector] public LineRenderer lineRenderer;
    [HideInInspector] public Camera cam;

    private void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
        orbit = GetComponent<EllipticalOrbit>();
        cam = GetComponentInChildren<Camera>();
        cam.enabled = false;
    }
}
