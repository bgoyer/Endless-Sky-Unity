using UnityEngine;

public class AI_Explode : StateMachineBehaviour
{
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.transform.GetChild(0).GetComponent<Animator>().SetBool("Dead", true);
        //animator.transform.GetChild(0).GetComponent<SpriteRenderer>().enabled = false;
        Destroy(animator.gameObject, 1.5f);
    }
}