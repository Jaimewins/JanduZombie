using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoDestroy : MonoBehaviour
{
    public float timeDestroy = 1.0f;
    // Start is called before the first frame update
    void Start()
    {
        Invoke("Destruyete", timeDestroy);
    }

    // Update is called once per frame
    void Destruyete()
    {
        Destroy(this.gameObject);
    }
}
