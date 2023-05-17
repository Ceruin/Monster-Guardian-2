using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class ThrowObject : MonoBehaviour
{
    public float throwSpeed = 10f;
    public Vector3 throwDirection = new Vector3(0, 1, 1);

    public void Throw()
    {
        Rigidbody rb = GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.AddForce(throwDirection.normalized * throwSpeed, ForceMode.VelocityChange);
        }
    }
}