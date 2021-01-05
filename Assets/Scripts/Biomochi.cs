using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Biomochi : MonoBehaviour
{
    //genes heredados
    
    [SerializeField] Color color;
    enum Dietas { carnivoro, hervivoro, omnivoro };
    [SerializeField] int gloton = 3;
    [SerializeField] float size = 1.0f;

    [SerializeField] Dietas dieta;

    //genes aleatorios

    enum Genes
    {
        Social,
        Flatulencia,
        EDV,
        Canibal,
        Cultismo,
        Zombie,
        Jugueton,
        Aventurero,
        Tenacidad,
        Metabolismo,
    };

    [SerializeField] float[] genC;

    const int limitGen = 3;
    [SerializeField]List<Genes> genes = new List<Genes>(limitGen);

    //atributos

    [SerializeField] bool sexo; //true H, false M;
    [SerializeField] float velocidad; //depende tamaño
    [SerializeField] int hambre; //0-glotoneria
    [SerializeField] float edad;

    //constructor

    public Biomochi(Color color, int gloton, float size, int dietaTipo)
    {
        this.color = color;
        this.gloton = gloton;
        this.size = size;
        

        switch (dietaTipo)
        {
            case 0: dieta = Dietas.carnivoro; break;
            case 1: dieta = Dietas.hervivoro; break;
            case 2: dieta = Dietas.omnivoro; break;
        }

        sexo = Random.Range(0,2) != 0;
        velocidad = size;
        hambre = 0;
        edad = 0.0f;

        randomGen();
    }

    void randomGen()
    {
        HashSet<int> gen = new HashSet<int>();

        while (gen.Count < limitGen)
        {
            gen.Add(Random.Range(0, 10));
        };

        foreach (int i in gen)
        {
            switch (i)
            {
                case 0: genes.Add(Genes.Social); genC[i] = Random.Range(0.2f,1.0f); break;
                case 1: genes.Add(Genes.Flatulencia); genC[i] = Random.Range(0.25f, 0.85f); break;
                case 2: genes.Add(Genes.EDV); genC[i] = Random.Range(0.25f, 1.75f); break;
                case 3: genes.Add(Genes.Canibal); break;
                case 4: genes.Add(Genes.Cultismo); break;
                case 5: genes.Add(Genes.Zombie); break;
                case 6: genes.Add(Genes.Jugueton); break;
                case 7: genes.Add(Genes.Aventurero); break;
                case 8: genes.Add(Genes.Tenacidad); break;
                case 9: genes.Add(Genes.Metabolismo); genC[i] = Random.Range(0.8f, 1.2f); break;
            }
        }
    }

    public Biomochi modoSexo(Biomochi mochiPartner) {

        float v = Random.Range(0.0f, 1.0f);
        Color newColor = v * this.color + (1 - v) * mochiPartner.color;

        v = Random.Range(0.0f, 1.0f);
        int newGloton = (int) (v*this.gloton + (1-v) * mochiPartner.gloton);

        v = Random.Range(0.0f, 1.0f);
        float newSize = v * this.size + (1 - v) * mochiPartner.size;
        int newDieta;
        if (this.dieta == mochiPartner.dieta)
        {
            newDieta = (int) this.dieta;
        }
        else {
            newDieta = Random.Range(0, 3);
        }

        return new Biomochi(newColor,newGloton,newSize,newDieta);
    }

    public void Start()
    {
        gameObject.GetComponentInChildren<Renderer>().material.color = this.color;
        gameObject.GetComponent<Transform>().localScale *= this.size;
    }
}
