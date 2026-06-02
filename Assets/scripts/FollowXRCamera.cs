using UnityEngine;

public class FollowXRCamera : MonoBehaviour
{
    [SerializeField] private CapsuleCollider capsuleCollider;
    [SerializeField] private Transform xrCamera;
    [SerializeField] private Transform xrRig;

    private Vector3 previousCameraPosition;

    void Start()
    {
        previousCameraPosition = xrCamera.position;
    }

    void Update()
    {
        SyncColliderToCamera();
    }

    private void SyncColliderToCamera()
    {
        Vector3 cameraDelta = xrCamera.position - previousCameraPosition;
        cameraDelta.y = 0;

        previousCameraPosition = xrCamera.position;

        if (cameraDelta.sqrMagnitude > 0.0001f)
        {
            capsuleCollider.transform.position += cameraDelta;
            xrRig.position -= cameraDelta;
        }

        Vector3 camLocal = capsuleCollider.transform.InverseTransformPoint(xrCamera.position);
        capsuleCollider.center = new Vector3(camLocal.x, capsuleCollider.center.y, camLocal.z);
    }
}