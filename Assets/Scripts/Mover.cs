using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mover : MonoBehaviour
{
    public float speed;
    GameObject gameControllerObject;
    GameController gameController;

    private Rigidbody rb;

    void Start()
    {

        gameControllerObject = GameObject.FindGameObjectWithTag("GameController");
        gameController = gameControllerObject.GetComponent<GameController>();

        rb = GetComponent<Rigidbody>();

        rb.velocity = transform.forward * speed;
        
    }

    private void Update()
    {
        if (gameController.isSuperBoost == true && this.tag == "Enemy")
        {
            rb.constraints = RigidbodyConstraints.FreezeAll;
        }
        else if (rb.velocity == Vector3.zero)
        {
            rb.constraints = RigidbodyConstraints.None;
            rb.velocity = transform.forward * speed;
        }
    }
}