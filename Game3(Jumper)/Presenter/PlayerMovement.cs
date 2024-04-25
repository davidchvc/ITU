/*
 * Name = PlayerMovement.cs
 * Functionality = gives new coordinates to the PlayerObject based on user input
 * Author = xchova25
 */
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private bool Moving = false;
    
    private float speed;
    GameObject destination;
    private int playerID;
    private void Start()
    {
        /*
         * slow = 1000
         * mid = 1500
         * fast = 2000
         */
        /* before CRUD */
        playerID = 0;
        speed = 2000;
        gameObject.GetComponent<TrailRenderer>().widthMultiplier = 40;
    }
    public void MoveTo(GameObject platform)
    {
        /* dont move if you are moving already */
        destination = platform;
        Moving = true;
    }
    void Update()
    {
        if (!Moving)
            return;

        float step = speed * Time.deltaTime;
        var x = destination.transform.position.x;
        var y = destination.transform.position.y;

        transform.position = Vector3.MoveTowards(transform.position, new Vector3(x,y + 50, 0), step);
        if(Vector3.Distance(transform.position, new Vector3(x, y + 50, 0)) < 0.25f)
            StopMovement();
    }
    private void StopMovement()
    {
        Moving = false;
        GameObject.Find("Presenter").GetComponent<JumperLogic>().ReceiveLanding(destination, playerID);
    }

}
