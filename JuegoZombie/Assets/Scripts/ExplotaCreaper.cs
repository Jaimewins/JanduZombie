using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplotaCreaper : MonoBehaviour
{
    // Update is called once per frame
    public void ExplotaBro()
    {

        //instanciar explosion

        //explotar creaper
        Destroy(gameObject.transform.parent.gameObject);
    }
}
