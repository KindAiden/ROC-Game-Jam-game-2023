using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.Callbacks;
using UnityEngine;

public class Movement : MonoBehaviour

{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
      if(Input.GetKey(KeyCode.A))
      {
        rb.AddForce(Vector2.left);
        transform.position += Vector3.left*1.0*fTime.deltaTime;
      }
      if(Input.GetKey(KeyCode.D))
      {
        rb.AddForce(Vector2.right);
        transform.position += Vector3.right*1.0*fTime.deltaTime;
      }
      if(Input.GetKey(KeyCode.Space))
      {
        transform.position += Vector3.up*5.0*fTime.deltaTime;
      }

      Ray2D ray = new Ray2D(transform.position, Vector2.down);

      RaycastHit2D hit = Physics2D.Raycast(transform.position, -Vector2.up);
      float distanceToHitObject;
      if(hit.collider != null)
      {
        distanceToHitObject = Vector2.Distance(transform.position, hit.collider.transform.position);
        if(distanceToHitObject < 0.2f)
        {
           Debug.Log(hit.collider.gameObject.name);
        }


      }

    }
}