using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;

public class QuatTest : MonoBehaviour
{
    [SerializeField] float t;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // === WITHOUT SLERP ===
        // angle += Time.deltaTime;

        // Quat q = new Quat(angle, new MyVector3(0, 1, 0));

        // MyVector3 p = new MyVector3(1, 2, 3);

        // Quat K = new Quat(1, p);

        // Quat newK = q * K * q.Inverse();

        // MyVector3 newP = newK.axis;

        // transform.rotation = Quat.ToUnityQuat(newK);

        // === ADDING SLERP ===

        t += Time.deltaTime * 0.5f;

        Quat q = new Quat(Mathf.PI * 0.5f, new MyVector3(0, 1, 0));
        Quat r = new Quat(Mathf.PI * 0.25f, new MyVector3(1, 0, 0));

        Quat slerped = Quat.SLERP(q, r, t);

        MyVector3 p = new MyVector3(1, 2, 3);

        Quat K = new Quat(1, p);

        Quat newK = slerped * K * slerped.Inverse();

        transform.rotation = Quat.ToUnityQuat(newK);     
    }
}
