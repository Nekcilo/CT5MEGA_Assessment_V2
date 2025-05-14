using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransformMatrix : MonoBehaviour
{
    Vector3[] ModelSpaceVertices;

    [SerializeField] MeshFilter meshFilter;
    [SerializeField] GameObject Object;

    [SerializeField] float Angle = 0.0f;
    [SerializeField] float Scale = 0.0f;
    [SerializeField] float Multiplier;
    [SerializeField] float Timer;
    bool Counting;

    [SerializeField] Vector3 VecPosition; 

    void Start()
    {
        ModelSpaceVertices = meshFilter.mesh.vertices;
        VecPosition = new Vector3(60, 3, 0);
    }

    void FixedUpdate()
    {
        TimerFunc();
        TRS();
    }

    void TimerFunc()
    {
        if (Counting)
        {
            Timer += 1.5f * Time.deltaTime;
            Multiplier = 0.025f;

            if (Timer >= 3f)
            {
                Counting = false;
            }
        }
        else
        {
            Timer -= 1.5f * Time.deltaTime;
            Multiplier = -0.025f;

            if (Timer <= 0f)
            {
                Counting = true;
            }
        }
    }

    public Matrix4by4 ScaleMesh()
    {
        //SCALE
        //Define a new array with the correct size
        Vector3[] TransformedVertices = new Vector3[ModelSpaceVertices.Length];

        Scale = Mathf.Clamp(Scale + Multiplier, 0.5f, 3f);

        //Creates our scaling matrix (2x, y, z)
        Matrix4by4 scaleMatrix = new Matrix4by4(
            new Vector3(1, 0, 0) * Scale,
            new Vector3(0, 1, 0) * Scale,
            new Vector3(0, 0, 1) * Scale,
            Vector3.zero
            );

        Matrix4by4 S = scaleMatrix;
        return S;
    }

    public Matrix4by4 TransformMesh()
    {
        //TRANSFORMATION
        //Define a new array with the correct size
        Vector3[] TransformedVertices = new Vector3[ModelSpaceVertices.Length];

        VecPosition.x = (VecPosition.x + Multiplier);
        VecPosition.y = (VecPosition.y + Multiplier);
        VecPosition.z = (VecPosition.z + Multiplier);

        //Creates our translation matrix
        Matrix4by4 translationMatrix = new Matrix4by4(
            new Vector4(1, 0, 0, 0),
            new Vector4(0, 1, 0, 0),
            new Vector4(0, 0, 1, 0),
            new Vector4(VecPosition.x - transform.position.x, VecPosition.y - transform.position.y, VecPosition.z - transform.position.z, 1)
            );

        Matrix4by4 T = translationMatrix;
        return translationMatrix;
    }

    public Matrix4by4 RotateMesh()
    {
        //ROTATION
        //Define a new array with the correct size
        Vector3[] TransformedVertices = new Vector3[ModelSpaceVertices.Length];

        Angle = Angle + Multiplier;

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

        return R;
    }

    public void TRS()
    {
        //Define a new array with the correct size
        Vector3[] TransformedVertices = new Vector3[ModelSpaceVertices.Length];
        Matrix4by4 S = ScaleMesh();
        Matrix4by4 R = RotateMesh();
        Matrix4by4 T = TransformMesh();

        transform.position = VecPosition;

        Matrix4by4 M = T * R * S;

        for (int i = 0; i < TransformedVertices.Length; i++)
        {
            TransformedVertices[i] = M * new Vector4(ModelSpaceVertices[i].x, ModelSpaceVertices[i].y, ModelSpaceVertices[i].z, 1.0f);
        }

        meshFilter.mesh.vertices = TransformedVertices;

        meshFilter.mesh.RecalculateNormals();
        meshFilter.mesh.RecalculateBounds();
    }
}
