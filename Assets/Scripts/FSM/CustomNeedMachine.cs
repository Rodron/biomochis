
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

class CustomNeedMachine{

    
    public GameObject biomochi;
    public int currentState;
    bool llegadaAlObjetivo;
    GameObject objective;

    
    public bool exit;
    
    bool animacionTerminada;

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
                interactionWithObjectivePerception();
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
                    Debug.Log("BIOMOCHI OBJETIVO");
                    currentState = 1;
                }
            }
            else if(biomochi.GetComponent<Biomochi>().isInSexyState){
                objective = biomochi.GetComponent<Biomochi>().ChooseNearest(biomochi.transform.position, biomochi.GetComponent<Biomochi>().biomochisInRange);
                if(objective!=null && objective.GetComponent<Biomochi>().sexo != biomochi.GetComponent<Biomochi>().sexo && !objective.GetComponent<Biomochi>().isInZombieState){
                    Debug.Log("BIOMOCHI SUCULENTO OBJETIVO");
                    currentState = 1;                    
                }
            }
            else if(biomochi.GetComponent<Biomochi>().genes.ContainsKey(Biomochi.Genes.Social)){                
                objective = biomochi.GetComponent<Biomochi>().ChooseNearest(biomochi.transform.position, biomochi.GetComponent<Biomochi>().biomochisInRange);
                if(objective!=null && !objective.GetComponent<Biomochi>().isInZombieState){
                    Debug.Log("BIOMOCHI QUIERO AMIGOS OBJETIVO");
                    currentState = 1;
                }
            }
            biomochi.GetComponent<Biomochi>().Persue(objective);
            return;
        }
        else if (typeOfObjective == "hoguera")
        {
            //objective = biomochi.GetComponent<Biomochi>().ChooseNearest(biomochi.transform.position, biomochi.GetComponent<Biomochi>().campfireInRange);
            biomochi.GetComponent<Biomochi>().Persue(objective);
        }
        else if (typeOfObjective == "agua")
        {

        }
        else if (typeOfObjective == "food" || typeOfObjective == "food_V" || typeOfObjective == "food_C")
        {

        }
    }

    void arrivedAtObjectivePerception(){        
        Debug.Log("LLEGANDO A OBJETIVO" );
        if(llegadaAlObjetivo){
            Debug.Log("EN EL OBJETIVO");
            currentState = 2;            
        }
    }

    void interactionWithObjectivePerception(){
        Debug.Log("INTERACTUANDO CON EL OBJETIVO");
        if(animacionTerminada){
            Debug.Log("HE TERMINADO DE INTERACTUAR");
            exit = true;
        }        
    }
}