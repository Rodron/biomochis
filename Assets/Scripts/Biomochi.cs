using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Biomochi : MonoBehaviour
{
    //genes heredados
    static float standardEDV = 300;
    static float standardH = 10;
    float energy = 1000f;
    float timerHambre = 0;
    float t;
    int id;
    Vector3 dir = new Vector3(0,0,0);
    float movT;
    float coolmov = 0;
    float persueVel = 1f;
    public bool idle = true;


    public bool isInZombieState = false;
    public bool isInSexyState = false;
    float zombieLifeTime = 100000;
    public bool stop = false;
    float changeDirT = 0f;
    [SerializeField] Color color;
    public enum Dietas { carnivoro, hervivoro, omnivoro };
    [SerializeField] int gloton = 3;
    [SerializeField] float size = 1.0f;
    [SerializeField] public Dietas dieta;
    Random biomochiRandom = new Random();
    GameObject objective = null;

    public List<GameObject> foodInRange = new List<GameObject>();
    public List<GameObject> biomochisInRange = new List<GameObject>();
    public List<GameObject> islandsInRange = new List<GameObject>();
    public List<GameObject> firecampInRange = new List<GameObject>();

    //genes aleatorios

    public enum Genes
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

    [SerializeField]const int limitGen = 3;

    public Dictionary<Genes, object> genes = new Dictionary<Genes, object>(limitGen);

    //atributos

    public bool sexo; //true H, false M;
    [SerializeField] float velocidad; //depende tamaño
    int hambre; //0+glotoneria
    [SerializeField] float edad;
    [SerializeField] GameObject prefab;

    public void Born(){        
        World mundo = GameObject.FindGameObjectWithTag("world").GetComponent<World>();
        id = mundo.lastId;
        sexo = Random.Range(0,2) != 0;
        hambre = 0;
        edad=0;
        mundo.population++;
        mundo.lastId++;
        velocidad = size / 45;
        randomGen();
        UpdateVisuals();
        energy = 1000f*(id+1);

        gameObject.transform.GetChild(4).gameObject.SetActive(sexo);        
    }

    public void Die()
    {
        if(genes.ContainsKey(Genes.Zombie)){
            isInZombieState = true;
            zombieLifeTime = 0;
        }else{
            GameObject.FindGameObjectWithTag("world").GetComponent<World>().population--;
            gameObject.SetActive(false);
        }
        
        //DestroyObject(gameObject);
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
                case 0: genes.Add(Genes.Social, Random.Range(0.2f, 1.0f)); break;
                case 1: genes.Add(Genes.Flatulencia,Random.Range(0.25f, 0.85f)); break;
                case 2: genes.Add(Genes.EDV, Random.Range(0.25f, 1.75f)); break;
                case 3: genes.Add(Genes.Canibal, true); break;
                case 4: genes.Add(Genes.Cultismo, true); break;
                case 5: genes.Add(Genes.Zombie, true); break;
                case 6: genes.Add(Genes.Jugueton, true); break;
                case 7: genes.Add(Genes.Aventurero, true); break;
                case 8: genes.Add(Genes.Tenacidad,true); break;
                case 9: genes.Add(Genes.Metabolismo, Random.Range(0.8f, 1.2f)); break;
            }
            /*
            Debug.Log((Genes) i);
            Debug.Log(genes[(Genes) i]);
            */
        }
    }

    public void modoSexo(Biomochi mochiPartner) {
        // Executar animação

        // Cuando a animação foi executada
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


        GameObject child = Instantiate(prefab,new Vector3 (this.gameObject.GetComponent<Transform>().position.x - .5f, this.gameObject.GetComponent<Transform>().position.y, this.gameObject.GetComponent<Transform>().position.z + .2f), Quaternion.identity);

        child.GetComponent<Biomochi>().InstantiateAttrib(newColor,newGloton,newSize,newDieta);
    }

    public void InstantiateAttrib(Color sourceColor, int sourceGloton, float sourceSize, int sourceDiet){
        this.color = sourceColor;
        this.gloton = sourceGloton;
        this.size = sourceSize;

        switch (sourceDiet)
        {
            case 0: this.dieta = Dietas.carnivoro; break;
            case 1: this.dieta = Dietas.hervivoro; break;
            case 2: this.dieta = Dietas.omnivoro; break;
        }
        
        velocidad = size/10;

        //this.randomGen();

        UpdateVisuals();
    }

    public void UpdateVisuals(){
        this.gameObject.GetComponentInChildren<Renderer>().material.color = this.color;
        this.gameObject.GetComponent<Transform>().localScale *= this.size;
    }

    public void Move(){
        this.gameObject.GetComponent<Animator>().SetBool("Movimiento", true);
        if (this.genes.ContainsKey(Genes.Jugueton)){
            this.gameObject.GetComponent<Animator>().SetBool("Jugueton", true);
        }
        //Random.InitState((Random.Range(id, id+100000)));
        if(idle){
            if(movT>changeDirT){

                changeDirT = Random.Range(2.5f,5f);
                dir = newDirection();
                movT = 0;
                transform.LookAt(transform.position+dir);
            }
            timerHambre += Time.deltaTime;
            spendEnergy();
        }
        else{
            gameObject.transform.LookAt(new Vector3(objective.transform.position.x, 0, objective.transform.position.z));            
        }
        transform.Translate(dir*velocidad*persueVel, Space.World);
        //transform.Translate(transform.forward*velocidad);
    }

    Vector3 newDirection(){
        return new Vector3 (Random.Range(-1f,2f),0,Random.Range(-1f,2f)).normalized;
    }

    public GameObject ChooseNearest(Vector3 location, List<GameObject> destinations)
    {
        try
        {
            float nearestSqrMag = float.PositiveInfinity;
            GameObject nearestGameObj = null;            

            for (int i = 0; i < destinations.Count; i++)
            {
                float sqrMag = (destinations[i].transform.position - location).sqrMagnitude;
                if (sqrMag < nearestSqrMag)
                {
                    nearestSqrMag = sqrMag;
                    nearestGameObj = destinations[i];
                    destinations.RemoveAt(i);
                }
            }
            return nearestGameObj;
        }
        catch (System.Exception e) { throw; }
    }

    void OnTriggerExit(Collider obj)
    {
        if (obj.gameObject.tag.Equals("limit"))
        //if (obj.gameObject.tag.Equals("world"))
        {
            dir = -transform.position.normalized;
            changeDirT = Random.Range(6f, 9f);
            movT = 0;
            transform.LookAt(transform.position + dir);
        }
        else if (obj.gameObject.tag.Equals("Interact"))
        {
            Debug.Log("mochi pequeño sale");
        }
        else if (obj.gameObject.tag.Equals("Perception"))
        {
            Debug.Log("mochi grande");
        }
        else if (obj.gameObject.tag.Equals("food")||obj.gameObject.tag.Equals("food_V")||obj.gameObject.tag.Equals("food_C"))
        {
            foodInRange.Remove(obj.gameObject);
        }
        else if (obj.gameObject.tag.Equals("Biomochi"))
        {
            biomochisInRange.Remove(obj.gameObject);
        }
        else if (obj.gameObject.tag.Equals("agua"))
        {
            islandsInRange.Remove(obj.gameObject);
        }
        else if (obj.gameObject.tag.Equals("hoguera"))
        {
            firecampInRange.Remove(obj.gameObject);
        }
    }

    void OnTriggerEnter(Collider obj)
    {
        if (obj.gameObject.tag.Equals("food")||obj.gameObject.tag.Equals("food_V")||obj.gameObject.tag.Equals("food_C"))
        {
            foodInRange.Add(obj.gameObject);
        }
        else if (obj.gameObject.tag.Equals("Biomochi"))
        {
            biomochisInRange.Add(obj.gameObject);
        }
        else if (obj.gameObject.tag.Equals("agua"))
        {
            islandsInRange.Add(obj.gameObject);
        }
        else if (obj.gameObject.tag.Equals("hoguera"))
        {
            firecampInRange.Add(obj.gameObject);
        }
    }

    public void Persue(GameObject objectiveObj)
    {
        idle = false;
        this.objective = objectiveObj;
        persueVel = 1.2f;
    }

    public void spendEnergy(){
      //Random.InitState(id*(Random.Range(id, id+100000)));
      energy -= Random.Range(0.4f*id%3,1.3f);
      if(energy <= 0){
          this.gameObject.GetComponent<Animator>().SetBool("Movimiento", false);
          this.gameObject.GetComponent<Animator>().SetBool("Jugueton", false);
          movT = 0;
          stop = true;
      }
    }

    public void Start()
    {        
        Born();
    }

    public void Update(){
        zombieLifeTime += Time.deltaTime;
        movT += Time.deltaTime;
        if(!stop)
            Move();
        if(movT>changeDirT && energy <= 0){
            energy = 1000f*(id+1);
            movT = 0;
            stop = false;
        }
        if (genes.ContainsKey(Genes.Metabolismo))
        {
            if(timerHambre > standardH  * (float) genes[Genes.Metabolismo])
            {
                hambre++;
            }
        
        }else if (timerHambre > standardH)
        {
            hambre++;
        }
        
        edad+=Time.deltaTime;
        if (isInZombieState)
        {
            if (genes.ContainsKey(Genes.EDV))
            {
                if (zombieLifeTime >= 60  * (float)genes[Genes.EDV])
                {
                    GameObject.FindGameObjectWithTag("world").GetComponent<World>().population--;
                    gameObject.SetActive(false);
                }
            }
            else if (zombieLifeTime >= 60)
            {
                GameObject.FindGameObjectWithTag("world").GetComponent<World>().population--;
                gameObject.SetActive(false);
            }
        }
        else{
            if(genes.ContainsKey(Genes.EDV)){            
            if(edad >= standardEDV * (float) genes[Genes.EDV]){
                Die();
            }
        }        
        else if(edad >= standardEDV){
            Die();            
        }        
        }
        
    }
}

