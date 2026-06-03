using UnityEngine;

public class FollowXRCamera : MonoBehaviour
{
    [SerializeField] private CharacterController characterController;
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
            characterController.Move(cameraDelta);
            xrRig.position -= cameraDelta;
        }

        Vector3 camLocal = xrRig.InverseTransformPoint(xrCamera.position);
        characterController.center = new Vector3(camLocal.x, characterController.center.y, camLocal.z);
    }
}