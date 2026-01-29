using UnityEngine;

public class LaserBeam : MonoBehaviour
{
    public float length = 30f;

    private LineRenderer lr;

    void Awake()
    {
        lr = GetComponent<LineRenderer>();
    }

    public void Fire()
    {
        lr.enabled = true;

        lr.SetPosition(0, transform.position);
        lr.SetPosition(1, transform.position + transform.forward * length);
    }

    public void Stop()
    {
        lr.enabled = false;
    }
}