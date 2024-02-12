using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContainerCounterAnimation : MonoBehaviour
{
    [SerializeField] private ContainerCounter containerCounter;
    private Animator animator;
    private const string OPEN_CLOSE = "OpenClose";
    private void Awake()
    {
        animator = GetComponent<Animator>();
    }
    private void Start()
    {
        containerCounter.OnPlayerGrabbedObject += CountainerCounter_OnPlayerGrabbedObject;
    }
    private void CountainerCounter_OnPlayerGrabbedObject(object sender, EventArgs e)
    {
        animator.SetTrigger(OPEN_CLOSE);
    }

}
