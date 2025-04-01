using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // === MOVEMENT ===
    [SerializeField] int Speed = 5;
    Vector3 eulerRotation = new Vector3();
    Vector3 forwardDirection = new Vector3();
    Vector3 rightDirection = new Vector3();

    // === CAMERA ===
    float xSens = 0.05f;
    float ySens = 0.05f;
    [SerializeField] Camera Camera;

    float xRotation;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        PlayerMove();
        //if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
        //{
        //    transform.position += new Vector3(Input.GetAxis("Horizontal") * Speed * Time.deltaTime, 0, Input.GetAxis("Vertical") * Speed * Time.deltaTime);

        //}
    }

        private void CameraMove()
    {
        Vector3 CameraforwardDirection = new Vector3();
        Vector3 CamerarightDirection = new Vector3();

        // === CAMERA - LEFT and RIGHT ===
        float mouseY = Input.GetAxis("Mouse Y") * ySens;

        eulerRotation.x += mouseY;

        CameraforwardDirection = MathsLib.EulerAnglesToDirection(new Vector3(eulerRotation.x,0,0));
        CamerarightDirection = MyVector3.ToUnityVector(MathsLib.VectorCrossProduct(MyVector3.ToMyVector(CameraforwardDirection), MyVector3.ToMyVector(Camera.transform.up)));

        //CameraforwardDirection.x = Mathf.Clamp(CameraforwardDirection.x, -90f, 90f);
        Camera.transform.forward = CameraforwardDirection;
        Camera.transform.eulerAngles=new Vector3(Mathf.Clamp(Camera.transform.eulerAngles.x,-90,90),Camera.transform.eulerAngles.y,Camera.transform.eulerAngles.z);
        //Camera.transform.right = CamerarightDirection;
    }

    private void PlayerMove()
    {
        // === MOVEMENT ===
        float mouseX = Input.GetAxis("Mouse X") * xSens;
        eulerRotation.y -= mouseX;

        forwardDirection = MathsLib.EulerAnglesToDirection(new Vector3(0,eulerRotation.y,0));
        rightDirection = MyVector3.ToUnityVector(MathsLib.VectorCrossProduct(MyVector3.ToMyVector(forwardDirection), MyVector3.ToMyVector(transform.up)));

        transform.forward = forwardDirection;
        transform.right = rightDirection;

        if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
        {
           Vector3 PlayerVector = (transform.forward * (Input.GetAxis("Vertical"))) + (transform.right * (Input.GetAxis("Horizontal")));
           transform.position += PlayerVector * Speed * Time.deltaTime;
        }

    }
}
