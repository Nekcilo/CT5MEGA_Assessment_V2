using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class TransformMatrix : MonoBehaviour
{
    Vector3[] ModelSpaceVertices;

    [SerializeField] MeshFilter meshFilter;
    [SerializeField] GameObject Object;

    [SerializeField] float Angle = 0.0f;
    [SerializeField] float Scale = 0.0f;

    [SerializeField] Vector3 VecPosition; 

    void Start()
    {
        ModelSpaceVertices = meshFilter.mesh.vertices;
    }

    void FixedUpdate()
    {
        TRS();
    }

    public Matrix4by4 ScaleMesh()
    {
        //SCALE
        //Define a new array with the correct size
        Vector3[] TransformedVertices = new Vector3[ModelSpaceVertices.Length];

        Scale = Scale + 0.01f;

        //Creates our scaling matrix (2x, y, z)
        Matrix4by4 scaleMatrix = new Matrix4by4(
            new Vector3(1, 0, 0) * Scale,
            new Vector3(0, 1, 0) * Scale,
            new Vector3(0, 0, 1) * Scale,
            Vector3.zero
            );

        Matrix4by4 S = scaleMatrix;
        //Transform each individual vertex
        // for (int i = 0; i < TransformedVertices.Length; i++)
        // {
        //     TransformedVertices[i] = scaleMatrix * ModelSpaceVertices[i];
        //     Debug.Log(TransformedVertices[i]);
        // }

        // meshFilter.mesh.vertices = TransformedVertices;

        // meshFilter.mesh.RecalculateNormals();
        // meshFilter.mesh.RecalculateBounds();
        return S;
    }

    public Matrix4by4 TransformMesh()
    {
        //TRANSFORMATION
        //Define a new array with the correct size
        Vector3[] TransformedVertices = new Vector3[ModelSpaceVertices.Length];

        VecPosition.x = (VecPosition.x + 0.05f);
        VecPosition.y = (VecPosition.y + 0.05f);
        VecPosition.z = (VecPosition.z + 0.05f);

        //Creates our translation matrix
        Matrix4by4 translationMatrix = new Matrix4by4(
            new Vector3(1, 0, 0),
            new Vector3(0, 1, 0),
            new Vector3(0, 0, 1),
            new Vector3(VecPosition.x, VecPosition.y, VecPosition.z)
            );

        Matrix4by4 T = translationMatrix;
        return translationMatrix;
        //Transform each individual vertex
        // for (int i = 0; i < TransformedVertices.Length; i++)
        // {
        //     // ModelSpaceVertices is a Vector 3, so a Vector 4 is needed for translation (0, 0, 0, 1)
        //     TransformedVertices[i] = translationMatrix * new Vector4(ModelSpaceVertices[i].x, ModelSpaceVertices[i].y, ModelSpaceVertices[i].z, 1); // w = 1

        //     //This code doesn't work because the Vector 3 returns (0, 0, 0, (0)) as there is no 4th value
        //     //TransformedVertices[i] = translationMatrix * ModelSpaceVertices[i]; //Original Code from slides

        //     //Debug.Log(TransformedVertices[i]);
        // }

        // meshFilter.mesh.vertices = TransformedVertices;

        // meshFilter.mesh.RecalculateNormals();
        // meshFilter.mesh.RecalculateBounds();
        
    }

    public Matrix4by4 RotateMesh()
    {
        //ROTATION
        //Define a new array with the correct size
        Vector3[] TransformedVertices = new Vector3[ModelSpaceVertices.Length];

        Angle = Angle + 0.05f;

        //z axis
        Matrix4by4 rollMatrix = new Matrix4by4(
            new Vector3(Mathf.Cos(Angle), Mathf.Sin(Angle), 0),
            new Vector3(-Mathf.Sin(Angle), Mathf.Cos(Angle), 0),
            new Vector3(0, 0, 1),
            Vector3.zero
            );

        //x axis
        Matrix4by4 pitchMatrix = new Matrix4by4(
            new Vector3(1, 0, 0),
            new Vector3(0, Mathf.Cos(Angle), Mathf.Sin(Angle)),
            new Vector3(0, -Mathf.Sin(Angle), Mathf.Cos(Angle)),
            Vector3.zero
            );

        //y axis
        Matrix4by4 yawMatrix = new Matrix4by4(
            new Vector3(Mathf.Cos(Angle), 0, -Mathf.Sin(Angle)),
            new Vector3(0, 1, 0),
            new Vector3(Mathf.Sin(Angle), 0, Mathf.Cos(Angle)),
            Vector3.zero
            );

        Matrix4by4 R = yawMatrix * (pitchMatrix * rollMatrix);
        //Transform each individual vertex
        // for (int i = 0; i < TransformedVertices.Length; i++)
        // {
        //     TransformedVertices[i] = R * ModelSpaceVertices[i];
        // }

        // meshFilter.mesh.vertices = TransformedVertices;

        // meshFilter.mesh.RecalculateNormals();
        // meshFilter.mesh.RecalculateBounds();

        return R;
    }

    public void TRS()
    {
        //Define a new array with the correct size
        Vector3[] TransformedVertices = new Vector3[ModelSpaceVertices.Length];
        Matrix4by4 R = RotateMesh();
        Matrix4by4 S = ScaleMesh();
        Matrix4by4 T = TransformMesh();

        Matrix4by4 M = T * (R * S);

        for (int i = 0; i < TransformedVertices.Length; i++)
        {
            TransformedVertices[i] = M * ModelSpaceVertices[i];
        }

        meshFilter.mesh.vertices = TransformedVertices;

        meshFilter.mesh.RecalculateNormals();
        meshFilter.mesh.RecalculateBounds();
    }
}
