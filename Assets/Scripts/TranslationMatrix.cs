using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TranslationMatrix : MonoBehaviour
{
    [SerializeField] float Angle;

    void Update()
    {

    }

    public Matrix4by4 rollMatrix(float Angle)
    {
        //z axis
        Matrix4by4 rollMatrix = new Matrix4by4(
            new Vector3(Mathf.Cos(Angle), Mathf.Sin(Angle), 0),
            new Vector3(-Mathf.Sin(Angle), Mathf.Cos(Angle), 0),
            new Vector3(0, 0, 1),
            Vector3.zero
            );

        return rollMatrix; 
    }

    public Matrix4by4 pitchMatrix(float Angle)
    {
        //x axis
        Matrix4by4 pitchMatrix = new Matrix4by4(
            new Vector3(1, 0, 0),
            new Vector3(0, Mathf.Cos(Angle), Mathf.Sin(Angle)),
            new Vector3(0, -Mathf.Sin(Angle), Mathf.Cos(Angle)),
            Vector3.zero
            );

        return pitchMatrix;
    }

    public Matrix4by4 yawMatrix(float Angle)
    {
        //y axis
        Matrix4by4 yawMatrix = new Matrix4by4(
            new Vector3(Mathf.Cos(Angle), 0, -Mathf.Sin(Angle)),
            new Vector3(0, 1, 0),
            new Vector3(Mathf.Sin(Angle), 0, Mathf.Cos(Angle)),
            Vector3.zero
            );

        return yawMatrix;
    }

    public Matrix4x4 GetRotationMatrix(float pitch, float yaw, float roll)
    {
        Matrix4x4 pitchM = pitchMatrix(pitch);
        Matrix4x4 yawM = yawMatrix(yaw);
        Matrix4x4 rollM = rollMatrix(roll);

        return yawM * (pitchM * rollM)
    }

    // public Matrix4by4 RotateMesh(float Angle)
    // {
    //     Angle = Angle + 0.05f;

    //     //z axis
    //     Matrix4by4 rollMatrix = new Matrix4by4(
    //         new Vector3(Mathf.Cos(Angle), Mathf.Sin(Angle), 0),
    //         new Vector3(-Mathf.Sin(Angle), Mathf.Cos(Angle), 0),
    //         new Vector3(0, 0, 1),
    //         Vector3.zero
    //         );

    //     //x axis
    //     Matrix4by4 pitchMatrix = new Matrix4by4(
    //         new Vector3(1, 0, 0),
    //         new Vector3(0, Mathf.Cos(Angle), Mathf.Sin(Angle)),
    //         new Vector3(0, -Mathf.Sin(Angle), Mathf.Cos(Angle)),
    //         Vector3.zero
    //         );

    //     //y axis
    //     Matrix4by4 yawMatrix = new Matrix4by4(
    //         new Vector3(Mathf.Cos(Angle), 0, -Mathf.Sin(Angle)),
    //         new Vector3(0, 1, 0),
    //         new Vector3(Mathf.Sin(Angle), 0, Mathf.Cos(Angle)),
    //         Vector3.zero
    //         );

    //     Matrix4by4 R = yawMatrix * (pitchMatrix * rollMatrix);

    //     return R;
    // }
}
