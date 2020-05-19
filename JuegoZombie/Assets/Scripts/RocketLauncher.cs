using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketLauncher : FireTemplate
{
    public float throughForce;
    public bool launched = false;
    public GameObject explosionPrefab;
    public override void DestroySelf()
    {
        Explosion();
    }

    public override void Fire()
    {
        launched = true;        
    }


    private void FixedUpdate()
    {
        if (launched)
        {
            GetComponent<Rigidbody>().AddForce(throughForce * transform.forward, ForceMode.Force);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        Explosion();
    }


    private void Explosion()
    {
        //Instanciar una explosion
        Instantiate(explosionPrefab, transform.position, transform.rotation);

        //Destruirse
        Destroy(this.gameObject);
    }
}
