using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimateButton : MonoBehaviour
{
    [SerializeField] private Animator animator;
    string currentState;

    // Start is called before the first frame update
    void awake()
    {
        animator = GetComponent<Animator>();
    }

    public Animator getAnimator()
    {
        return animator;
    }

    public void changeAnimationState(string newState)
    {
        if (currentState == newState) return;
        animator.Play(newState);
        currentState = newState;
    }
}