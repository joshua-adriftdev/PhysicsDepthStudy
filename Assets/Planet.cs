using UnityEngine;

public class Planet : MonoBehaviour
{
    [HideInInspector] public EllipticalOrbit orbit;
    [HideInInspector] public LineRenderer lineRenderer;
    [HideInInspector] public Camera cam;
    public Transform body;

    private float initialScale = 1f;
    private float knownScale = 1f;

    private void Awake()
    {
        lineRenderer = GetComponentInChildren<LineRenderer>();
        orbit = GetComponent<EllipticalOrbit>();
        cam = GetComponentInChildren<Camera>();
        cam.enabled = false;
        if (body == null)
            body = GetComponentInChildren<MeshRenderer>().transform;
        initialScale = body.localScale.x;
    }

    private void Update()
    {
        if (StateManager.instance.planetScale != knownScale)
        {
            knownScale = StateManager.instance.planetScale;
            float newScale = initialScale * knownScale;
            body.localScale = new Vector3(newScale, newScale, newScale);
        }
    }
}
