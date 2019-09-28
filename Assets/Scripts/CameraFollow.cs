using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target; // Target object to follow

    public int offsetZ; // Offset between Camera and target object

    private void Start()
    {
        // Setting music Mute/Unmute depends on PlayerPrefs
        if (PlayerPrefs.GetInt("ISMUSICPLAYING") == 1)
        {// Set Unmute
            GetComponent<AudioSource>().mute = false;
        }
        else
        {// Set Mute
            GetComponent<AudioSource>().mute = true;
        }
    }

    private void FixedUpdate()
    {
        // Setting Camera position for follow the target object
        transform.position = new Vector3(transform.position.x, transform.position.y, target.position.z + offsetZ);
    }
}
