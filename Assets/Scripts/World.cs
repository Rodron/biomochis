using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class World : MonoBehaviour
{
    
    [SerializeField] GameObject hoguera;
    [SerializeField] GameObject lago;
    [SerializeField] GameObject[] comida;
    [SerializeField] GameObject luz;
    [SerializeField] GameObject mochi;
    float gen = 0f;
    float genC = 0f;
    public int limite = 35;
    public int contador = 0;    
    Transform newtransform;

    //Variables del mundo
    float tclima = 0f;

    public int climate = 2; //  (default)-->Normal   (0)-->Frío     (1)-->Calor
    int current;
    public int population = 0;
    public int lastId;
    
    void Start()
    {
        current = climate;
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

    public void dios() {
        mochi.GetComponent<NewBT>().diosInteractua = true;    
    }

    public void randomBorn() {

        Biomochi randomBoi = mochi.GetComponent<Biomochi>();

        randomBoi.dieta = (Biomochi.Dietas) Random.Range(0,4);
        randomBoi.color = new Color(Random.Range(0f,1f), Random.Range(0f, 1f), Random.Range(0f, 1f));
        randomBoi.size = Random.Range(1f,3f);
        randomBoi.gloton = Random.Range(1,6);

       Instantiate(
            randomBoi.gameObject, new Vector3(
            Camera.main.transform.localPosition.x,
            0,
             Camera.main.transform.localPosition.z),
            Quaternion.identity);

    }
    public void forceClimate(int i) {
        climate = i;

        if (current != climate)
        {

            Debug.Log(climate);
            switch (climate)
            {
                case 0: //frio

                    Camera.main.GetComponent<Animator>().SetTrigger("frio");
                    luz.GetComponent<Animator>().SetTrigger("frio");

                    break;

                case 1: //calor

                    Camera.main.GetComponent<Animator>().SetTrigger("calor");
                    luz.GetComponent<Animator>().SetTrigger("calor");

                    break;

                default: //normal

                    Camera.main.GetComponent<Animator>().SetTrigger("normal");
                    luz.GetComponent<Animator>().SetTrigger("normal");

                    break;
            }
            current = climate;
            genC = 0;
        }
       
    }

    public void godFood() {

        int f = Random.Range(0, 3);
        Instantiate(comida[f], new Vector3(
            Camera.main.transform.localPosition.x,
            Camera.main.transform.localPosition.y + 1,
             Camera.main.transform.localPosition.z),
            Quaternion.identity);
        contador++;

        gen = 0;

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
        if (genC >= tclima)
        {
            tclima = Random.Range(30, 90);
            climate = Random.Range(0, 4);
            
            if (climate > 2) { climate = 2;}
            forceClimate(climate);
            genC = 0;
        }
    }
}
