using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{

    MyVector3 hidden_dir;
    [SerializeField] GameObject Player;

    MyVector3 Distance;
    float LODistance;
    float LOhidden_dir;
    float Angle;


    // Start is called before the first frame update
    void Start()
    {
        hidden_dir = MyVector3.ToMyVector(transform.forward); //sets hidden_dir to the current forward direction in the editor
    }

    // Update is called once per frame
    void Update()
    {
        Distance = MyVector3.ToMyVector(Player.transform.position) - hidden_dir;

        LODistance = Distance.Length();

        LOhidden_dir = hidden_dir.Length();

        Angle = Mathf.Sin(LOhidden_dir / LODistance);

        Quat tempQuat = new Quat(0, 0, Angle, 0);

        transform.rotation = Quat.ToUnityQuat(tempQuat);
    }
}
