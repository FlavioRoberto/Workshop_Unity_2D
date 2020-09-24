using Assets.Extensions;
using UnityEngine;

public class AnimationDeath : StateMachineBehaviour
{
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.gameObject.DesabilitarColisoresFilhos();
        Destroy(animator.gameObject, stateInfo.length);
    }
}
