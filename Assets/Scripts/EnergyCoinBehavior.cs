using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyCoinBehavior : MonoBehaviour
{
    Spawner my_spawner; // Spawner object in game

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Rigidbody>().angularVelocity = new Vector3(0, 3, 0); // Turn the coin on Z axis
        my_spawner = FindObjectOfType<Spawner>();
    }

    private void FixedUpdate()
    {// if this object position z is smaller than main camera's position z, decrease the "energyCoinCount" and destroy this object
        if (transform.position.z < Camera.main.transform.position.z)
        {
            my_spawner.energyCoinCount--;
            Destroy(this.gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {// if collision object is player decrease the "energyCoinCount" and destroy this object
        if (collision.gameObject.tag == "Player")
        {
            my_spawner.energyCoinCount--;
            Destroy(this.gameObject); 
        }
    }
}
