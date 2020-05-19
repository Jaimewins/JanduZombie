using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    public int life;
    public int stamina;
    public int left;
    public int right;
    public int remain;
    private int contador;
    [SerializeField] Text lifeT;
    [SerializeField] Text staminaT;
    [SerializeField] Text leftT;
    [SerializeField] Text rightT;
    [SerializeField] Text remainT;
    public GameObject player;

    // Start is called before the first frame update
    void Start()
    {
       life = 100;
       stamina = 150;
       left = 20;
       right = 20;
       remain = 10;
       contador = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (stamina != 150)
        {
            contador++;
            if (contador == 50)
            {
                stamina++;
                contador = 0;
            }
        }
        leftT.GetComponent<Text>().text = left.ToString();
        rightT.GetComponent<Text>().text = right.ToString();
        staminaT.GetComponent<Text>().text = stamina.ToString();
    }
}
