using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Teleport : MonoBehaviour
{

    //Ping Pong Follow
    [SerializeField] GameObject PurpleCube;
    [SerializeField] GameObject PinkCube;
    MyVector3 PurpleVector;
    MyVector3 PinkVector;

    //Square Follow
    [SerializeField] GameObject BlueCube;
    [SerializeField] GameObject GreenCube;
    MyVector3 BlueVector;
    MyVector3 GreenVector;
    int Count;

    float Speed = 1f;
    float Speed2 = 0.8f;

    // Update is called once per frame
    void Update()
    {
        //Ping Pong Follow
        PinkVector = new MyVector3(PinkCube.transform.position.x, PinkCube.transform.position.y, PinkCube.transform.position.z);
        PurpleVector = new MyVector3(PurpleCube.transform.position.x, PurpleCube.transform.position.y, PurpleCube.transform.position.z);

        PinkCube.transform.position = MathsLib.LinearInterpolation(PinkCube.transform.position, transform.position, (Speed2 * Time.deltaTime));

        Vector3 Position1 = new Vector3(35f, 1.5f, 5f);
        Vector3 Position2 = new Vector3(45f, 1.5f, 5f);

        PurpleCube.transform.position = MathsLib.LinearInterpolation(Position1, Position2, Mathf.PingPong(Time.time * Speed, 1.0f));

        //Square Follow
        GreenVector = new MyVector3(GreenCube.transform.position.x, GreenCube.transform.position.y, GreenCube.transform.position.z);
        BlueVector = new MyVector3(BlueCube.transform.position.x, BlueCube.transform.position.y, BlueCube.transform.position.z);

        GreenCube.transform.position = MathsLib.LinearInterpolation(GreenCube.transform.position, BlueCube.transform.position, (Speed2 * Time.deltaTime));

        Vector3 Position3 = new Vector3(35f, 1.5f, -5f);
        Vector3 Position4 = new Vector3(35f, 4.5f, -5f);
        Vector3 Position5 = new Vector3(45f, 4.5f, -5f);
        Vector3 Position6 = new Vector3(45f, 1.5f, -5f);

        switch (Count)
        {
            case 0:
                BlueCube.transform.position = MathsLib.LinearInterpolation(Position3, Position4, (Time.deltaTime));
                Count++;
                break;
            case 1:
                BlueCube.transform.position = MathsLib.LinearInterpolation(Position4, Position5, (Time.deltaTime));
                Count++;
                break;
            case 2:
                BlueCube.transform.position = MathsLib.LinearInterpolation(Position5, Position6, (Time.deltaTime));
                Count++;
                break;
            case 3:
                BlueCube.transform.position = MathsLib.LinearInterpolation(Position6, Position3, (Time.deltaTime));
                Count = 0;
                break;
        }

    }
}
