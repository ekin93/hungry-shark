using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public SoundManager soundManager;
    public float playerSpeed = 10;
    public Text gameOverText;

    private SpriteRenderer rend;
    private Animator animator;
    private string currentAnimation;
    private bool isDead = false;
    private bool canMove = false;
    private float maxSpeed = 15;

    void Start()
    {
        rend = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        currentAnimation = "IDLE_Shark";
        gameOverText.enabled = false;
    }

    void Update()
    {
        if (!isDead)
        {
            MovePlayer();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "EnemyFish")
        {
            if (playerSpeed < maxSpeed)
            {
                playerSpeed += 0.5f;
            }
            PlayAnimation("BITE_Shark");
            soundManager.PlayEatSound();
            ToggleControls(false);
        }
        else if (collision.transform.tag == "Blowfish")
        {
            soundManager.PlayDieSound();
            rend.color = new Color(1, 1, 0);
            PlayAnimation("BITE_Shark");
            ToggleControls(false);
        }
    }

    private void MovePlayer()
    {
        if (canMove)
        {
            Vector3 moveVector = Vector3.zero;

            if (Input.GetKey(KeyCode.UpArrow))
            {
                moveVector += new Vector3(0, 0, playerSpeed * Time.deltaTime);
            }
            if (Input.GetKey(KeyCode.DownArrow))
            {
                moveVector += new Vector3(0, 0, -playerSpeed * Time.deltaTime);
            }
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                moveVector += new Vector3(-playerSpeed * Time.deltaTime, 0, 0);
            }
            if (Input.GetKey(KeyCode.RightArrow))
            {
                moveVector += new Vector3(playerSpeed * Time.deltaTime, 0, 0);
            }

            transform.position += moveVector;
            if (PlayerMoveRaycast() != "PlayerMoveSpace")
            {
                transform.position -= moveVector;
            }
            if (moveVector != Vector3.zero)
            {
                PlayAnimation("SWIM_Shark");
                if (moveVector.x > 0)
                {
                    transform.localScale = new Vector3(1, 1, 1);
                }
                else if (moveVector.x < 0)
                {
                    transform.localScale = new Vector3(-1, 1, 1);
                }
            }
            else
            {
                PlayAnimation("IDLE_Shark");
            }
        }
    }

    private string PlayerMoveRaycast()
    {
        string tag;
        RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.down, out hit, 10))
        {
            tag = hit.transform.tag;
        }
        else
        {
            tag = "Nothing";
        }
        return tag;
    }

    private void PlayAnimation(string anim)
    {
        if (currentAnimation != anim)
        {
            animator.Play(anim);
            currentAnimation = anim;
        }
    }

    public void Die()
    {
        isDead = true;
        GameObject enemySpawner = GameObject.FindGameObjectWithTag("EnemySpawner");
        enemySpawner.GetComponent<EnemySpawner>().StopSpawning();
        gameOverText.enabled = true;
        Invoke("ReloadGame", 3);
    }

    public void ToggleControls(bool flag)
    {
        canMove = flag;
    }

    public void OnEndBiteAnimation()
    {
        ToggleControls(true);
    }

    public void ReloadGame()
    {
        SceneManager.LoadScene(0);
    }
}
