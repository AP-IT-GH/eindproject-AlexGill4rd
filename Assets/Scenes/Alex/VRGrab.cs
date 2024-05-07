using UnityEngine;

public class VRGrab : MonoBehaviour
{
    private bool isGrabbed = false;
    private Transform controllerTransform;
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Controller"))
        {
            controllerTransform = other.transform;
        }
    }

    void Update()
    {
        if (controllerTransform != null)
        {
            if (Input.GetButtonDown("GrabButton")) // Adjust input button according to your VR controller mapping
            {
                isGrabbed = true;
                rb.useGravity = false;
                rb.constraints = RigidbodyConstraints.FreezeAll;
            }
            if (isGrabbed)
            {
                transform.position = controllerTransform.position;
                transform.rotation = controllerTransform.rotation;
            }
            if (Input.GetButtonUp("GrabButton"))
            {
                isGrabbed = false;
                rb.useGravity = true;
                rb.constraints = RigidbodyConstraints.None;
            }
        }
    }
}
