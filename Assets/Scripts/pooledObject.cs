using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pooledObject : MonoBehaviour
{
    // Start is called before the first frame update
    void OnEnable()
    {
        Invoke("Disable",5);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Disable()
    {
        gameObject.SetActive(false);
    }
}
