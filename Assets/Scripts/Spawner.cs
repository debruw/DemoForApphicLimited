using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject energyCoin; // Game object for spawning in game
    public int energyCoinCount; // Object counter for energy coin on game
    public Transform player; // Player transform for positioning new objects

    private void Update()
    {
        // if Energy coin count smaller than 3 Instantiate another energy coin and increase "energyCoinCount"
        if (energyCoinCount < 3)
        {
            Instantiate(energyCoin, new Vector3(Random.Range(-3, 3), 1, Random.Range(player.position.z + 10, player.position.z + 20)), energyCoin.transform.rotation);
            energyCoinCount++;
        }
    }
}
