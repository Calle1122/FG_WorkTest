using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetGeneratorScript : MonoBehaviour
{
    [SerializeField, Tooltip("A GameObject that will act as a parent for all spawned planets")]
    private GameObject galaxy;
    [SerializeField, Tooltip("A prefab of the object that should be spawned")]
    private GameObject planet;

    [SerializeField, Range(50f, 1000f), Tooltip("The value that is multiplied with the random point in a unit sphere, thus changing the spawn range")]
    private float rangeForSpawning = 250f;

    [SerializeField, Range(1, 100), Tooltip("This int defines the number of planets spawned in the world")]
    private int numberOfPlanets = 1;

    private Vector3 spawnPoint;

    private float offsetAmount = 0.3f;

    private void Start() {
        for(int i = 0; i < numberOfPlanets; i++){
            spawnPoint = Random.insideUnitSphere * rangeForSpawning;
            transform.position = spawnPoint;

            var generatedPlanet = Instantiate(planet, spawnPoint, Quaternion.identity, galaxy.transform);
            var planetMesh = generatedPlanet.GetComponent<MeshFilter>().mesh;
            var planetVertices = planetMesh.vertices;
            var planetNormals = planetMesh.normals;

            float x1 = Random.Range(0f, 9999999f);
            float y1 = Random.Range(0f, 9999999f);
            float z1 = Random.Range(0f, 9999999f);

            for(int x = 0; x < planetVertices.Length; ++x){
                offsetAmount = PerlinNoise3D(planetVertices[x].x + x1, planetVertices[x].y + y1, planetVertices[x].z + z1);

                planetVertices[x] += planetNormals[x] * offsetAmount;
            }

            planetMesh.vertices = planetVertices;

            planetMesh.RecalculateBounds();

            planetMesh.UploadMeshData(true);



            MeshCollider meshc = generatedPlanet.AddComponent(typeof(MeshCollider)) as MeshCollider;
            meshc.sharedMesh = planetMesh;
        }
    }

    public static float PerlinNoise3D(float x, float y, float z)
    {
        float xy = Mathf.PerlinNoise(x, y);
        float xz = Mathf.PerlinNoise(x, z);
        float yz = Mathf.PerlinNoise(y, z);
        float yx = Mathf.PerlinNoise(y, x);
        float zx = Mathf.PerlinNoise(z, x);
        float zy = Mathf.PerlinNoise(z, y);
    
        return ((xy + xz + yz + yx + zx + zy) / 6) * 3.5f;
    }

}
