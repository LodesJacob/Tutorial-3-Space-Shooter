using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class particleSpeed : MonoBehaviour
{
    private ParticleSystem ps;
    public int multiplier = 1;
    GameObject gameControllerObject;
    GameController gameController;

    void Start()
    {
        ps = GetComponent<ParticleSystem>();
        gameControllerObject = GameObject.FindGameObjectWithTag("GameController");
        gameController = gameControllerObject.GetComponent<GameController>();
    }

    void Update()
    {
        var main = ps.main;
        if (gameController.isSuperBoost == false)
        {
            main.simulationSpeed = multiplier;
        } else
        {
            main.simulationSpeed = 0;
        }
        
    }
}
