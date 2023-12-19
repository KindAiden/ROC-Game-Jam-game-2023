using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CameraBehavior : MonoBehaviour
{
    public Transform Player;
    public Vector3 offset = new Vector3(2, 2, -10);
    public float loadDistance = 20;
    public List<GameObject> objects;
    public Vector3 velocity;

    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.FindWithTag("Player").transform;
        objects = GameObject.FindGameObjectsWithTag("enemy").ToList();
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.paused)
            return;

        Vector3 centerPoint = new Vector3(Player.position.x, Player.position.y, 0);
        Vector3 newPosition = centerPoint + offset;
        transform.position = Vector3.SmoothDamp(transform.position, newPosition, ref velocity, 0.5f);

        //load objects that come close to the camera
        foreach (GameObject obj in objects)
        {
            // Check the distance between the object and the camera
            float distanceToCamera = Vector2.Distance(obj.transform.position, transform.position);

            // Activate or deactivate based on the distance
            if (distanceToCamera < loadDistance)
                obj.SetActive(true);
            else
                obj.SetActive(false);
        }

        if (transform.position.y - Player.position.y > 8)
        {
            GameManager.GameOver();
        }
    }
}
