using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Boundary
{
    public float xMin, xMax, yMin, yMax;
}

public class PlayerController : MonoBehaviour
{
    public float moveSpeed, jumpSpeed; // Player move speed and jump speed variables
    public Boundary boundary; // Boundry variables for player to not leave screen

    private Vector3 firstTouchPos;   // First touch position
    private Vector3 lastTouchPos;   // Last touch position
    private float dragDistance;  // Minimum distance for a swipe to be registered

    // Start is called before the first frame update
    void Start()
    {
        dragDistance = Screen.height * .15f; // Set dragDistance is 15% height of the screen
    }

    // Update is called once per frame
    void Update()
    {// Control is there a touch on screen
        if (Input.touchCount > 0)
        {
            var touch = Input.GetTouch(0); // Getting touch variables
            
            if (touch.phase == TouchPhase.Began) //check for the first touch
            {// Set first and last touches if touch began
                firstTouchPos = touch.position;
                lastTouchPos = touch.position;
            }
            else if (touch.phase == TouchPhase.Moved) // update the last position based on where they moved
            {// Set the last touch if touch moved
                lastTouchPos = touch.position;
            }
            else if (touch.phase == TouchPhase.Ended) //check if the finger is removed from the screen
            {// Set the last touch if touch end
                lastTouchPos = touch.position;  //last touch position. Ommitted if you use list

                //Check if drag distance is greater than 20% of the screen height for drag detect
                if (Mathf.Abs(lastTouchPos.y - firstTouchPos.y) > dragDistance)
                {//It's a drag
                    //Check if vertical movement grater than horizontal movement
                    if (Mathf.Abs(lastTouchPos.x - firstTouchPos.x) < Mathf.Abs(lastTouchPos.y - firstTouchPos.y))
                    {// İts a vertical swipe
                        // Check if its a up or down swipe
                        if (lastTouchPos.y > firstTouchPos.y)
                        {   // İts a Up swipe
                            Debug.Log("Up Swipe");
                            Jump(); // Make Player Jump
                        }
                    }
                }
            }
            else if(touch.phase == TouchPhase.Stationary) //check if the finger is still touching the screen witout move
            {
                // Check the touch position 
                if (touch.position.x < Screen.width / 2)
                {// Touch position is on the left side of the screen
                    Debug.Log("Left side");
                    // MovePlayer to the left
                    transform.position = Vector3.MoveTowards(transform.position, new Vector3(transform.position.x - moveSpeed, transform.position.y, transform.position.z), moveSpeed);
                }
                else if (touch.position.x > Screen.width / 2)
                {// Touch position is on the right side of the screen
                    Debug.Log("Right side");
                    // Move Player to the right
                    transform.position = Vector3.MoveTowards(transform.position, new Vector3(transform.position.x + moveSpeed, transform.position.y, transform.position.z), moveSpeed);
                }
            }
        }

        // Clamp Player x and y position depends on boundry variables
        // And make player z position move forward
        transform.position = new Vector3
            (
                Mathf.Clamp(transform.position.x, boundary.xMin, boundary.xMax),
                Mathf.Clamp(transform.position.y, boundary.yMin, boundary.yMax),
                transform.position.z + moveSpeed
            );
    }

    /// <summary>
    /// İncrease Players velocity for jumping
    /// </summary>
    void Jump()
    {
        GetComponent<Rigidbody>().velocity = Vector3.up * jumpSpeed;
    }
}
