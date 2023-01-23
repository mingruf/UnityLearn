using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeRemoveBehavior : StateMachineBehaviour
{
    public float fadeDelay = 0.0f;
    public float fadeTime = 0.5f;
    public float fadeDelayElapse = 0;
    private float timeElapse;
    GameObject gameObject;
    SpriteRenderer spriteRenderer;
    Color startColor;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        timeElapse = 0f;
        gameObject = animator.gameObject;
        spriteRenderer= gameObject.GetComponent<SpriteRenderer>();
        startColor = spriteRenderer.color;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (fadeDelay > fadeDelayElapse)
        {
            fadeDelayElapse += Time.deltaTime;
        }
        else
        {
            timeElapse += Time.deltaTime;
            float newAlpha = startColor.a * (1 - (timeElapse / fadeTime));
            spriteRenderer.color = new Color(startColor.r, startColor.g, startColor.b, newAlpha);
            if (timeElapse > fadeTime)
            {
                Destroy(gameObject);
            }
        }
    }
}
