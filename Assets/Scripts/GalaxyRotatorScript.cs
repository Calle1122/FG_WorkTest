using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GalaxyRotatorScript : MonoBehaviour
{
    [SerializeField, Range(0.0f, 0.01f), Tooltip("The float value used to rotate the galaxy on the Y-axis, every frame")]
    private float rotationForceY = 0.005f;
    private Vector3 appliedRotation;

    private void Awake() {
        appliedRotation = new Vector3(0.0f, rotationForceY, 0.0f);
    }

    void Update()
    {
        transform.Rotate(appliedRotation, Space.Self);
    }
}
