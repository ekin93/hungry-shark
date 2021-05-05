using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialManager : MonoBehaviour
{
    public Text text1;
    public Text text2;
    public Text text3;
    public Text text4;
    public PlayerController player;
    public EnemySpawner enemySpawner;
    public bool atEatingTutorial = true;

    private bool atControlTutorial = false;
    private bool tutorialOver = false;

    // Start is called before the first frame update
    void Start()
    {
        text1.enabled = true;
        text2.enabled = false;
        text3.enabled = false;
        text4.enabled = false;
        Invoke("ControlsTutorial", 3);
    }

    // Update is called once per frame
    void Update()
    {
        if (!tutorialOver)
        {
            if (atControlTutorial && Input.GetKey(KeyCode.RightArrow))
            {
                atControlTutorial = false;
                EatingTutorial();
            }
            if (!atEatingTutorial && Input.GetKey(KeyCode.Space))
            {
                text1.enabled = false;
                text2.enabled = false;
                text3.enabled = false;
                text4.enabled = false;
                tutorialOver = true;
                enemySpawner.StartSpawning();
            }
        }
    }

    public void ControlsTutorial()
    {
        atControlTutorial = true;
        text1.enabled = false;
        text2.enabled = true;
        text3.enabled = false;
        text4.enabled = false;
        player.ToggleControls(true);
    }

    public void EatingTutorial()
    {
        text1.enabled = false;
        text2.enabled = false;
        text3.enabled = true;
        text4.enabled = false;
        enemySpawner.StartTutorialSpawn();
    }

    public void EndEatingTutorial()
    {
        text1.enabled = false;
        text2.enabled = false;
        text3.enabled = false;
        text4.enabled = true;
        atEatingTutorial = false;
        enemySpawner.EndTutorialSpawn();
    }
}
