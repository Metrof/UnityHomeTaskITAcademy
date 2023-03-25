using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeGenerator : MonoBehaviour
{
    Mesh mesh;

    private void Start()
    {
        float m_Distance = Vector3.Dot(Vector3.up, new Vector3(1, 0, 1));
        Debug.Log("m_Distance " + m_Distance);
    }
    public GameObject CreateCube(Vector3 size, Material material)
    {
        mesh = new Mesh();
        GameObject cube = Instantiate(new GameObject());
        MeshFilter filter =  cube.AddComponent<MeshFilter>();
        cube.AddComponent<MeshRenderer>().material = material;

        filter.mesh = mesh;
        mesh.vertices = GetVertices(size);
        mesh.triangles = GetTriangleMap();
        mesh.RecalculateBounds();

        return cube;
    }
    public GameObject CreateCube(Mesh newMesh, Material material)
    {
        GameObject cube = Instantiate(new GameObject());
        cube.AddComponent<MeshFilter>().mesh = newMesh;
        cube.AddComponent<MeshRenderer>().material = material;
        cube.AddComponent<Rigidbody>();
        return cube;
    }

    Vector3[] GetVertices(Vector3 cubeScale)
    {
        cubeScale /= 2;
        Vector3 vertice_0 = new(cubeScale.x, -cubeScale.y, cubeScale.z);
        Vector3 vertice_1 = new(-cubeScale.x, -cubeScale.y, cubeScale.z);
        Vector3 vertice_2 = new(cubeScale.x, cubeScale.y, cubeScale.z);
        Vector3 vertice_3 = new(-cubeScale.x, cubeScale.y, cubeScale.z);

        Vector3 vertice_4 = new(cubeScale.x, cubeScale.y, -cubeScale.z);
        Vector3 vertice_5 = new(-cubeScale.x, cubeScale.y, -cubeScale.z);
        Vector3 vertice_6 = new(cubeScale.x, -cubeScale.y, -cubeScale.z);
        Vector3 vertice_7 = new(-cubeScale.x, -cubeScale.y, -cubeScale.z);

        return new Vector3[]
        {
            vertice_0, vertice_1, vertice_2, vertice_3, vertice_4, vertice_5, vertice_6, vertice_7, 
            vertice_2, vertice_3, vertice_4, vertice_5, vertice_6, vertice_0, vertice_1, vertice_7,
            vertice_1, vertice_3, vertice_5, vertice_7, vertice_6, vertice_4, vertice_2, vertice_0,
        };
    }

    int[] GetTriangleMap()
    {
        return new int[]
        {
            0, 2, 3, 0, 3, 1,  
            8, 4, 5, 8, 5, 9,  
            10, 6, 7, 10, 7, 11,   
            12, 13, 14, 12, 14, 15,   
            16, 17, 18, 16, 18, 19,   
            20, 21, 22, 20, 22, 23,   
        };
    }
}
