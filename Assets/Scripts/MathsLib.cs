using System.Collections;
using System.Collections.Generic;
//using System.Numerics;
using UnityEngine;

public class MathsLib
{
    public static float VectorToRadians(Vector2 Vec1)
    {
        //Turning the X and Y from the vector in radians (angle)
        return Mathf.Atan(Vec1.y / Vec1.x);
    }

    public static Vector2 RadiansToVector(float Angle)
    {
        //Turning the radians (angles) into the X and Y for the Vector
        return new Vector2(Mathf.Cos(Angle), Mathf.Sin(Angle)); //(x, y)
    }

    public static MyVector3 VectorCrossProduct(MyVector3 A, MyVector3 B)
    {
        return new MyVector3(((A.ypos * B.zpos) - (A.zpos * B.ypos)), ((A.zpos * B.xpos) - (A.xpos * B.zpos)), ((A.xpos * B.ypos) - (A.ypos * B.xpos)));
    }

    public static Vector3 VectorCrossProduct(Vector3 A, Vector3 B)
    {
        return new Vector3(((A.y * B.z) - (A.z * B.y)), ((A.z * B.x) - (A.x * B.z)), ((A.x * B.y) - (A.y * B.x)));
    }

    public static Vector3 EulerAnglesToDirection(Vector3 EulerAngles)
    {
        Vector3 rv = new Vector3();

        rv.x = Mathf.Cos(EulerAngles.y) * Mathf.Cos(EulerAngles.x);
        rv.y = Mathf.Sin(EulerAngles.x);
        rv.z = Mathf.Cos(EulerAngles.x) * Mathf.Sin(EulerAngles.y);

        return rv;
    }

    public static float YDirection(float Input1)
    {
        return Mathf.Sin(Input1); // Y = Sin(P)
    }

    public static float XDirection(float Input2)
    {
        return Mathf.Sin(Input2); // Y = Sin(P)
    }

    public static MyVector3 LinearInterpolation(MyVector3 Vec1, MyVector3 Vec2, float T)
    {
        return Vec1 * (1.0f - T) + Vec2 * T;
    }

    public static Vector3 LinearInterpolation(Vector3 Vec1, Vector3 Vec2, float T)
    {
        return Vec1 * (1.0f - T) + Vec2 * T;
    }
}

public class MyVector3
{
    public float xpos;
    public float ypos;
    public float zpos;

    public MyVector3 rv;

    public MyVector3(float x, float y, float z)
    {
        xpos = x;
        ypos = y;
        zpos = z;
    }

    public static MyVector3 Addition(MyVector3 Vec1, MyVector3 Vec2)
    {
        //Initialise and Add all 3 vectors and return the value
        return new MyVector3(Vec1.xpos + Vec2.xpos, Vec1.ypos + Vec2.ypos, Vec1.zpos + Vec2.zpos);
    }


    public static MyVector3 Subtraction(MyVector3 Vec1, MyVector3 Vec2)
    {
        //Initialise and Subtract all 3 vectors and return the value
        return new MyVector3(Vec1.xpos - Vec2.xpos, Vec1.ypos - Vec2.ypos, Vec1.zpos - Vec2.zpos);
    }


    public static MyVector3 Multiply(MyVector3 Vec1, float Scalar)
    {
        return new MyVector3((Vec1.xpos * Scalar), (Vec1.ypos * Scalar), (Vec1.zpos * Scalar));
    }


    public static MyVector3 Divide(MyVector3 Vec1, float Divisor)
    {
        return new MyVector3((Vec1.xpos / Divisor), (Vec1.ypos / Divisor), (Vec1.zpos / Divisor));
    }

    public static float Divide(float Vec1, float Vec2)
    {
        float rv; 

        return rv = Vec1 / Vec2;
    }

    public MyVector3 Normalizing()
    {
        return Divide(new MyVector3(xpos, ypos, zpos), Length());
    }

    public static float DotProduct(MyVector3 Vec1, MyVector3 Vec2, bool ShouldNormalize = true)
    {
        float rv;

        if (ShouldNormalize)
        {
            MyVector3 A = Vec1.Normalizing();
            MyVector3 B = Vec2.Normalizing();

            return rv = A.xpos * B.xpos + A.ypos * B.ypos + A.zpos * B.zpos;
        }
        else
        {
            return rv = Vec1.xpos * Vec2.xpos + Vec1.ypos * Vec2.ypos + Vec1.zpos * Vec2.zpos;
        }
    }

    public float LengthSq()
    {     
        //Squared
        return xpos * xpos + ypos * ypos + zpos * zpos;
    }


    public float Length()
    {
        //Square Root
        return Mathf.Sqrt((xpos * xpos) + (ypos * ypos) + (zpos * zpos));
    }


    public static Vector3 ToUnityVector(MyVector3 Vec1)
    {
        return new Vector3(Vec1.xpos, Vec1.ypos, Vec1.zpos);
    }

    public static Vector2 ToUnityVector2(MyVector3 Vec1)
    {
        return new Vector2(Vec1.xpos, Vec1.zpos);
    }

    public static Vector3 ToUnityVector(float fl1, float fl2, float fl3)
    {
        return new Vector3(fl1, fl2, fl3);
    }

    public static MyVector3 ToMyVector(Vector3 Vec1)
    {
        return new MyVector3(Vec1.x, Vec1.y, Vec1.z);
    }

    public MyVector3 ToMyVector()
    {
        return new MyVector3(xpos, ypos, zpos);
    }


    //Overloads yippppppeeeeeee
    public static MyVector3 operator *(MyVector3 Vec1, float f)
    {
        return Multiply(Vec1, f);
    }

    public static MyVector3 operator *(float f, MyVector3 Vec1)
    {
        return Multiply(Vec1, f);
    }


    public static MyVector3 operator /(MyVector3 Vec1, float f)
    {
        return Divide(Vec1, f);
    }

    public static MyVector3 operator +(MyVector3 Vec1, MyVector3 Vec2)
    {
        return Addition(Vec1, Vec2);
    }

    public static MyVector3 operator -(MyVector3 Vec1, MyVector3 Vec2)
    {
        return Subtraction(Vec1, Vec2);
    }

    public static MyVector3 operator -(MyVector3 Vec1)
    {
        return new MyVector3(-Vec1.xpos, -Vec1.ypos, -Vec1.zpos);
    }
}

public class Quat
{
    public float w, x, y, z;

    public Quat(float Angle, MyVector3 Axis)
    {
        float halfAngle = Angle / 2;
        w = Mathf.Cos(halfAngle);
        x = Axis.xpos * Mathf.Sin(halfAngle);
        y = Axis.ypos * Mathf.Sin(halfAngle);
        z = Axis.zpos * Mathf.Sin(halfAngle);
    }

    public Quat(float QuatW, float QuatX, float QuatY, float QuatZ)
    {
        w = QuatW;
        x = QuatX;
        y = QuatY;
        z = QuatZ;
    }

    public MyVector3 axis
    {
        get 
        {
            return new MyVector3(x, y, z);
        }

        set
        {
            x = value.xpos;
            y = value.ypos;
            z = value.zpos;
        }
    }

    public static Quat operator*(Quat S, Quat R)
    {
        return new Quat (((S.w * R.w) - MyVector3.DotProduct(S.axis, R.axis, false)), ((R.axis * S.w) + (R.w * S.axis) + MathsLib.VectorCrossProduct(R.axis, S.axis)));
    }

    public Quat Inverse()
    {
        return new Quat(w, -axis);
    }

    public Vector4 GetAxisAngle()
    {
        Vector4 rv = new Vector4();

        float halfAngle = Mathf.Acos(w);
        rv.w = halfAngle * 2;

        rv.x = x / Mathf.Sin(halfAngle);
        rv.y = y / Mathf.Sin(halfAngle);
        rv.z = z / Mathf.Sin(halfAngle);

        return rv;
    }

    public static Quat SLERP(Quat q, Quat r, float t)
    {
        t = Mathf.Clamp(t, 0.0f, 1.0f);

        Quat d = r * q.Inverse();
        Vector4 AxisAngle = d.GetAxisAngle();
        Quat dT = new Quat(AxisAngle.w * t, new MyVector3(AxisAngle.x, AxisAngle.y, AxisAngle.z));
    
        return dT * q;
    }

    public static Quaternion ToUnityQuat(Quat Quat1)
    {
        return new Quaternion(Quat1.x, Quat1.y, Quat1.z, Quat1.w);
    }
    public static Quat ToMyQuat(Quaternion Quat1)
    {
        return new Quat(Quat1.x, Quat1.y, Quat1.z, Quat1.w);
    }
}

public class Matrix4by4
{
    public float[,] values; // [row, col]

    public Matrix4by4(Vector4 column1, Vector4 column2, Vector4 column3, Vector4 column4)
    {
        values = new float[4,4];

        //     column 1    |
        values[0, 0] = column1.x; 
        values[1, 0] = column1.y;
        values[2, 0] = column1.z;
        values[3, 0] = column1.w;

        //      column2      |
        values[0, 1] = column2.x;
        values[1, 1] = column2.y;
        values[2, 1] = column2.z;
        values[3, 1] = column2.w;

        //      column 3     |
        values[0, 2] = column3.x;
        values[1, 2] = column3.y;
        values[2, 2] = column3.z;
        values[3, 2] = column3.w;

        //      column 4     |
        values[0, 3] = column4.x;
        values[1, 3] = column4.y;
        values[2, 3] = column4.z;
        values[3, 3] = column4.w;
    }

    public Matrix4by4(Vector3 column1, Vector3 column2, Vector3 column3, Vector3 column4)
    {
        values = new float[4, 4];
        //     column 1    |      column2      |      column 3     |      column 4     |
        values[0, 0] = column1.x; values[0, 1] = column2.x; values[0, 2] = column3.x; values[0, 3] = column4.x;
        values[1, 0] = column1.y; values[1, 1] = column2.y; values[1, 2] = column3.y; values[1, 3] = column4.y;
        values[2, 0] = column1.z; values[2, 1] = column2.z; values[2, 2] = column3.z; values[2, 3] = column4.z;
        values[3, 0] = 0; values[3, 1] = 0; values[3, 2] = 0; values[3, 3] = 1;
    }

    public static Vector4 operator *(Matrix4by4 Matrix, Vector4 Vector)
    {
        return new Vector4(
            ((Matrix.values[0, 0] * Vector.x) + (Matrix.values[0, 1] * Vector.y) + (Matrix.values[0, 2] * Vector.z) + (Matrix.values[0, 3] * Vector.w)),
            ((Matrix.values[1, 0] * Vector.x) + (Matrix.values[1, 1] * Vector.y) + (Matrix.values[1, 2] * Vector.z) + (Matrix.values[1, 3] * Vector.w)),
            ((Matrix.values[2, 0] * Vector.x) + (Matrix.values[2, 1] * Vector.y) + (Matrix.values[2, 2] * Vector.z) + (Matrix.values[2, 3] * Vector.w)),
            ((Matrix.values[3, 0] * Vector.x) + (Matrix.values[3, 1] * Vector.y) + (Matrix.values[3, 2] * Vector.z) + (Matrix.values[3, 3] * Vector.w))
            );
    }

    public static float operator *(Matrix4by4 Matrix, float Float)
    {
        return Matrix * Float;
    }

    public static Matrix4by4 operator *(Matrix4by4 Matrix1, Matrix4by4 Matrix2)
    {
        return new Matrix4by4
            (
              new Vector4(
              (Matrix1.values[0, 0] * Matrix2.values[0, 0]) + (Matrix1.values[1, 0] * Matrix2.values[0, 1]) + (Matrix1.values[2, 0] * Matrix2.values[0, 2]) + (Matrix1.values[3, 0] * Matrix2.values[0, 3]),
              (Matrix1.values[0, 1] * Matrix2.values[0, 0]) + (Matrix1.values[1, 1] * Matrix2.values[0, 1]) + (Matrix1.values[2, 1] * Matrix2.values[0, 2]) + (Matrix1.values[3, 1] * Matrix2.values[0, 3]),
              (Matrix1.values[0, 2] * Matrix2.values[0, 0]) + (Matrix1.values[1, 2] * Matrix2.values[0, 1]) + (Matrix1.values[2, 2] * Matrix2.values[0, 2]) + (Matrix1.values[3, 2] * Matrix2.values[0, 3]),
              (Matrix1.values[0, 3] * Matrix2.values[0, 0]) + (Matrix1.values[1, 3] * Matrix2.values[0, 1]) + (Matrix1.values[2, 3] * Matrix2.values[0, 2]) + (Matrix1.values[3, 3] * Matrix2.values[0, 3])
              ),

              new Vector4(
              (Matrix1.values[0, 0] * Matrix2.values[1, 0]) + (Matrix1.values[1, 0] * Matrix2.values[1, 1]) + (Matrix1.values[2, 0] * Matrix2.values[1, 2]) + (Matrix1.values[3, 0] * Matrix2.values[1, 3]),
              (Matrix1.values[0, 1] * Matrix2.values[1, 0]) + (Matrix1.values[1, 1] * Matrix2.values[1, 1]) + (Matrix1.values[2, 1] * Matrix2.values[1, 2]) + (Matrix1.values[3, 1] * Matrix2.values[1, 3]),
              (Matrix1.values[0, 2] * Matrix2.values[1, 0]) + (Matrix1.values[1, 2] * Matrix2.values[1, 1]) + (Matrix1.values[2, 2] * Matrix2.values[1, 2]) + (Matrix1.values[3, 2] * Matrix2.values[1, 3]),
              (Matrix1.values[0, 3] * Matrix2.values[1, 0]) + (Matrix1.values[1, 3] * Matrix2.values[1, 1]) + (Matrix1.values[2, 3] * Matrix2.values[1, 2]) + (Matrix1.values[3, 3] * Matrix2.values[1, 3])
              ),

              new Vector4(
              (Matrix1.values[0, 0] * Matrix2.values[2, 0]) + (Matrix1.values[1, 0] * Matrix2.values[2, 1]) + (Matrix1.values[2, 0] * Matrix2.values[2, 2]) + (Matrix1.values[3, 0] * Matrix2.values[2, 3]),
              (Matrix1.values[0, 1] * Matrix2.values[2, 0]) + (Matrix1.values[1, 1] * Matrix2.values[2, 1]) + (Matrix1.values[2, 1] * Matrix2.values[2, 2]) + (Matrix1.values[3, 1] * Matrix2.values[2, 3]),
              (Matrix1.values[0, 2] * Matrix2.values[2, 0]) + (Matrix1.values[1, 2] * Matrix2.values[2, 1]) + (Matrix1.values[2, 2] * Matrix2.values[2, 2]) + (Matrix1.values[3, 2] * Matrix2.values[2, 3]),
              (Matrix1.values[0, 3] * Matrix2.values[2, 0]) + (Matrix1.values[1, 3] * Matrix2.values[2, 1]) + (Matrix1.values[2, 3] * Matrix2.values[2, 2]) + (Matrix1.values[3, 3] * Matrix2.values[2, 3])
              ),

              new Vector4(
              (Matrix1.values[0, 0] * Matrix2.values[3, 0]) + (Matrix1.values[1, 0] * Matrix2.values[3, 1]) + (Matrix1.values[2, 0] * Matrix2.values[3, 2]) + (Matrix1.values[3, 0] * Matrix2.values[3, 3]),
              (Matrix1.values[0, 1] * Matrix2.values[3, 0]) + (Matrix1.values[1, 1] * Matrix2.values[3, 1]) + (Matrix1.values[2, 1] * Matrix2.values[3, 2]) + (Matrix1.values[3, 1] * Matrix2.values[3, 3]),
              (Matrix1.values[0, 2] * Matrix2.values[3, 0]) + (Matrix1.values[1, 2] * Matrix2.values[3, 1]) + (Matrix1.values[2, 2] * Matrix2.values[3, 2]) + (Matrix1.values[3, 2] * Matrix2.values[3, 3]),
              (Matrix1.values[0, 3] * Matrix2.values[3, 0]) + (Matrix1.values[1, 3] * Matrix2.values[3, 1]) + (Matrix1.values[2, 3] * Matrix2.values[3, 2]) + (Matrix1.values[3, 3] * Matrix2.values[3, 3])
              )
            );

    }

    public static Matrix4by4 Identity
    {
        get
        {
            return new Matrix4by4(
                new Vector4(1, 0, 0, 0),
                new Vector4(0, 1, 0, 0),
                new Vector4(0, 0, 1, 0),
                new Vector4(0, 0, 0, 1)
            );
        }
    }

    public Matrix4by4 TranslationInverse()
    {
        Matrix4by4 rv = Identity;

        rv.values[0, 3] = -values[0, 3];
        rv.values[1, 3] = -values[1, 3];
        rv.values[2, 3] = -values[2, 3];

        return rv;
    }

    public Vector4 col_1
    {
        get { return new Vector4(values[0, 0], values[1, 0], values[2, 0], values[3, 0]); }
    }
    public Vector4 col_2
    {
        get { return new Vector4(values[0, 1], values[1, 1], values[2, 1], values[3, 1]); }
    }
    public Vector4 col_3
    {
        get { return new Vector4(values[0, 2], values[1, 2], values[2, 2], values[3, 2]); }
    }
    public Vector4 col_4
    {
        get { return new Vector4(values[0, 3], values[1, 3], values[2, 3], values[3, 3]); }
    }

    public Matrix4x4 ToUnity()
    {
        return new Matrix4x4(col_1, col_2, col_3, col_4);
    }

    public Quat ToQuat()
    {
        float T = 1 + values[0, 0] + values[1, 1] + values[2, 2];

        float S = Mathf.Sqrt(T) * 2;
        return new Quat(
            (0.25f * S),
            ((values[1, 2] - values[2, 1]) / S),
            ((values[2, 0] - values[0, 2]) / S),
            ((values[0, 1] - values[1, 0]) / S)
        );
    }
}