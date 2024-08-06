using UnityEngine;

public class Planet : MonoBehaviour
{
    [HideInInspector] public EllipticalOrbit orbit;
    [HideInInspector] public LineRenderer lineRenderer;
    [HideInInspector] public Camera cam;

    public float dayInHours;
    public Vector3 rotationAxis;

    private void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
        orbit = GetComponent<EllipticalOrbit>();
        //cam = GetComponentInChildren<Camera>();
        //cam.enabled = false;
    }

    public void Update()
    {
        orbit.body.rotation = Quaternion.Euler(rotationAxis);

        /*Debug.Log(rotationAxis);

        Quaternion quaternion = Quaternion.Euler(0, 0, -23.5f);
        Debug.Log(quaternion);

        cam.transform.localEulerAngles = new Vector3(0, 180, rotationAxis.z);*/
    }
}
