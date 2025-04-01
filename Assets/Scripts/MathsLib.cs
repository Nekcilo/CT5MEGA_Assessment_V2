using System.Collections;
using System.Collections.Generic;
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

    // public static Vector3 EulerAnglesToDirection(Vector3 EulerAngles)
    // {
    //     Vector3 rv = new Vector3();

    //     rv.x = Mathf.Cos(EulerAngles.y) * Mathf.Cos(EulerAngles.x);
    //     rv.y = Mathf.Sin(EulerAngles.x);
    //     rv.z = Mathf.Cos(EulerAngles.x) * Mathf.Sin(EulerAngles.y);

    //     return rv;
    // }
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

    static float DotProduct(MyVector3 Vec1, MyVector3 Vec2, bool ShouldNormalize = true)
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


    public Vector3 ToUnityVector()
    {
        return new Vector3(xpos, ypos, zpos);
    }
}
