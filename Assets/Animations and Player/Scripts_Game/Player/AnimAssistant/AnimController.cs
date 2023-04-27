using System.Collections;
using UnityEngine;

public class AnimController : MonoBehaviour
{
    // Animator component of the game object
    private Animator animator;
    // Parameter name for the idle integer in the Animator controller
    private const string idleIntParam = "WIdel";
    // Parameter name for the idle boolean in the Animator controller
    private const string idleBoolParam = "Idle";
    // Parameter name for the speaking boolean in the Animator controller
    private const string speakingBoolParam = "Speaking";
    // The number of the next idle animation to play
    private int nextIdleAnimNumber = 0;
    // Whether the current idle animation has finished playing
    private bool idleAnimFinished = true;

    void Start()
    {
        // Get the Animator component on Start
        animator = GetComponent<Animator>();
        // Set the Idle boolean to true to start playing idle animations
        animator.SetBool(idleBoolParam, true);
    }

    void FixedUpdate()
    {
        // If the player is speaking or moving, set the Idle boolean to false to stop playing idle animations
        if (animator.GetBool(speakingBoolParam))
        {
            animator.SetBool(idleBoolParam, false);
        }
        else
        {
            if (idleAnimFinished)
            {
                StartCoroutine(PlayIdleAnim(new WaitForSeconds(10f)));
                idleAnimFinished = false;
            }
        }
    }

    IEnumerator PlayIdleAnim(WaitForSeconds waitTime)
    {
        // Check if the Idle boolean is true
        if (animator.GetBool(idleBoolParam))
        {
            // Wait for the specified time
            yield return waitTime;

            // Set the next idle animation to play
            animator.SetInteger(idleIntParam, nextIdleAnimNumber);

            // Wait until the idle animation finishes playing before changing the next idle animation
            while (animator.GetCurrentAnimatorStateInfo(0).IsName("WIdel_" + nextIdleAnimNumber))
            {
                yield return null;
            }

            // Increment the next idle animation number
            nextIdleAnimNumber = (nextIdleAnimNumber + 1) % 3;

            // Set the idle animation finished flag to true
            idleAnimFinished = true;
        }
    }
}
