using System.Collections;
using System.Collections.Generic;
//using System.Numerics;
using UnityEngine;
using UnityEngine.Animations;

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

    public static MyVector3 ToMyVector(Vector3 Vec1)
    {
        return new MyVector3(Vec1.x, Vec1.y, Vec1.z);
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
}