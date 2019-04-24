using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGScroller : MonoBehaviour
{

    public float scrollSpeed;
    public float tileSizeZ;
    public float scrollMultiplier;
    GameObject gameControllerObject;
    GameController gameController;

    private Vector3 startPosition;

    // Start is called before the first frame update
    void Start()
    {
        startPosition = transform.position;
        scrollMultiplier = 1;

        gameControllerObject = GameObject.FindGameObjectWithTag("GameController");
        gameController = gameControllerObject.GetComponent<GameController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (gameController.isSuperBoost == false)
        {
            float newPosition = Mathf.Repeat(Time.time * -scrollSpeed * scrollMultiplier, tileSizeZ);
            transform.position = startPosition + Vector3.forward * newPosition;
        }
    }
}
