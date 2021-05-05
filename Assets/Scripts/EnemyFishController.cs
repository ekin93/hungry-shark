using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFishController : MonoBehaviour
{
    public float moveSpeed = 3;

    private bool startMoving = false;
    private Animator animator;
    private string currentAnimation;

    void Start()
    {
        Destroy(gameObject, 10);
        animator = GetComponent<Animator>();
        currentAnimation = "SWIM_Y_Fish";
    }
    
    void Update()
    {
        MoveFish();
    }

    private void MoveFish()
    {
        if (startMoving)
        {
            if (moveSpeed > 0)
            {
                transform.localScale = new Vector3(1, 1, 1);
            }
            else if (moveSpeed < 0)
            {
                transform.localScale = new Vector3(-1, 1, 1);
            }
            transform.position += new Vector3(moveSpeed * Time.deltaTime, 0, 0);
        }
    }

    public void SetSpeed(float newSpeed)
    {
        moveSpeed = newSpeed;
        startMoving = true;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "Player")
        {
            PlayAnimation("DEATH_Y_Fish");
            SetSpeed(0);
            TutorialManager tutorialManager = GameObject.FindGameObjectWithTag("TutorialManager").GetComponent<TutorialManager>();
            if (tutorialManager.atEatingTutorial)
            {
                tutorialManager.EndEatingTutorial();
            }
        }
    }

    private void PlayAnimation(string anim)
    {
        if (currentAnimation != anim)
        {
            animator.Play(anim);
            currentAnimation = anim;
        }
    }

    public void OnDeathAnimationEnd()
    {
        Destroy(gameObject);
    }
}
