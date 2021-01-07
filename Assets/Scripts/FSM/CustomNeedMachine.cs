
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

class CustomNeedMachine{

    

    public int currentState;
    bool objetivoDetectado;
    bool llegadaAlObjetivo;

    
    public bool exit;
    
    bool animacionTerminada;

    public CustomNeedMachine(){
        currentState = 0;
        exit = true;
    }

    

    public void Update(string typeOfObjective,bool animacionTerminada,bool objetivoDetectado, bool llegadaAlObjetivo){
        this.animacionTerminada = animacionTerminada;
        this.llegadaAlObjetivo = llegadaAlObjetivo;
        this.objetivoDetectado = objetivoDetectado;

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
        if(objetivoDetectado ){
            Debug.Log(typeOfObjective + "ENCONTRADO");
            currentState = 1;
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