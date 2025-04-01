using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // === MOVEMENT ===
    [SerializeField] int Speed = 5;
    Vector3 forwardDirection = new Vector3();
    Vector3 rightDirection = new Vector3();

    // === CAMERA ===
    //nothing here yet :)

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

    private void PlayerMove()
    {
        //// === MOVEMENT ===
        forwardDirection = MathsLib.EulerAnglesToDirection(eulerRotation);
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
