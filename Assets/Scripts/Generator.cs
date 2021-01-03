using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Generator : MonoBehaviour
{
    [SerializeField] Color gencolor;
    [SerializeField] int dieta;
    [SerializeField] int gloton;
    [SerializeField] float size;
    [SerializeField] GameObject biomochi;

    // Start is called before the first frame update
    void Start()
    {
        biomochi.GetComponent<Renderer>().material.color = gencolor;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
