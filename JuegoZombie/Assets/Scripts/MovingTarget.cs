using UnityEngine;

public class MovingTarget : MonoBehaviour
{
    public SpacePoint[] puntos;
    int currentPoint = 0;
    public float speed = 5;


    void Update()
    {
        if (puntos.Length < 2) { Debug.LogError("Pon los puntos del recorrido"); return;  }

        //Miramos si hemos llegado al punto actual
        if(Vector3.Distance(transform.position, puntos[currentPoint].transform.position)< 0.2f){
            currentPoint++;
            currentPoint %= puntos.Length; // -->currentPoint = currenPoint % puntos.Lenght
        }

        //Movemos
        transform.position = Vector3.MoveTowards(transform.position,
            puntos[currentPoint].transform.position,
            Time.deltaTime * speed);
    }

}

