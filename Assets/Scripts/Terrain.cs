using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Terrain : MonoBehaviour
{

    public GameObject hoguera;
    public GameObject lago;
    public GameObject comida;
    public GameObject terreno;
    public float gen;
    public int limite = 30;
    int contador = 0;
    Transform newtransform;

    // Start is called before the first frame update
    void Start()
    {
        newtransform = terreno.GetComponent<Transform>();
        for (int i = 50; i < 450; i += Random.Range(50, 150))
        {
            for (int j = 50; j < 450; j += Random.Range(50, 150))
            {
                Instantiate(hoguera, new Vector3(
                    newtransform.position.x + i,
                    newtransform.position.y + 1,
                    newtransform.position.z + j),
                    Quaternion.identity);
            }

        }
        for (int i = 50; i < 450; i += Random.Range(50, 150))
        {
            for (int j = 50; j < 450; j += Random.Range(50, 150))
            {
                Instantiate(lago, new Vector3(
                    newtransform.position.x + i,
                    newtransform.position.y + 1,
                    newtransform.position.z + j),
                    Quaternion.identity);
            }

        }
    }

    // Update is called once per frame
    void Update()
    {
        gen += Time.deltaTime;
        if (gen >= 1 && contador <= limite)
        {
                    Instantiate(comida, new Vector3(
                        newtransform.position.x + Random.Range(50, 350),
                        newtransform.position.y + 1,
                        newtransform.position.z + Random.Range(50, 350)),
                        Quaternion.identity);
            contador++;
            
            gen = 0;
        }
    }
}
