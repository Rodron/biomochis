
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class CustomNeedMachine{    
    public GameObject biomochi;
    public int currentState;
    public bool order;
    public bool llegadaAlObjetivo;
    GameObject objective = null;

    
    public bool exit;
    
    public bool animacionTerminada;

    public CustomNeedMachine(GameObject biom){
        currentState = 0;
        biomochi = biom;
        exit = true;
    }

    

    //public void Update(string typeOfObjective,bool animacionTerminada, bool llegadaAlObjetivo){
        //this.animacionTerminada = animacionTerminada;
        //this.llegadaAlObjetivo = llegadaAlObjetivo;
    public void Update(string typeOfObjective){

        switch(currentState){
            case 0 :
                lookForObjectivePerception (typeOfObjective);
                break;
            case 1 : 
                arrivedAtObjectivePerception();
                break;
            case 2 :
                interactionWithObjectivePerception(typeOfObjective);
                break;
        }

    }


    
    void lookForObjectivePerception(string typeOfObjective){
        if(biomochi.GetComponent<Biomochi>().id == 0)
            Debug.Log("BUSCANDO " + typeOfObjective);
        if(typeOfObjective == "Biomochi"){
            if (biomochi.GetComponent<Biomochi>().isInZombieState)
            {
                objective = biomochi.GetComponent<Biomochi>().ChooseNearest(biomochi.transform.position, biomochi.GetComponent<Biomochi>().biomochisInRange);
                if(objective!=null && !objective.GetComponent<Biomochi>().isInZombieState){
                    if(biomochi.GetComponent<Biomochi>().id == 0)
                        Debug.Log("BIIIIOOOMOOOOOOCHI");
                    currentState = 1;
                    biomochi.GetComponent<Biomochi>().Persue(objective);
                }
            }
            else if(biomochi.GetComponent<Biomochi>().isInSexyState){
                objective = biomochi.GetComponent<Biomochi>().ChooseNearest(biomochi.transform.position, biomochi.GetComponent<Biomochi>().biomochisInRange);
                if(objective!=null && objective.GetComponent<Biomochi>().sexo != biomochi.GetComponent<Biomochi>().sexo && biomochi.GetComponent<Biomochi>().isInSexyState && !objective.GetComponent<Biomochi>().isInZombieState){
                    if(biomochi.GetComponent<Biomochi>().id == 0)
                        Debug.Log("BIOMOCHI ZUKULEMTO OBJETIVO");
                    currentState = 1;
                    biomochi.GetComponent<Biomochi>().Persue(objective);
                }
            }
            else if(biomochi.GetComponent<Biomochi>().genes.ContainsKey(Biomochi.Genes.Social)){                
                objective = biomochi.GetComponent<Biomochi>().ChooseNearest(biomochi.transform.position, biomochi.GetComponent<Biomochi>().biomochisInRange);
                if(objective!=null && !objective.GetComponent<Biomochi>().isInZombieState){
                    if(biomochi.GetComponent<Biomochi>().id == 0)
                        Debug.Log("BIOMOCHI QUIERO AMIGOS OBJETIVO");
                    currentState = 1;
                    biomochi.GetComponent<Biomochi>().Persue(objective);
                }
            }            
        }
        else{
            if (typeOfObjective == "hoguera")
            {
                objective = biomochi.GetComponent<Biomochi>().ChooseNearest(biomochi.transform.position, biomochi.GetComponent<Biomochi>().campfireInRange);                
            }
            else if (typeOfObjective == "agua")
            {
                objective = biomochi.GetComponent<Biomochi>().ChooseNearest(biomochi.transform.position, biomochi.GetComponent<Biomochi>().islandsInRange);
            }
            else if (typeOfObjective == "food")
            {
                objective = biomochi.GetComponent<Biomochi>().ChooseNearest(biomochi.transform.position, biomochi.GetComponent<Biomochi>().foodInRange);                
            }
            
            if(objective != null){
                biomochi.GetComponent<Biomochi>().Persue(objective);
            }
        }
    }

    void arrivedAtObjectivePerception(){
        if(biomochi.GetComponent<Biomochi>().id == 0)            
            Debug.Log("LLEGANDO A OBJETIVO" );
        if(llegadaAlObjetivo){
            llegadaAlObjetivo = false;
            if(biomochi.GetComponent<Biomochi>().id == 0)
                Debug.Log("EN EL OBJETIVO");
            currentState = 2;            
        }
    }

    void interactionWithObjectivePerception(string typeOfObjective)
    {
        Debug.Log("INTERACTUANDO CON EL OBJETIVO");
        Debug.Log("mi tipo ez: " + typeOfObjective);
        //if (order)
        //{
            order = false;
        
            if (typeOfObjective == "Biomochi")
            {
                Debug.Log("soy bobo");
                if (biomochi.GetComponent<Biomochi>().isInZombieState)
                {
                    biomochi.GetComponent<Biomochi>().kill(objective);
                }
                else if (biomochi.GetComponent<Biomochi>().isInSexyState)
                {
                    biomochi.GetComponent<Biomochi>().modoSexo(objective.GetComponent<Biomochi>());
                }
                else if (biomochi.GetComponent<Biomochi>().genes.ContainsKey(Biomochi.Genes.Social))
                {
                    Debug.Log("SocIAliZandOO");
                    biomochi.GetComponent<Animator>().SetTrigger("Interaccion");
                }
            }
            else
            {
                if (typeOfObjective == "hoguera")
                {
                    Debug.Log("calentito");
                    ////quedarse quieto
                    biomochi.GetComponent<Biomochi>().Refugio();
                }
                else if (typeOfObjective == "agua")
                {
                    Debug.Log("fresquito");
                    biomochi.GetComponent<Biomochi>().Refugio();
                }
                else if (typeOfObjective == "food")
                {
                    Debug.Log("comiendo algo");
                    ////comer
                    biomochi.GetComponent<Biomochi>().comer(objective);
                }
            }
        //}
        if (animacionTerminada)
        {
            animacionTerminada = false;
            order = true;
            if(biomochi.GetComponent<Biomochi>().id == 0)
                Debug.Log("HE TERMINADO DE INTERACTUAR");
            exit = true;
            biomochi.GetComponent<Biomochi>().idle = true;
            biomochi.GetComponent<Biomochi>().stop = false;
            if(objective.tag.Equals("Biomochi")){
                    objective.GetComponent<Biomochi>().stop = false;
                    objective.GetComponent<Biomochi>().idle = true;
                    //objective.transform.LookAt(gameObject.transform.position);
            }            
        }
    }     
}