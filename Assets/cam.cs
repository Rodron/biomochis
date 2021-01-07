using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cam : MonoBehaviour
{
    // Start is called before the first frame update

    float xRotation;
    float yRotation;


    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        xRotation += 10*Input.GetAxis("Mouse X");
        yRotation += -1*10*Input.GetAxis("Mouse Y");
        this.transform.rotation = Quaternion.Euler(yRotation,xRotation,0.0f);
    }
}
