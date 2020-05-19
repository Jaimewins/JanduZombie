using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class GotoScript : MonoBehaviour
{
    public SpacePoint[] puntos;
    int currentPoint = 0;
    private NavMeshAgent navMeshAgent;

    [SerializeField] SoundPlayer soundplayer;
    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        Debug.Log("GOTO TO: " + puntos[currentPoint].transform.position);
        navMeshAgent.destination = puntos[currentPoint].transform.position;
    }

    private void Update()
    {
        if (puntos.Length < 1) { return; }

        // Miramos si hemos llegado al punto actual

        if (Vector3.Distance(transform.position, puntos[currentPoint].transform.position) < 0.4f)
        {
            currentPoint++;
            currentPoint %= puntos.Length; // Cuando es 3 si el numero de puntos es 3, el modulo es 0
            navMeshAgent.destination = puntos[currentPoint].transform.position;
            Debug.Log("GOTO TO: " + puntos[currentPoint].transform.position);
            soundplayer.Play(0, 1);
        }
    }
}
