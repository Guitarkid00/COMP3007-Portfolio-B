using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour {

    public Player player;
    public GameObject enemyPrefab;
    public float enemySpawnInterval = 1f;
    public float horizontalLimit = 2.8f;
    public float fuelDecreaseSpeed = 5f;

    public GameObject fuelPrefab;
    public float fuelSpawnInterval = 9f;
    private float fuelSpawnTimer;

    private float enemySpawnTimer;
    public GameObject gameCamera;

    //UI Variables
    private int score;
    private float fuel = 100f;
    public Text scoreText;
    public Text fuelText;

	// Use this for initialization
	void Start () {

        enemySpawnTimer = enemySpawnInterval;

        player.OnFuel += OnFuel;
        fuelSpawnTimer = Random.Range(0f, fuelSpawnInterval);
	}
	
	// Update is called once per frame
	void Update ()
    {
		
        if(player != null)
        {
            enemySpawnTimer -= Time.deltaTime;
            if (enemySpawnTimer <= 0)
            {
                enemySpawnTimer = enemySpawnInterval;

                GameObject enemyInstance = Instantiate(enemyPrefab);
                enemyInstance.transform.SetParent(transform);
                enemyInstance.transform.position = new Vector2(Random.Range(-horizontalLimit, horizontalLimit), player.transform.position.y + Screen.height / 100f);
                enemyInstance.GetComponent<Enemy>().OnKill += OnEnemyKill;
            }
        }

        foreach(Enemy enemy in GetComponentsInChildren<Enemy>())
        {
            if(enemy != null)
            {
                if(gameCamera.transform.position.y - enemy.transform.position.y > Screen.height / 100f)
                {
                    Destroy(enemy.gameObject);
                }
            }
        }


        fuelSpawnTimer -= Time.deltaTime;
        if(fuelSpawnTimer <= 0)
        {
            fuelSpawnTimer = fuelSpawnInterval;

            GameObject fuelInstance = Instantiate(fuelPrefab);
            fuelInstance.transform.SetParent(transform);
            fuelInstance.transform.position = new Vector2(Random.Range(-horizontalLimit, horizontalLimit), player.transform.position.y + Screen.height / 100f);
        }


        fuel -= Time.deltaTime * fuelDecreaseSpeed;
        fuelText.text = "Fuel: " + (int)fuel;
        if (fuel < 0f)
        {
            fuelText.text = "Fuel: 0";
            Destroy(player.gameObject);
        }
    }

    void OnEnemyKill()
    {
        score += 25;
        scoreText.text = "Score: " + score;
    }

    void OnFuel()
    {
        fuel = 100f;
    }
}
