using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NewBT : MonoBehaviour {

    #region variables
    
    [SerializeField] bool llegadaAlObjetivo;

    [SerializeField] bool animacionTerminada;

    private string typeOfObjective ;
    private bool machineRunning;

    [SerializeField] bool esZombie;
    [SerializeField] bool tieneHambre; 
    [SerializeField] bool esCultista;
    public bool diosInteractua;

    [SerializeField] bool ClimaChungo;

    [SerializeField] bool esAdulto;

    [SerializeField] bool esSocial;

    [SerializeField] bool tenacidad;
    private BehaviourTreeEngine NewBT_BT;
    public CustomNeedMachine needMachine;
    
    private LoopDecoratorNode Loop;
    private SelectorNode Necesidad;
    private SequenceNode SecuenciaZombie;
    private SequenceNode SecuenciaCultismo;
    private SequenceNode SecuenciaRefugiarse;
    private SequenceNode SecuenciaComer;
    private SequenceNode SecuenciaSocializar;
    private SequenceNode SecuenciaReproducción;

    private LeafNode zombieNodeMachine;
    private LeafNode diosNodeMachine;
    private LeafNode refugiarseNodeMachine;
    private LeafNode comerNodeMachine;
    private LeafNode socializarNodeMachine;
    private LeafNode reproducirseNodeMachine;
    
    private LeafNode Zombie;
    private LeafNode DiosInteractua;
    private LeafNode Cultismo;
    private LeafNode NoTenacidad;
    private LeafNode Friocalor;
    private LeafNode Hambre;
    private LeafNode Social;
    private LeafNode Adulto;
    
 

  
  

    //Place your variables here

    #endregion variables

    // Start is called before the first frame update
    private void Start()
    {
        typeOfObjective = "Biomochi";
        NewBT_BT = new BehaviourTreeEngine(false);
        needMachine = new CustomNeedMachine(gameObject);
        CreateBehaviourTree();
        
    }
    
    
    private void CreateBehaviourTree()
    {
        // Nodes
        
        Necesidad = NewBT_BT.CreateSelectorNode("Necesidad");
        Loop = NewBT_BT.CreateLoopNode("LoopDecoratorNode",Necesidad);
        SecuenciaZombie = NewBT_BT.CreateSequenceNode("SecuenciaZombie", false);
        SecuenciaCultismo = NewBT_BT.CreateSequenceNode("SecuenciaCultismo", false);
        SecuenciaRefugiarse = NewBT_BT.CreateSequenceNode("SecuenciaRefugiarse", false);
        SecuenciaComer = NewBT_BT.CreateSequenceNode("SecuenciaComer", false);
        SecuenciaSocializar = NewBT_BT.CreateSequenceNode("SecuenciaSocializar", false);
        SecuenciaReproducción = NewBT_BT.CreateSequenceNode("SecuenciaReproducción", false);
        Zombie = NewBT_BT.CreateLeafNode("Zombie", ZombieAction, ZombieSuccessCheck);
        DiosInteractua = NewBT_BT.CreateLeafNode("Dios Interactua", DiosInteractuaAction, DiosInteractuaSuccessCheck);
        Cultismo = NewBT_BT.CreateLeafNode("Cultismo", CultismoAction, CultismoSuccessCheck);
        NoTenacidad = NewBT_BT.CreateLeafNode("NoTenacidad", NoTenacidadAction, NoTenacidadSuccessCheck);
        Friocalor = NewBT_BT.CreateLeafNode("Frio/calor", FriocalorAction, FriocalorSuccessCheck);
        Hambre = NewBT_BT.CreateLeafNode("Hambre", FriocalorAction, HambreSuccessCheck);
        Social = NewBT_BT.CreateLeafNode("Social", SocialAction, SocialSuccessCheck);
        Adulto = NewBT_BT.CreateLeafNode("Adulto", AdultoAction, AdultoSuccessCheck);
        zombieNodeMachine = NewBT_BT.CreateLeafNode("ZombieFSM",ZombieNodeMachineAction,ZombieNodeMachineSuccesCheck);
        diosNodeMachine = NewBT_BT.CreateLeafNode("DiosFSM",DiosNodeMachineAction,DiosNodeMachineSuccesCheck);
        refugiarseNodeMachine = NewBT_BT.CreateLeafNode("RefugiarseFSM",RefugiarseNodeMachineAction,RefugiarseNodeMachineSuccesCheck);
        comerNodeMachine = NewBT_BT.CreateLeafNode("ComerFSM",ComerNodeMachineAction,ComerNodeMachineSuccesCheck);
        socializarNodeMachine = NewBT_BT.CreateLeafNode("SocializarFSM",SocializarNodeMachineAction,SocializarNodeMachineSuccesCheck);
        reproducirseNodeMachine = NewBT_BT.CreateLeafNode("ReproducirseFSM",ReproducirseNodeMachineAction,ReproducirseNodeMachineSuccesCheck);
        
        // Child adding
        Necesidad.AddChild(SecuenciaZombie);
        Necesidad.AddChild(SecuenciaCultismo);
        Necesidad.AddChild(SecuenciaRefugiarse);
        Necesidad.AddChild(SecuenciaComer);        
        Necesidad.AddChild(SecuenciaReproducción);
        Necesidad.AddChild(SecuenciaSocializar);

        SecuenciaZombie.AddChild(Zombie);
        SecuenciaZombie.AddChild(zombieNodeMachine);
        
        
        SecuenciaCultismo.AddChild(DiosInteractua);
        SecuenciaCultismo.AddChild(Cultismo);
        SecuenciaCultismo.AddChild(diosNodeMachine);
        
        SecuenciaRefugiarse.AddChild(NoTenacidad);
        SecuenciaRefugiarse.AddChild(Friocalor);
        SecuenciaRefugiarse.AddChild(refugiarseNodeMachine);

        SecuenciaComer.AddChild(Hambre);
        SecuenciaComer.AddChild(comerNodeMachine);

        SecuenciaReproducción.AddChild(Adulto);
        SecuenciaReproducción.AddChild(reproducirseNodeMachine);

        SecuenciaSocializar.AddChild(Social);
        SecuenciaSocializar.AddChild(socializarNodeMachine);

        
        // SetRoot
        NewBT_BT.SetRootNode(Loop);
        
        // ExitPerceptions
        
        // ExitTransitions
        
    }

    // Update is called once per frame
    private void Update()
    {
        
        if(!needMachine.exit){
            Debug.Log("MAQUINA EJECUTA");
            needMachine.Update(typeOfObjective);
        }
       
        NewBT_BT.Update();
        
    }

    // Create your desired actions
    
    private void ZombieAction()
    {
        
    }
    
    private ReturnValues ZombieSuccessCheck()
    {
        //Write here the code for the success check for Zombie
        if(gameObject.GetComponent<Biomochi>().isInZombieState)
        {
            Debug.Log("QUIERO COMER BIOMOCHIS");
            typeOfObjective = "Biomochi";
            needMachine.exit = false;
            return ReturnValues.Succeed;
        }
        return ReturnValues.Failed;
    }
    
    private void DiosInteractuaAction()
    {
        
    }
    
    private ReturnValues DiosInteractuaSuccessCheck()
    {
        if(diosInteractua){
            return ReturnValues.Succeed;
            
        }
        //Write here the code for the success check for DiosInteractua
        return ReturnValues.Failed;
    }
    
    private void CultismoAction()
    {
        
    }
    
    private ReturnValues CultismoSuccessCheck()
    {
        if(gameObject.GetComponent<Biomochi>().genes.ContainsKey(Biomochi.Genes.Cultismo))
        {
            Debug.Log("DIOS TE AMO , DAME UN POQUITO DE COMIDA BB");            
            return ReturnValues.Succeed;            
        }
        //Write here the code for the success check for Cultismo
        return ReturnValues.Failed;
    }
    
    private void NoTenacidadAction()
    {
        
    }
    
    private ReturnValues NoTenacidadSuccessCheck()
    {
        if(!gameObject.GetComponent<Biomochi>().genes.ContainsKey(Biomochi.Genes.Tenacidad))
        {
            Debug.Log("soy poderoso");
            return ReturnValues.Succeed;
        }
        //Write here the code for the success check for NoTenacida
        return ReturnValues.Failed;
    }
    
    private void FriocalorAction()
    {
        
    }
    
    private ReturnValues FriocalorSuccessCheck()
    {
        if (GameObject.FindGameObjectWithTag("world").GetComponent<World>().climate < 2)
        {
            if (GameObject.FindGameObjectWithTag("world").GetComponent<World>().climate == 0){ //frio
                typeOfObjective = "hoguera";
            }
            if(GameObject.FindGameObjectWithTag("world").GetComponent<World>().climate == 1){ //calor
                typeOfObjective = "agua";
            }
            Debug.Log("ME VOY A REFUGIAR QUE ME MATO");
            needMachine.exit = false;
            return ReturnValues.Succeed;
        }
        //Write here the code for the success check for Friocalor
        return ReturnValues.Failed;
    }
    
        private void SocialAction()
    {
        
    }
    
    private ReturnValues SocialSuccessCheck()
    {
        if (gameObject.GetComponent<Biomochi>().genes.ContainsKey(Biomochi.Genes.Social) && Random.Range(0f, 1f) >= (float)gameObject.GetComponent<Biomochi>().genes[Biomochi.Genes.Social])
        {
            Debug.Log("BUSCO A UN AMIGO");
            typeOfObjective = "Biomochi";
            needMachine.exit = false;
            return ReturnValues.Succeed;
        }
        //Write here the code for the success check for Friocalor
        return ReturnValues.Failed;
    }
        private void HambreAction()
    {
        
    }
    
    private ReturnValues HambreSuccessCheck()
    {
        if (gameObject.GetComponent<Biomochi>().hambre >= gameObject.GetComponent<Biomochi>().gloton-1)
        {
           
            typeOfObjective="food";            
            needMachine.exit = false;                    
            Debug.Log("TENGO MUCHISIMO HAMBRE");
            return ReturnValues.Succeed;
        }
        //Write here the code for the success check for Friocalor
        return ReturnValues.Failed;
    }

    private void AdultoAction()
    {
        
    }

    private ReturnValues AdultoSuccessCheck()
    {
        if (gameObject.GetComponent<Biomochi>().isInSexyState)
        {
            if(gameObject.GetComponent<Biomochi>())
            typeOfObjective = "Biomochi";
            needMachine.exit = false;
            Debug.Log("MODO SEXO ACTIVADO");
            return ReturnValues.Succeed;
        }
        //Write here the code for the success check for Friocalor
        return ReturnValues.Failed;
    }

    private void ZombieNodeMachineAction(){
        
    }

    private ReturnValues  SuccessCheck(){
        if(needMachine.exit){
            needMachine.currentState = 0;                      
            return ReturnValues.Succeed;
        }

        return ReturnValues.Failed;
    }
    private ReturnValues ZombieNodeMachineSuccesCheck(){
        return SuccessCheck();
    }
     private void DiosNodeMachineAction(){

        gameObject.GetComponent<Animator>().SetTrigger("Dios");

    }

    private ReturnValues  DiosNodeMachineSuccesCheck(){
        return SuccessCheck();
    }
     private void RefugiarseNodeMachineAction(){
        
    }

    private ReturnValues  RefugiarseNodeMachineSuccesCheck(){
        return SuccessCheck();
    }
     private void ComerNodeMachineAction(){
        
    }

    private ReturnValues  ComerNodeMachineSuccesCheck(){
        return SuccessCheck();       
    }
    
    private void SocializarNodeMachineAction(){
        
    }
    
    private ReturnValues  SocializarNodeMachineSuccesCheck(){
        return SuccessCheck();
    }
    
    private void ReproducirseNodeMachineAction(){
        
    }

    private ReturnValues  ReproducirseNodeMachineSuccesCheck(){
        return SuccessCheck();
    }
}