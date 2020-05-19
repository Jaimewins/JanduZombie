using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PelotaLauncher2 : FireTemplate2
{
    public float launchForce = 5.0f;
    public override void Fire()
    {
        Debug.Log("Lanzamos pelota");
        GetComponent<Rigidbody>().AddForce(transform.forward * launchForce, ForceMode.Impulse);
    }
}
