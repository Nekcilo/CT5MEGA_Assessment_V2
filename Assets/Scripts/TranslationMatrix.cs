using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TranslationMatrix : MonoBehaviour
{
    //[SerializeField] float Angle;
    [SerializeField] Vector3 Angle2;
    [SerializeField] GameObject Object;

    void FixedUpdate()
    {
        //Angle.x = Angle.x + 0.01f;
        //Angle.y = Angle.y + 0.01f;
        //Angle.z = Angle.z + 0.01f;
        //TransformObject(Angle2);
    }

    public void TransformObject(Vector3 Angle)
    {

        Object.transform.rotation = Quat.ToUnityQuat(GetRotationMatrix(Angle).ToQuat());
        //Debug.Log(Object.transform.rotation);
        Angle2 = Angle;

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

    public Matrix4by4 GetRotationMatrix(Vector3 Angle)
    {
        Matrix4by4 pitchM = pitchMatrix(Angle.x);
        Matrix4by4 yawM = yawMatrix(Angle.y);
        Matrix4by4 rollM = rollMatrix(Angle.z);

        return yawM * (pitchM * rollM);
    }

}
