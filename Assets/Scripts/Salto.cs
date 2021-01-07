using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Salto : StateMachineBehaviour
{
    float height = .9f;
    float t;
    float y;

    Vector3 a, b;    
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        t = 0f;        
        y = animator.gameObject.GetComponent<Transform>().position.y;
        a = new Vector3 (0, height/24,0);
        b = new Vector3 (0, height/34,0);
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        t+=1f; 
        if(t<24f){
            animator.gameObject.GetComponent<Transform>().position += a;
        }
        if(t>=26f&&t<60f){
            Debug.Log("hola");
            animator.gameObject.GetComponent<Transform>().position -= b;
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.gameObject.GetComponent<Transform>().position = new Vector3(animator.gameObject.GetComponent<Transform>().position.x, 
        y, animator.gameObject.GetComponent<Transform>().position.z);
    }

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}
