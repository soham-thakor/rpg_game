using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy_Chase : StateMachineBehaviour
{
    public float speed = 1.0f;
    public float attackRange = .01f;
    public float chaseRange = .5f;

    private bool near=false;
    private float offset;
    private Transform player;
    private Rigidbody2D rb;
    private Enemy enemy;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
       player =  GameObject.FindGameObjectWithTag("Player").transform;
       rb = animator.GetComponent<Rigidbody2D>();
    //    enemy = animator.GameObject.GetComponent<Enemy>();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

        if(Vector2.Distance(player.position, rb.position) <= chaseRange)
        {
            near=true;
        }
        // calculate offset of player position (so enemy doesnt stand inside of player)
        if(player.position.x > 0) { offset = 0.2f; }
        else { offset = -0.2f; }
        
        if(near){
            Vector2 target = new Vector2(player.position.x+offset, player.position.y);

            Vector2 newPos = Vector2.MoveTowards(rb.position, target, speed * Time.fixedDeltaTime);

            rb.MovePosition(newPos);
        }

        // if(Vector2.Distance(player.position, rb.position) <= attackRange)
        // {
        //     animator.SetTrigger("Attack");
        // }
        
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        
    }
}
