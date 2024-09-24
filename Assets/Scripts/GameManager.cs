using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public Movement player;
    public ParticleSystem explosion;
    public TextMeshProUGUI scoreText;
    public AsteroidSpawner BuffSpawn;
    public GameObject gameOverPanel;

    public float respawnTime = 3.0f;
    public float ImmuneTime = 3.0f;
    public int lives = 3;
    public float gameOverDelay = 2.0f;

    public int score = 0;

    private bool specialAsteroidSpawned = false;
    private bool totallySpecialAsteroidSpawned = false;
    
    private void Start(){
        UpdateScoreText();
    }

    public void AsteroidDestroyed(Asteroid asteroid){
        this.explosion.transform.position = asteroid.transform.position;
        this.explosion.Play();

        if(asteroid.size < 0.75f){
            this.score += 100;
        }else if(asteroid.size < 0.12f){
            this.score += 50;
        }else{
            this.score += 25;
        }

        UpdateScoreText();

        if (this.score > 4000 && !specialAsteroidSpawned){
            BuffSpawn.SpawnSpecialAsteroid();
            specialAsteroidSpawned = true;
        }else if(this.score > 8000 && !totallySpecialAsteroidSpawned){
            BuffSpawn.SpawnTotallySpecialAsteroid();
            totallySpecialAsteroidSpawned = true;
        }
    }

    public void PlayerDied(){
        this.explosion.transform.position = this.player.transform.position;
        this.explosion.Play();

        this.lives--;
        if(this.lives <= 0){
            StartCoroutine(GameOverCoroutine());
        }else{
            Invoke(nameof(Respawn), this.respawnTime);
        }
    }

    private void Respawn(){
        this.player.transform.position = Vector3.zero;
        this.player.gameObject.layer = LayerMask.NameToLayer("IgnoreCollision");
        this.player.gameObject.SetActive(true);
        Invoke(nameof(TurnOnCollisions),this.ImmuneTime);
    }

    private void TurnOnCollisions(){
        this.player.gameObject.layer = LayerMask.NameToLayer("Player");

    }

    private IEnumerator GameOverCoroutine(){
        BuffSpawn.StopSpawning();

        this.player.gameObject.SetActive(false);

        yield return new WaitForSeconds(gameOverDelay);

        gameOverPanel.SetActive(true);
        Time.timeScale = 0f;
    }

    private void UpdateScoreText(){
        scoreText.text = "Score: " + score.ToString(); 
    }

    public static GameManager Instance { get; private set; }
    private void Awake()
    {
        if (Instance == null){
            Instance = this;
            DontDestroyOnLoad(gameObject); 
        }
        else{
            Destroy(gameObject); 
        }
    }
}
