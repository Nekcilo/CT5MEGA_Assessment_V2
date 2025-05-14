using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

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

    float MainSpeed = 1f;
    float SecondarySpeed = 0.8f;

    float Timer;
    public float time;
    float Duration = 1f;

    enum STATE
    {
        UP,
        DOWN,
        LEFT,
        RIGHT,
        NONE
    }
    STATE state;

    // Update is called once per frame
    void Update()
    {
        PingPong();
        Square();
        TimerFunc();
    }

    void PingPong()
    {
        PinkVector = new MyVector3(PinkCube.transform.position.x, PinkCube.transform.position.y, PinkCube.transform.position.z);
        PurpleVector = new MyVector3(PurpleCube.transform.position.x, PurpleCube.transform.position.y, PurpleCube.transform.position.z);

        PinkCube.transform.position = MathsLib.LinearInterpolation(PinkCube.transform.position, transform.position, (SecondarySpeed * Time.deltaTime));

        Vector3 Position1 = new Vector3(35f, 1.5f, 5f);
        Vector3 Position2 = new Vector3(45f, 1.5f, 5f);

        PurpleCube.transform.position = MathsLib.LinearInterpolation(Position1, Position2, Mathf.PingPong(Time.time * MainSpeed, 1.0f));

    }

    void Square()
    {
        GreenVector = new MyVector3(GreenCube.transform.position.x, GreenCube.transform.position.y, GreenCube.transform.position.z);
        BlueVector = new MyVector3(BlueCube.transform.position.x, BlueCube.transform.position.y, BlueCube.transform.position.z);

        GreenCube.transform.position = MathsLib.LinearInterpolation(GreenCube.transform.position, BlueCube.transform.position, (SecondarySpeed * Time.deltaTime));

        MyVector3 Position3 = new MyVector3(35f, 1.5f, -5f);
        MyVector3 Position4 = new MyVector3(35f, 8f, -5f);
        MyVector3 Position5 = new MyVector3(45f, 8f, -5f);
        MyVector3 Position6 = new MyVector3(45f, 1.5f, -5f);

        float TempSpeed = 0.15f;

        // Move in current direction
        switch (state)
        {
            case STATE.UP:
                BlueCube.transform.position += new Vector3(0f, TempSpeed, 0f);
                if (MyVector3.Distance(MyVector3.ToMyVector(BlueCube.transform.position), Position4) < (TempSpeed * 1.1f))
                    state = STATE.RIGHT;
                break;

            case STATE.RIGHT:
                BlueCube.transform.position += new Vector3(TempSpeed, 0f, 0f);
                if (MyVector3.Distance(MyVector3.ToMyVector(BlueCube.transform.position), Position5) < (TempSpeed * 1.1f))
                    state = STATE.DOWN;
                break;

            case STATE.DOWN:
                BlueCube.transform.position -= new Vector3(0f, TempSpeed, 0f);
                if (MyVector3.Distance(MyVector3.ToMyVector(BlueCube.transform.position), Position6) < (TempSpeed * 1.1f))
                    state = STATE.LEFT;
                break;

            case STATE.LEFT:
                BlueCube.transform.position -= new Vector3(TempSpeed, 0f, 0f);
                if (MyVector3.Distance(MyVector3.ToMyVector(BlueCube.transform.position), Position3) < (TempSpeed * 1.1f))
                    state = STATE.UP;
                break;
        }

        //Debug.Log(state);
    }

    void TimerFunc()
    {
        time = Timer / Duration;
        Timer += (0.5f * Time.deltaTime);

        if (Count != 4)
        {
            if (Timer >= Duration)
            {
                Count++;
                Timer = 0;
            }
        }
        else
        {
            Count = 0;
        }
    }
}
