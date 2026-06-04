using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class FollowXRCameraTrigger : MonoBehaviour
{
    [SerializeField] private Transform xrCamera;

    private BoxCollider triggerBox;

    private void Awake()
    {
        triggerBox = GetComponent<BoxCollider>();
    }

    private void Update()
    {
        Vector3 localCameraPos = transform.InverseTransformPoint(xrCamera.position);

        triggerBox.center = new Vector3(
            localCameraPos.x,
            triggerBox.center.y,
            localCameraPos.z
        );
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log($"Entered {other.name}");
    }

    private void OnTriggerExit(Collider other)
    {
        Debug.Log($"Exited {other.name}");
    }
}