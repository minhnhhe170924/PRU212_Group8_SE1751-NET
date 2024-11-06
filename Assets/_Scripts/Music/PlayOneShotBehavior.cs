using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayOneShotBehavior : StateMachineBehaviour
{
    public AudioClip soundPlay;
    public float volume =1f;
    public bool PlayOnter = true, PlayOnExit = false, PlayAfterDelay = false;
    public float playDelay = 0.25f;
    private float timeSinceEntered = 0;
    private bool hasDelaySoundPlayed  = false;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if(PlayOnter)
        {
            volume= PlayerPrefs.GetFloat("SFXVolumne");
            AudioSource.PlayClipAtPoint(soundPlay,animator.gameObject.transform.position,volume);
        }
        timeSinceEntered = 0f;
        hasDelaySoundPlayed = false;
    }
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if(PlayAfterDelay && !hasDelaySoundPlayed)
        {
             timeSinceEntered += Time.deltaTime;
            if(timeSinceEntered > playDelay)
            {
                volume = PlayerPrefs.GetFloat("SFXVolumne");
                AudioSource.PlayClipAtPoint(soundPlay, animator.gameObject.transform.position, volume);
                hasDelaySoundPlayed = true;
            }
        }
    }
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (PlayOnExit)
        {
            volume = PlayerPrefs.GetFloat("SFXVolumne");
            AudioSource.PlayClipAtPoint(soundPlay, animator.gameObject.transform.position, volume);
        }
    }

}
