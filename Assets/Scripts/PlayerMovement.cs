using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Inspection")]
    [SerializeField] TranslationMatrix CubeMatrix;
    public float rotationSpeed = 0.01f;
    private Vector3 previousMousePosition;

    [Header("Movement")]
    [SerializeField] int Speed = 5;
    [SerializeField] GameObject Capsule;
    Vector3 eulerRotation = new Vector3();
    Vector3 forwardDirection = new Vector3();
    Vector3 rightDirection = new Vector3();

    [Header("Camera")]
    [SerializeField] Camera Camera;
    MyVector3 currentEulerAngles = new MyVector3(0,0,0);
    Vector3 CameraVector;
    float Sens = 500f;
    float mouseX = 0;
    float mouseY = 0;
    Vector3 upDirection = new Vector3();

    float xRotation;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = false;
        Camera.transform.eulerAngles = Vector3.zero;
    }

    // Update is called once per frame
    private void Update()
    {
        if (Input.GetMouseButton(1))
        {
            Cursor.lockState = CursorLockMode.None;
            CubeSpin();
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
            PlayerMove();
            CameraMove();
        }
    }

    private void CameraMove()
    {
        upDirection = MathsLib.VectorCrossProduct(forwardDirection, rightDirection);
        //transform.up = upDirection;

        if (Input.GetAxis("Mouse X") != 0 || Input.GetAxis("Mouse Y") != 0)
        {
            CameraVector.x = Input.GetAxis("Mouse X");
            CameraVector.y = Input.GetAxis("Mouse Y");


            MyVector3 CameraInput;

            CameraInput = new MyVector3(-CameraVector.y, CameraVector.x, 0f);

            CameraInput = (CameraInput * Time.deltaTime * Sens); //Must be seperate otherwise it errors :) because MyVector3 doesn't like multiplying like this unless it's seperate

            currentEulerAngles = currentEulerAngles + CameraInput; //Must be seperate otherwise it errors :) Because MyVector3 can't += overload

            currentEulerAngles.xpos = Mathf.Clamp(currentEulerAngles.xpos, -90, 90);

            Camera.transform.eulerAngles = MyVector3.ToUnityVector(currentEulerAngles);

            Capsule.transform.eulerAngles = MyVector3.ToUnityVector(0f, currentEulerAngles.ypos, 0f);
        }
    }

    private void PlayerMove()
    {
        forwardDirection = Camera.transform.forward;
        rightDirection = MyVector3.ToUnityVector(MathsLib.VectorCrossProduct(MyVector3.ToMyVector(forwardDirection), MyVector3.ToMyVector(transform.up)));

        if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
        {
           Vector3 PlayerVector = (forwardDirection * (Input.GetAxis("Vertical"))) + (rightDirection * (Input.GetAxis("Horizontal")));
           transform.position += PlayerVector * Speed * Time.deltaTime;
        }
    }

    private void CubeSpin()
    {

        mouseY -= Input.GetAxisRaw("Mouse X") * Time.deltaTime;
        mouseX += Input.GetAxisRaw("Mouse Y") * Time.deltaTime;

        //if (Input.GetKey(KeyCode.E))
        //{
        //    Z += 1 * rotationSpeed * Time.deltaTime;
        //}
        //if (Input.GetKey(KeyCode.Q))
        //{
        //    Z -= 1 * rotationSpeed * Time.deltaTime;
        //}

                                        // X                Y            Z
        Vector3 Angle = new Vector3(mouseX * rotationSpeed, 0, mouseY * rotationSpeed);

        CubeMatrix.TransformObject(Angle);


    }
}
