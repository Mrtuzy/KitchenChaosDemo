using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorControl : MonoBehaviour
{
    private Animator animator;
    [SerializeField] private Player player;
    private const string IS_WALKING = "IsWalking";
    private void Awake()
    {
        animator = GetComponent<Animator>();
    }


   private void Update()
    {
        animator.SetBool(IS_WALKING, player.IsWalking());
    }
}
