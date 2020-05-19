using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PelotaLauncher : FireTemplate
{
    public float throughForce;

    public override void DestroySelf()
    {
        Destroy(this.gameObject);
    }

    public override void Fire()
    {
        GetComponent<Rigidbody>().AddForce(throughForce * transform.forward, ForceMode.Impulse);
    }

}
