using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    [SerializeField] BoxCollider _mainBlock;
    [SerializeField] Material _capMaterial;

    private float _currentXLarge;
    private float _currentZLarge;
    private float _disToMainBlock;
    private Vector3 _startPoint = Vector3.right * 2;
    private Vector3 _normal = Vector3.up;
    private GameObject _currentCube;
    private Mesh _previosMesh;

    private CubeGenerator _cubeGenerator;
    MeshMaker _fallingSide = new MeshMaker();
    MeshMaker _standingSide = new MeshMaker();
    Triangle fallingTriangleCache = new Triangle(new Vector3[3], new Vector2[3], new Vector3[3], new Vector4[3]);
    Triangle standingTriangleCache = new Triangle(new Vector3[3], new Vector2[3], new Vector3[3], new Vector4[3]);
    Triangle newTriangleCache = new Triangle(new Vector3[3], new Vector2[3], new Vector3[3], new Vector4[3]);
    List<Vector3> newVerticesCache = new List<Vector3>();

    Triangle _triangleCache = new(new Vector3[3], new Vector2[3], new Vector3[3], new Vector4[3]);

    Controller _inputActions;

    private void Awake()
    {
        _inputActions = new Controller();
    }
    private void Start()
    {
        _cubeGenerator = GetComponent<CubeGenerator>();
        UpdateParram(_mainBlock);
    }
    private void OnEnable()
    {
        _inputActions.Enable();
        _inputActions.Game.PutBlock.performed += Slice;
    }
    private void OnDisable()
    {
        _inputActions.Game.PutBlock.performed -= Slice;
        _inputActions.Disable();
    }

    private void Slice(InputAction.CallbackContext obj)
    {
        GameObject[] sliced = BlockCut(_currentCube);

        foreach (var i in sliced)
        {
            i.name = string.Format("{0} ({1})", gameObject.name, i.name);

            if (i.GetComponent<Rigidbody>() == null)
            {
                i.AddComponent<Rigidbody>();
            }
        }
    }

    private void UpdateParram(BoxCollider previosBox)
    {
        _currentXLarge = previosBox.size.x;
        _currentZLarge = previosBox.size.z;
        _startPoint.y += previosBox.size.y;
        (_startPoint.x, _startPoint.z) = (_startPoint.z, _startPoint.x);
        SpawnBlock();
    }
    private void SpawnBlock()
    {
        if (_previosMesh != null)
        {
            _currentCube = _cubeGenerator.CreateCube(_previosMesh, _capMaterial);
        }
        else
        {
            _currentCube = _cubeGenerator.CreateCube(new Vector3(_currentXLarge, 1, _currentZLarge), _capMaterial);
        }
        _currentCube.AddComponent<BoxCollider>();
        _currentCube.AddComponent<CubeMovement>().SetMoveDirection(new Vector3(-_startPoint.x, 0, -_startPoint.z));
        _currentCube.transform.position = _startPoint;
    }
    public GameObject[] BlockCut(GameObject victim)
    {
        Vector3 inversedNormal = victim.transform.InverseTransformDirection(-_mainBlock.transform.up);
        Vector3 inversedAnchor = victim.transform.InverseTransformPoint(_mainBlock.transform.position);
        inversedNormal = Vector3.Normalize(inversedNormal);

        _disToMainBlock = -Vector3.Dot(inversedNormal, inversedAnchor);
        print(_disToMainBlock);
        List<GameObject> sliced = new List<GameObject>();

        Mesh originalMesh = victim.GetComponent<MeshFilter>().mesh;

        Vector3[] vertices = originalMesh.vertices;
        Vector3[] normals = originalMesh.normals;
        Vector2[] uvs = originalMesh.uv;
        Vector4[] tangents = originalMesh.tangents;

        for (int i = 0; i < normals.Length; i++)
        {
            Debug.Log(normals[i]);
        }

        for (int subMeshIndex = 0; subMeshIndex < originalMesh.subMeshCount; ++subMeshIndex)
        {
            int[] indices = originalMesh.GetTriangles(subMeshIndex);

            for (int i = 0; i < indices.Length; i += 3)
            {
                Vector3 p1 = vertices[indices[i + 0]];
                Vector3 p2 = vertices[indices[i + 1]];
                Vector3 p3 = vertices[indices[i + 2]];

                //Vector3 n1 = normals[indices[i + 0]];
                //Vector3 n2 = normals[indices[i + 1]];
                //Vector3 n3 = normals[indices[i + 2]];

                //Vector2 uv1 = uvs[indices[i + 0]];
                //Vector2 uv2 = uvs[indices[i + 1]];
                //Vector2 uv3 = uvs[indices[i + 2]];

                //Vector4 tangent1 = tangents[indices[i + 0]];
                //Vector4 tangent2 = tangents[indices[i + 1]];
                //Vector4 tangent3 = tangents[indices[i + 2]];

                _triangleCache.vertices[0] = p1;
                _triangleCache.vertices[1] = p2;
                _triangleCache.vertices[2] = p3;

                //_triangleCache.normals[0] = n1;
                //_triangleCache.normals[1] = n2;
                //_triangleCache.normals[2] = n3;

                //_triangleCache.uvs[0] = uv1;
                //_triangleCache.uvs[1] = uv2;
                //_triangleCache.uvs[2] = uv3;

                //_triangleCache.tangents[0] = tangent1;
                //_triangleCache.tangents[1] = tangent2;
                //_triangleCache.tangents[2] = tangent3;

                bool isP1LeftHandSide = IsFallingSide(p1);
                bool isP2LeftHandSide = IsFallingSide(p2);
                bool isP3LeftHandSide = IsFallingSide(p3);

                if (isP1LeftHandSide || isP2LeftHandSide || isP3LeftHandSide)
                {
                    CutTriangle(ref _triangleCache, subMeshIndex);
                }
                else
                {
                    _fallingSide.AddTriangle(_triangleCache, subMeshIndex);
                }
            }

            //Material[] mats = victim.GetComponent<MeshRenderer>().sharedMaterials;

            //if (mats[mats.Length - 1] != capMaterial)
            //{
            //    // append capMaterial
            //    Material[] newMats = new Material[mats.Length + 1];
            //    mats.CopyTo(newMats, 0);
            //    newMats[newMats.Length - 1] = capMaterial;
            //    mats = newMats;
            //}

            // subMeshIndex for cap
            //CapCut(mats.Length - 1);

            Mesh fallingMesh = _fallingSide.GetMesh("Left HandSide Mesh");
            Mesh standingMesh = _standingSide.GetMesh("Right HandSide Mesh");
            _previosMesh = standingMesh;

            GameObject standingObject = _cubeGenerator.CreateCube(standingMesh, _capMaterial);
            GameObject fallingObject = _cubeGenerator.CreateCube(fallingMesh, _capMaterial);
            BoxCollider standingColl = standingObject.AddComponent<BoxCollider>();
            fallingObject.AddComponent<BoxCollider>();

            fallingObject.transform.position = victim.transform.position;
            fallingObject.transform.rotation = victim.transform.rotation;
            fallingObject.transform.localScale = victim.transform.localScale;
            if (victim.transform.parent != null)
            {
                fallingObject.transform.parent = victim.transform.parent;
            }

            //GameObject standingObject = new GameObject("Right Hand Side", typeof(MeshFilter), typeof(MeshRenderer));
            standingObject.transform.position = victim.transform.position;
            standingObject.transform.rotation = victim.transform.rotation;
            standingObject.transform.localScale = victim.transform.localScale;
            if (victim.transform.parent != null)
            {
                standingObject.transform.parent = victim.transform.parent;
            }

            sliced.Add(fallingObject);
            sliced.Add(standingObject);
            UpdateParram(standingColl);

            Destroy(victim);
        }

        _fallingSide.Clear();
        _fallingSide.Clear();
        newVerticesCache.Clear();

        return sliced.ToArray();
    }
    private bool IsFallingSide(Vector3 point)
    {
        if (_startPoint.z > 0)
        {
            return Mathf.Abs(Vector3.Dot(_normal, point)) - _disToMainBlock > _currentZLarge / 2;
        }
        else
        {
            return Mathf.Abs(Vector3.Dot(_normal, point)) - _disToMainBlock > _currentXLarge / 2;
        }
    }
    private float DistFromPlane(Vector3 point)
    {
        return Vector3.Dot(_normal, point);
    }
    void CutTriangle(ref Triangle triangleCache, int subMeshIndex)
    {
        // first, we split three points into two sides

        int falingCount = 0, rightCount = 0;

        for (int i = 0; i < 3; ++i)
        {
            bool isLeftHandSide = IsFallingSide(triangleCache.vertices[i]);

            if (isLeftHandSide)
            {
                fallingTriangleCache.vertices[falingCount] = triangleCache.vertices[i];
                fallingTriangleCache.uvs[falingCount] = triangleCache.uvs[i];
                fallingTriangleCache.tangents[falingCount] = triangleCache.tangents[i];
                fallingTriangleCache.normals[falingCount] = triangleCache.normals[i];
                ++falingCount;
            }
            else
            {
                standingTriangleCache.vertices[rightCount] = triangleCache.vertices[i];
                standingTriangleCache.uvs[rightCount] = triangleCache.uvs[i];
                standingTriangleCache.tangents[rightCount] = triangleCache.tangents[i];
                standingTriangleCache.normals[rightCount] = triangleCache.normals[i];
                ++rightCount;
            }
        }

        // Second, we fill the intersect points into newTriangleCache

        // Get single point that has no other point in the same side
        if (falingCount == 1)
        {
            triangleCache.vertices[0] = fallingTriangleCache.vertices[0];
            triangleCache.uvs[0] = fallingTriangleCache.uvs[0];
            triangleCache.normals[0] = fallingTriangleCache.normals[0];
            triangleCache.tangents[0] = fallingTriangleCache.tangents[0];

            triangleCache.vertices[1] = standingTriangleCache.vertices[0];
            triangleCache.uvs[1] = standingTriangleCache.uvs[0];
            triangleCache.normals[1] = standingTriangleCache.normals[0];
            triangleCache.tangents[1] = standingTriangleCache.tangents[0];

            triangleCache.vertices[2] = standingTriangleCache.vertices[1];
            triangleCache.uvs[2] = standingTriangleCache.uvs[1];
            triangleCache.normals[2] = standingTriangleCache.normals[1];
            triangleCache.tangents[2] = standingTriangleCache.tangents[1];
        }
        else
        {
            triangleCache.vertices[0] = standingTriangleCache.vertices[0];
            triangleCache.uvs[0] = standingTriangleCache.uvs[0];
            triangleCache.normals[0] = standingTriangleCache.normals[0];
            triangleCache.tangents[0] = standingTriangleCache.tangents[0];

            triangleCache.vertices[1] = fallingTriangleCache.vertices[0];
            triangleCache.uvs[1] = fallingTriangleCache.uvs[0];
            triangleCache.normals[1] = fallingTriangleCache.normals[0];
            triangleCache.tangents[1] = fallingTriangleCache.tangents[0];

            triangleCache.vertices[2] = fallingTriangleCache.vertices[1];
            triangleCache.uvs[2] = fallingTriangleCache.uvs[1];
            triangleCache.normals[2] = fallingTriangleCache.normals[1];
            triangleCache.tangents[2] = fallingTriangleCache.tangents[1];
        }

        // Get intersect point

        float d1 = 0.0f, d2 = 0.0f;
        float normalizedDistance = 0.0f;

        // Deal intersect point 1
        d1 = DistFromPlane(triangleCache.vertices[0]);
        d2 = DistFromPlane(triangleCache.vertices[1]);
        normalizedDistance = d1 / (d1 - d2);

        newTriangleCache.vertices[0] = Vector3.Lerp(triangleCache.vertices[0], triangleCache.vertices[1], normalizedDistance);
        newTriangleCache.uvs[0] = Vector2.Lerp(triangleCache.uvs[0], triangleCache.uvs[1], normalizedDistance);
        newTriangleCache.normals[0] = Vector3.Lerp(triangleCache.normals[0], triangleCache.normals[1], normalizedDistance);
        newTriangleCache.tangents[0] = Vector4.Lerp(triangleCache.tangents[0], triangleCache.tangents[1], normalizedDistance);

        // Deal intersect point 2
        d1 = DistFromPlane(triangleCache.vertices[0]);
        d2 = DistFromPlane(triangleCache.vertices[2]);
        normalizedDistance = d1 / (d1 - d2);

        newTriangleCache.vertices[1] = Vector3.Lerp(triangleCache.vertices[0], triangleCache.vertices[2], normalizedDistance);
        newTriangleCache.uvs[1] = Vector2.Lerp(triangleCache.uvs[0], triangleCache.uvs[2], normalizedDistance);
        newTriangleCache.normals[1] = Vector3.Lerp(triangleCache.normals[0], triangleCache.normals[2], normalizedDistance);
        newTriangleCache.tangents[1] = Vector4.Lerp(triangleCache.tangents[0], triangleCache.tangents[2], normalizedDistance);

        // record new create points
        if (newTriangleCache.vertices[0] != newTriangleCache.vertices[1])
        {
            newVerticesCache.Add(newTriangleCache.vertices[0]);
            newVerticesCache.Add(newTriangleCache.vertices[1]);
        }

        // Third, Connect intersect points with vertices

        // Again, Get Single point of the side. But this time we fill different data
        if (falingCount == 1)
        {
            // Connect Triangle: Single, intersect p1, intersect p2
            triangleCache.vertices[0] = fallingTriangleCache.vertices[0];
            triangleCache.uvs[0] = fallingTriangleCache.uvs[0];
            triangleCache.normals[0] = fallingTriangleCache.normals[0];
            triangleCache.tangents[0] = fallingTriangleCache.tangents[0];

            triangleCache.vertices[1] = newTriangleCache.vertices[0];
            triangleCache.uvs[1] = newTriangleCache.uvs[0];
            triangleCache.normals[1] = newTriangleCache.normals[0];
            triangleCache.tangents[1] = newTriangleCache.tangents[0];

            triangleCache.vertices[2] = newTriangleCache.vertices[1];
            triangleCache.uvs[2] = newTriangleCache.uvs[1];
            triangleCache.normals[2] = newTriangleCache.normals[1];
            triangleCache.tangents[2] = newTriangleCache.tangents[1];

            // Check Normal
            CheckNormal(ref triangleCache);

            _fallingSide.AddTriangle(triangleCache, subMeshIndex);

            // Connect Triangle: other side point 1, intersect p1, intersect p2
            triangleCache.vertices[0] = standingTriangleCache.vertices[0];
            triangleCache.uvs[0] = standingTriangleCache.uvs[0];
            triangleCache.normals[0] = standingTriangleCache.normals[0];
            triangleCache.tangents[0] = standingTriangleCache.tangents[0];

            triangleCache.vertices[1] = newTriangleCache.vertices[0];
            triangleCache.uvs[1] = newTriangleCache.uvs[0];
            triangleCache.normals[1] = newTriangleCache.normals[0];
            triangleCache.tangents[1] = newTriangleCache.tangents[0];

            triangleCache.vertices[2] = newTriangleCache.vertices[1];
            triangleCache.uvs[2] = newTriangleCache.uvs[1];
            triangleCache.normals[2] = newTriangleCache.normals[1];
            triangleCache.tangents[2] = newTriangleCache.tangents[1];

            // Check Normal
            CheckNormal(ref triangleCache);

            _standingSide.AddTriangle(triangleCache, subMeshIndex);

            // Connect Triangle: other side point 1, other side point 2, intersect p2
            triangleCache.vertices[0] = standingTriangleCache.vertices[0];
            triangleCache.uvs[0] = standingTriangleCache.uvs[0];
            triangleCache.normals[0] = standingTriangleCache.normals[0];
            triangleCache.tangents[0] = standingTriangleCache.tangents[0];

            triangleCache.vertices[1] = standingTriangleCache.vertices[1];
            triangleCache.uvs[1] = standingTriangleCache.uvs[1];
            triangleCache.normals[1] = standingTriangleCache.normals[1];
            triangleCache.tangents[1] = standingTriangleCache.tangents[1];

            triangleCache.vertices[2] = newTriangleCache.vertices[1];
            triangleCache.uvs[2] = newTriangleCache.uvs[1];
            triangleCache.normals[2] = newTriangleCache.normals[1];
            triangleCache.tangents[2] = newTriangleCache.tangents[1];

            // Check Normal
            CheckNormal(ref triangleCache);

            _standingSide.AddTriangle(triangleCache, subMeshIndex);
        }
        else
        {
            // Connect Triangle: Single, intersect p1, intersect p2
            triangleCache.vertices[0] = standingTriangleCache.vertices[0];
            triangleCache.uvs[0] = standingTriangleCache.uvs[0];
            triangleCache.normals[0] = standingTriangleCache.normals[0];
            triangleCache.tangents[0] = standingTriangleCache.tangents[0];

            triangleCache.vertices[1] = newTriangleCache.vertices[0];
            triangleCache.uvs[1] = newTriangleCache.uvs[0];
            triangleCache.normals[1] = newTriangleCache.normals[0];
            triangleCache.tangents[1] = newTriangleCache.tangents[0];

            triangleCache.vertices[2] = newTriangleCache.vertices[1];
            triangleCache.uvs[2] = newTriangleCache.uvs[1];
            triangleCache.normals[2] = newTriangleCache.normals[1];
            triangleCache.tangents[2] = newTriangleCache.tangents[1];

            // Check Normal
            CheckNormal(ref triangleCache);

            _standingSide.AddTriangle(triangleCache, subMeshIndex);

            // Connect Triangle: other side point 1, intersect p1, intersect p2
            triangleCache.vertices[0] = fallingTriangleCache.vertices[0];
            triangleCache.uvs[0] = fallingTriangleCache.uvs[0];
            triangleCache.normals[0] = fallingTriangleCache.normals[0];
            triangleCache.tangents[0] = fallingTriangleCache.tangents[0];

            triangleCache.vertices[1] = newTriangleCache.vertices[0];
            triangleCache.uvs[1] = newTriangleCache.uvs[0];
            triangleCache.normals[1] = newTriangleCache.normals[0];
            triangleCache.tangents[1] = newTriangleCache.tangents[0];

            triangleCache.vertices[2] = newTriangleCache.vertices[1];
            triangleCache.uvs[2] = newTriangleCache.uvs[1];
            triangleCache.normals[2] = newTriangleCache.normals[1];
            triangleCache.tangents[2] = newTriangleCache.tangents[1];

            // Check Normal
            CheckNormal(ref triangleCache);

            _fallingSide.AddTriangle(triangleCache, subMeshIndex);

            // Connect Triangle: other side point 1, other side point 2, intersect p2
            triangleCache.vertices[0] = fallingTriangleCache.vertices[0];
            triangleCache.uvs[0] = fallingTriangleCache.uvs[0];
            triangleCache.normals[0] = fallingTriangleCache.normals[0];
            triangleCache.tangents[0] = fallingTriangleCache.tangents[0];

            triangleCache.vertices[1] = fallingTriangleCache.vertices[1];
            triangleCache.uvs[1] = fallingTriangleCache.uvs[1];
            triangleCache.normals[1] = fallingTriangleCache.normals[1];
            triangleCache.tangents[1] = fallingTriangleCache.tangents[1];

            triangleCache.vertices[2] = newTriangleCache.vertices[1];
            triangleCache.uvs[2] = newTriangleCache.uvs[1];
            triangleCache.normals[2] = newTriangleCache.normals[1];
            triangleCache.tangents[2] = newTriangleCache.tangents[1];

            // Check Normal
            CheckNormal(ref triangleCache);

            _fallingSide.AddTriangle(triangleCache, subMeshIndex);
        }
    }
    void CheckNormal(ref Triangle triangleCache)
    {
        Vector3 crossProduct = Vector3.Cross(triangleCache.vertices[1] - triangleCache.vertices[0], triangleCache.vertices[2] - triangleCache.vertices[0]);
        Vector3 averageNormal = (triangleCache.normals[0] + triangleCache.normals[1] + triangleCache.normals[2]) / 3.0f;
        float dotProduct = Vector3.Dot(averageNormal, crossProduct);
        if (dotProduct < 0)
        {
            // swap index 0 and index 2
            Vector3 temp = triangleCache.vertices[2];
            triangleCache.vertices[2] = triangleCache.vertices[0];
            triangleCache.vertices[0] = temp;

            temp = triangleCache.normals[2];
            triangleCache.normals[2] = triangleCache.normals[0];
            triangleCache.normals[0] = temp;

            Vector2 temp2 = triangleCache.uvs[2];
            triangleCache.uvs[2] = triangleCache.uvs[0];
            triangleCache.uvs[0] = temp2;

            Vector4 temp3 = triangleCache.tangents[2];
            triangleCache.tangents[2] = triangleCache.tangents[0];
            triangleCache.tangents[0] = temp3;
        }
    }
}
public struct Triangle
{
    public Vector3[] vertices;
    public Vector2[] uvs;
    public Vector3[] normals;
    public Vector4[] tangents;

    public Triangle(Vector3[] vertices = null, Vector2[] uvs = null, Vector3[] normals = null, Vector4[] tangents = null)
    {
        this.vertices = vertices;
        this.uvs = uvs;
        this.normals = normals;
        this.tangents = tangents;
    }
}
