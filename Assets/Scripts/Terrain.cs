using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Terrain : MonoBehaviour
{

    [SerializeField] GameObject hoguera;
    [SerializeField] GameObject lago;
    [SerializeField] GameObject[] comida;
    float gen = 0f;
    float genC = 0f;
    public int limite = 15;
    int contador = 0;    
    Transform newtransform;

    //Variables del mundo
    public int climate = 0; //  (0)-->Normal   (-1)-->Frío     (1)-->Calor
    public int population = 0;
    public int lastId;
    
    void Start()
    {
        newtransform = this.GetComponent<Transform>();

        for (int i = 100; i < 400; i += Random.Range(25, 100))
        {
            for (int j = 100; j < 400; j += Random.Range(25, 100))
            {
                if (0 != Random.Range(0, 2))
                {
                    Instantiate(hoguera, new Vector3(
                        newtransform.localPosition.x + i + Random.Range(-35, 35),
                        newtransform.localPosition.y + 1,
                        newtransform.localPosition.z + j + Random.Range(-35, 35)),
                        Quaternion.identity);
                }
                else
                {
                    Instantiate(lago, new Vector3(
                        newtransform.localPosition.x + i + Random.Range(-35, 35),
                        newtransform.localPosition.y + 1,
                        newtransform.localPosition.z + j + Random.Range(-35, 35)),
                        Quaternion.identity);
                }
            }
           
        }
        
                
        
    }
    
    void Update()
    {
        gen += Time.deltaTime;
        if (gen >= 1 && contador <= limite + population * 4)
        {
            int i = Random.Range(0,3);
                    Instantiate(comida[i], new Vector3(
                        newtransform.position.x + Random.Range(100, 400),
                        newtransform.position.y + 1,
                        newtransform.position.z + Random.Range(100, 400)),
                        Quaternion.identity);
            contador++;
            
            gen = 0;
        }

        genC += Time.deltaTime;
        if (genC >= 120)
        {
            climate = Random.Range(-1, 2);
            switch(climate){
                case -1:
                    break;
                case 0:
                    break;
                case 1:
                    break;
            }
            genC = 0;
        }
    }
}
