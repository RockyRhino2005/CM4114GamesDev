using UnityEngine;
using StarterAssets;

public class AttackRootMotion : StateMachineBehaviour
{
    private CharacterController characterController;
    private ThirdPersonController thirdPersonController;

    public override void OnStateEnter(
        Animator animator,
        AnimatorStateInfo stateInfo,
        int layerIndex)
    {
        animator.applyRootMotion = true;
        characterController =
            animator.GetComponentInParent<CharacterController>();

        thirdPersonController =
            animator.GetComponentInParent<ThirdPersonController>();

        // Temporarily disable normal movement
        if (thirdPersonController != null)
        {
            thirdPersonController.enabled = false;
        }
    }

    public override void OnStateExit(
        Animator animator,
        AnimatorStateInfo stateInfo,
        int layerIndex)
    {
        animator.applyRootMotion = false;
        // Restore normal movement
        if (thirdPersonController != null)
        {
            thirdPersonController.enabled = true;
        }
    }
}