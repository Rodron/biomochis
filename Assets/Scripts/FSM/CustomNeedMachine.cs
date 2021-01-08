
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

    

    public void Update(string typeOfObjective,bool animacionTerminada, bool llegadaAlObjetivo){
        this.animacionTerminada = animacionTerminada;
        this.llegadaAlObjetivo = llegadaAlObjetivo;
        

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
        Debug.Log("BUSCANDO " + typeOfObjective);
        if(typeOfObjective == "Biomochi"){
            if (biomochi.GetComponent<Biomochi>().isInZombieState)
            {
                objective = biomochi.GetComponent<Biomochi>().ChooseNearest(biomochi.transform.position, biomochi.GetComponent<Biomochi>().biomochisInRange);
                if(objective!=null && !objective.GetComponent<Biomochi>().isInZombieState){
                    Debug.Log("BIIIIOOOMOOOOOOCHI");
                    currentState = 1;
                    biomochi.GetComponent<Biomochi>().Persue(objective);
                }
            }
            else if(biomochi.GetComponent<Biomochi>().isInSexyState){
                objective = biomochi.GetComponent<Biomochi>().ChooseNearest(biomochi.transform.position, biomochi.GetComponent<Biomochi>().biomochisInRange);
                if(objective!=null && objective.GetComponent<Biomochi>().sexo != biomochi.GetComponent<Biomochi>().sexo && biomochi.GetComponent<Biomochi>().isInSexyState && !objective.GetComponent<Biomochi>().isInZombieState){
                    Debug.Log("BIOMOCHI ZUKULEMTO OBJETIVO");
                    currentState = 1;
                    biomochi.GetComponent<Biomochi>().Persue(objective);
                }
            }
            else if(biomochi.GetComponent<Biomochi>().genes.ContainsKey(Biomochi.Genes.Social)){                
                objective = biomochi.GetComponent<Biomochi>().ChooseNearest(biomochi.transform.position, biomochi.GetComponent<Biomochi>().biomochisInRange);
                if(objective!=null && !objective.GetComponent<Biomochi>().isInZombieState){
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
        Debug.Log("LLEGANDO A OBJETIVO" );
        if(llegadaAlObjetivo){
            llegadaAlObjetivo = false;
            Debug.Log("EN EL OBJETIVO");
            currentState = 2;            
        }
    }

    void interactionWithObjectivePerception(string typeOfObjective)
    {
        Debug.Log("INTERACTUANDO CON EL OBJETIVO");
        if (order)
        {
            order = false;

            if (typeOfObjective == "Biomochi")
            {
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
                    biomochi.GetComponent<Animator>().SetTrigger("Interaccion");

                }
            }
            else
            {
                if (typeOfObjective == "hoguera")
                {
                    ////quedarse quieto
                    biomochi.GetComponent<Biomochi>().Refugio();
                }
                else if (typeOfObjective == "agua")
                {
                    biomochi.GetComponent<Biomochi>().Refugio();
                }
                else if (typeOfObjective == "food")
                {
                    ////comer
                    biomochi.GetComponent<Biomochi>().comer(objective);
                }
            }
        }
        if (animacionTerminada)
        {
            animacionTerminada = false;
            order = true;
            Debug.Log("HE TERMINADO DE INTERACTUAR");
            exit = true;
            biomochi.GetComponent<Biomochi>().idle = true;
            biomochi.GetComponent<Biomochi>().stop = false;

        }
    }
    
}