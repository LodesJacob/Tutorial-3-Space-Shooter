using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public GameObject[] hazards;
    public Vector3 spawnValues;
    public int hazardCount;
    public float spawnWait;
    public float startWait;
    public float waveWait;
    public AudioClip musicGeneric;
    public AudioClip musicWin;
    public AudioClip musicLose;
    AudioSource audio;
    GameObject background;
    BGScroller backgroundScript;
    GameObject starfieldNear;
    particleSpeed particleNearSpeed;
    GameObject particleFar;
    particleSpeed particleFarSpeed;
    GameObject[] enemy;
    Mover enemySpeed;

    public Text scoreText;
    public Text restartText;
    public Text gameOverText;
    public Text creditText;
    public Text superBoostText;
    public bool superBoost;
    public bool isSuperBoost;

    private float timeLeft = 8;

    private bool gameOver;
    private bool restart;
    private int score;
    private int i;

    private GameObject[] enemyBolt;
    private GameObject[] enemies;

    void Start()
    {
        score = 0;
        UpdateScore();
        StartCoroutine(SpawnWaves());
        restartText.text = "";
        gameOverText.text = "";
        creditText.text = "";
        superBoostText.text = "";

        background = GameObject.FindGameObjectWithTag("Background");
        backgroundScript = background.GetComponent<BGScroller>();
        starfieldNear = GameObject.FindGameObjectWithTag("Starfield");
        particleNearSpeed = starfieldNear.GetComponent<particleSpeed>();
        particleFar = GameObject.FindGameObjectWithTag("StarfieldDistant");
        particleFarSpeed = particleFar.GetComponent<particleSpeed>();

        audio = gameObject.GetComponent<AudioSource>();
        audio.clip = musicGeneric;
        audio.Play();

        superBoost = false;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
        if (restart)
        {
            if (Input.GetKeyDown (KeyCode.Space))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
        }
        if (score >= 30 && superBoost == false && Input.GetKeyDown(KeyCode.E)) {
            useSuperBoost();
        }

        timeLeft -= Time.deltaTime;
        if (isSuperBoost == true)
        {
            if (timeLeft > 2)
            {
                superBoostText.text = "3";
            }
            else if (timeLeft > 1)
            {
                superBoostText.text = "2";
            }
            else if (timeLeft > 0)
            {
                superBoostText.text = "1";
            }
            if (timeLeft < 0)
            {
                isSuperBoost = false;
                superBoostText.text = "";
            }
        }
    }

    IEnumerator SpawnWaves()
    {
        yield return new WaitForSeconds(startWait);
        while (true)
        {
            for (int i = 0; i < hazardCount; i++)
            {
                GameObject hazard = hazards[Random.Range(0, hazards.Length)];
                Vector3 spawnPosition = new Vector3(Random.Range(-spawnValues.x, spawnValues.x), spawnValues.y, spawnValues.z);
                Quaternion spawnRotation = Quaternion.identity;
                Instantiate(hazard, spawnPosition, spawnRotation);
                yield return new WaitForSeconds(spawnWait);

                if (gameOver)
                {
                    break;
                }
            }
            yield return new WaitForSeconds(waveWait);

            if (gameOver)
            {
                restartText.text = "Press Space to Restart";
                restart = true;
                break;
            }
        }
    }

    private void setCredit()
    {
        creditText.text = "Made by Jacob Lodes";
    }

    public void AddScore(int newScoreValue)
    {
        score += newScoreValue;
        UpdateScore();

        if (score >= 100)
        {
            gameOverText.text = "You Win!";
            setCredit();
            gameOver = true;
            enemies = GameObject.FindGameObjectsWithTag("Enemy");

            for (var i = 0; i < enemies.Length; i++)
            {
                Destroy(enemies[i]);
            }

            backgroundScript.scrollMultiplier = 100;
            particleNearSpeed.multiplier = 100;
            particleFarSpeed.multiplier = 100;
            audio.clip = musicWin;
            audio.Play();
        }
        if (score >= 30 && superBoost == false)
        {
            superBoostText.text = "Press \"E\" to Super Boost!";
        }
    }

    void useSuperBoost()
    {
        enemy = GameObject.FindGameObjectsWithTag("Enemy");
        for (i = 0; i < enemy.Length; i++)
        {
            enemySpeed = enemy[i].GetComponent<Mover>();
        }
        superBoost = true;
        isSuperBoost = true;
        timeLeft = 3;

        enemyBolt = GameObject.FindGameObjectsWithTag("EnemyBolt");

        for (var i = 0; i < enemyBolt.Length; i++)
        {
            Destroy(enemyBolt[i].transform.parent.gameObject);
        }

    }

    void UpdateScore()
    {
        scoreText.text = "Points: " + score;
    }

    public void GameOver()
    {
        audio.clip = musicLose;
        audio.Play();
        gameOverText.text = "Game Over!";
        setCredit();
        gameOver = true;
    }
}
