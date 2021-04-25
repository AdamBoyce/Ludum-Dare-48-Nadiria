using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OnDefeated : StateMachineBehaviour
{
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //PLAY DEFEAT NOISE - THEN MOVE ON!
        SceneManager.LoadScene(1);
    }
 }