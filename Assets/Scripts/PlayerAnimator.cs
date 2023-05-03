using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{

    [SerializeField] private Player player;

    private Animator animator;
    private const string IS_RUNNING = "IsRunning";

    // Start is called before the first frame update
    private void Awake()
    {
        animator = GetComponent<Animator>();
        animator.SetBool( IS_RUNNING, player.IsRunning() );
    }

    // Update is called once per frame
    void Update()
    {
        animator.SetBool( IS_RUNNING, player.IsRunning() );
    }
}
