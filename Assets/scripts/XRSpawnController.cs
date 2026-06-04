using UnityEngine;
using Unity.XR.CoreUtils;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.XR;

[RequireComponent(typeof(XROrigin))]
public class XRSpawnController : MonoBehaviour
{
    [Header("Spawn Settings")]
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private float initDelay = 0.2f;

    private XROrigin _xrOrigin;

    void Awake() => _xrOrigin = GetComponent<XROrigin>();

    void Start() => StartCoroutine(SpawnAfterInit());

    private IEnumerator SpawnAfterInit()
    {
        yield return new WaitForSecondsRealtime(initDelay);
        AlignSceneToHeadset();
    }

    private void AlignSceneToHeadset()
    {
        Transform cam = _xrOrigin.Camera.transform;
        Transform origin = _xrOrigin.transform;

        // Step 1 — Rotate XR Origin so the player's current physical
        // forward becomes the scene's designed forward (spawnPoint forward)
        float headsetYaw  = cam.eulerAngles.y;
        float desiredYaw  = spawnPoint.eulerAngles.y;
        float yawDelta    = desiredYaw - headsetYaw;

        origin.RotateAround(cam.position, Vector3.up, yawDelta);

        // Step 2 — Move XR Origin so the camera lands at the spawn position
        Vector3 camFloor    = new Vector3(cam.position.x, 0f, cam.position.z);
        Vector3 targetFloor = new Vector3(spawnPoint.position.x, 0f, spawnPoint.position.z);
        origin.position    += targetFloor - camFloor;

        // Step 3 — Set floor Y
        origin.position = new Vector3(origin.position.x, spawnPoint.position.y, origin.position.z);
    }
}