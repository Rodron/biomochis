using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Terrain : MonoBehaviour
{

    [SerializeField] GameObject hoguera;
    [SerializeField] GameObject lago;
    [SerializeField] GameObject comida;
    float gen = 0f;
    float genC = 0f;
    int foodCount = 0;
    Transform newtransform;

    //Variables del mundo
    int climate = 0;
    public int population = 0;    
    
    void Start()
    {
        newtransform = this.GetComponent<Transform>();

        for (int i = 100; i < 400; i += Random.Range(50, 100))
        {
            for (int j = 100; j < 400; j += Random.Range(50, 100))
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
        if (gen >= 1 && foodCount <= population*4)
        {
            Instantiate(comida, new Vector3(
                newtransform.position.x + Random.Range(100, 400),
                newtransform.position.y + 1,
                newtransform.position.z + Random.Range(100, 400)),
                Quaternion.identity);
            
            foodCount++;
            
            gen = 0;
        }


        genC += Time.deltaTime;
        if (gen >= 120)
        {
            climate = Random.Range(-1, 2);            
            gen = 0;
        }
    }
}
