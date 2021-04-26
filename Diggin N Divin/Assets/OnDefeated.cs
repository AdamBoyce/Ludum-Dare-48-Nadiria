using UnityEngine;
using UnityEngine.SceneManagement;

public class OnDefeated : StateMachineBehaviour
{
    private const float _defeatOffset = 0.74f;

    private float AnimationEnd;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        AnimationEnd = Time.time + _defeatOffset;
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if(Time.time >= AnimationEnd)
            SceneManager.LoadScene(1);
    }
 }