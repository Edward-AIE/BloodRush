using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public float deadEnemies;
    
    float gameStartTime;
    float gameTime;

    float spawnCooldown;
    int spawnerInt;

    GameObject enemy;

    Transform spawner1;
    Transform spawner2;
    Transform spawner3;
    Transform spawner4;
    Transform spawner5;
    Transform spawner6;
    Transform spawner7;
    Transform spawner8;
    Transform spawner9;
    Transform spawner10;
    
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnCooldown(10, spawnerInt));
        
        gameStartTime = Time.time;

        spawner1 = GameObject.Find("Spawn1").GetComponent<Transform>();
        spawner2 = GameObject.Find("Spawn2").GetComponent<Transform>();
        spawner3 = GameObject.Find("Spawn3").GetComponent<Transform>();
        spawner4 = GameObject.Find("Spawn4").GetComponent<Transform>();
        spawner5 = GameObject.Find("Spawn5").GetComponent<Transform>();
        spawner6 = GameObject.Find("Spawn6").GetComponent<Transform>();
        spawner7 = GameObject.Find("Spawn7").GetComponent<Transform>();
        spawner8 = GameObject.Find("Spawn8").GetComponent<Transform>();
        spawner9 = GameObject.Find("Spawn9").GetComponent<Transform>();
        spawner10 = GameObject.Find("Spawn10").GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        gameTime = Time.time - gameStartTime;
        spawnerInt = Random.Range(1, 10);
    }

    public void EnemyKilled()
    {

    }

    IEnumerator SpawnCooldown(float cooldown, int spawner)
    {
        if (spawner == 1)
        {
            Instantiate(enemy, spawner1.transform.position, spawner1.transform.rotation);
        }
        else if (spawner == 2)
        {
            Instantiate(enemy, spawner2.transform.position, spawner2.transform.rotation);
        }
        else if (spawner == 3)
        {
            Instantiate(enemy, spawner3.transform.position, spawner3.transform.rotation);
        }
        else if (spawner == 4)
        {
            Instantiate(enemy, spawner4.transform.position, spawner4.transform.rotation);
        }
        else if (spawner == 5)
        {
            Instantiate(enemy, spawner5.transform.position, spawner5.transform.rotation);
        }
        else if (spawner == 6)
        {
            Instantiate(enemy, spawner6.transform.position, spawner6.transform.rotation);
        }
        else if (spawner == 7)
        {
            Instantiate(enemy, spawner7.transform.position, spawner7.transform.rotation);
        }
        else if (spawner == 8)
        {
            Instantiate(enemy, spawner8.transform.position, spawner8.transform.rotation);
        }
        else if (spawner == 9)
        {
            Instantiate(enemy, spawner9.transform.position, spawner9.transform.rotation);
        }
        else if (spawner == 10)
        {
            Instantiate(enemy, spawner10.transform.position, spawner10.transform.rotation);
        }

        yield return new WaitForSeconds(cooldown);
        StartCoroutine(SpawnCooldown(spawnCooldown, spawnerInt));
    }
    IEnumerator RaiseSpeed()
    {
        yield return new WaitForSeconds(10);

        if (spawnCooldown >= 1)
        {
            spawnCooldown -= 1;
        }
        else
        {
            spawnCooldown = 1;
        }
        StartCoroutine(RaiseSpeed());
    }
}
