using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{

    MyVector3 hidden_dir;
    MyVector3 Player_pos;
    [SerializeField] GameObject Player;

    MyVector3 Distance;
    float LODistance;
    float LOhidden_dir;
    float Angle;


    // Start is called before the first frame update
    void Start()
    {
        hidden_dir = MyVector3.ToMyVector(transform.forward); //sets hidden_dir to the current forward direction in the editor
        Player_pos = MyVector3.ToMyVector(Player.transform.position);
    }

    // Update is called once per frame
    void Update()
    {
        Distance = Player_pos - MyVector3.ToMyVector(transform.position); // getting the distance from the player position to the constant hidden_dir

        MyVector3.DotProduct(Distance, hidden_dir, true);

        //LODistance = Distance.Length(); // calculating the length of the new distance vector

        //LOhidden_dir = hidden_dir.Length(); // calculating the length of the original hidden_dir distance

        ////these two lengths now make up 2 sides of a triangle, where the angle between can now be calculated

        //Angle = Mathf.Cos(LOhidden_dir / LODistance); //SohCahToa

        //Quat tempQuat = new Quat(Angle, 0, 1, 0); //Applying this angle to the Y axis (w,x,y,z)

        //transform.rotation = Quat.ToUnityQuat(tempQuat); //Applying the new quaternion to the objects rotation
    }
}
