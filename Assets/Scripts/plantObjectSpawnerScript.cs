using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class plantObjectSpawnerScript : MonoBehaviour
{

    public float rayLength;

    public GameObject debugObject;

    void Start()
    {
        Physics.queriesHitBackfaces = true;

        var direction = Random.onUnitSphere;
        RaycastHit hit;

        Ray myRay = new Ray(transform.position, direction);
        var endPoint = myRay.GetPoint(rayLength);

        if (Physics.Raycast(endPoint, -direction, out hit, rayLength)){
            Debug.Log("HIT");
        }

        GameObject debugCube = Instantiate(debugObject, hit.point, transform.rotation);
        debugCube.transform.LookAt(endPoint);
        debugCube.transform.parent = transform.parent;
    }

}
