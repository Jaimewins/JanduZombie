using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class HandManManager : MonoBehaviour
{
    [SerializeField] private Sprite[] ahorcadoFrames;
    private int nframeActual = 0;
    [SerializeField] private Image frameActual;
    [SerializeField] private TextMeshProUGUI texto;
    [SerializeField] private TextMeshProUGUI palabraCifrada;
    [SerializeField] private Button button;

    [SerializeField] private string url;


    // Start is called before the first frame update
    private void Start()
    {
        IniciaPartida();
    }

    void IniciaPartida()
    {
        frameActual.sprite = ahorcadoFrames[0];
        palabraCifrada.text = "";
        texto.text = "";
        nframeActual = 0;

        //Cargar primera llamada
        StartCoroutine(CargaPalabraCifrada());
    }

    IEnumerator CargaPalabraCifrada()
    {

        string jsondata = "";

        // Hacer la llamada http/https a la api url + currenword
        using (UnityWebRequest webRequest = UnityWebRequest.Get(url+"currentword"))
        {
            // Request and wait for the desired page.
            yield return webRequest.SendWebRequest();

            if (webRequest.isNetworkError)
            {
                Debug.Log("Error: " + webRequest.error);
                
            }
            else
            {
                Debug.Log("Received: " + webRequest.downloadHandler.text);
                jsondata = webRequest.downloadHandler.text;
            }
        }
        // Procesar el JSON y obtener la palabra cifrada
        if (jsondata != "")
        {
            AhorcadoWord aw = AhorcadoWord.CreateFromJSON(jsondata);

            // Actualizar el valor de la palabracifrada en el frontend
            palabraCifrada.text = aw.word;
        }

       
    }

    public void JuegaPartida()
    {
        button.interactable = false;

        string textoFixed = "";
        if (texto.text.Length> 1)
        {
            textoFixed = texto.text.Substring(0, texto.text.Length - 1);
        }

        Debug.Log("Word = " + textoFixed);
        Debug.Log("Word Size = " + textoFixed.Length);

        // Si solo tiene 1 caracter, hacemos la llamada playletter, si tiene más de 1, hacemos playword
        if (textoFixed.Length == 1)
        {
            StartCoroutine(PlayLetter(textoFixed.ToUpper()));
        }
        else if(textoFixed.Length == 0)
        {
            Debug.Log("Eres una Bernat");
        }
        else
        {
            StartCoroutine(PlayWord(textoFixed.ToUpper()));
        }
    }

    IEnumerator PlayWord(string word)
    {
        Debug.Log("PlayWord");
        string jsondata = "";


        WWWForm form = new WWWForm();
        form.AddField("word", word);

        using (UnityWebRequest webRequest = UnityWebRequest.Post(url + "playword", form))
        {
            yield return webRequest.SendWebRequest();

            if (webRequest.isNetworkError || webRequest.isHttpError)
            {
                Debug.Log(webRequest.error);
                Debug.Log("Ha pasado algo");
                Debug.Log(webRequest.downloadHandler.text);
            }
            else
            {
                Debug.Log("Form upload complete!");
                jsondata = webRequest.downloadHandler.text;
            }
        }
        // Procesar el JSON y obtener la palabra cifrada
        if (jsondata != "")
        {
            Debug.Log(jsondata);
            AhorcadoWord aw = AhorcadoWord.CreateFromJSON(jsondata);
         

            if (aw.status != "WIN")
            {
                nframeActual++;
                frameActual.sprite = ahorcadoFrames[nframeActual];

                if (nframeActual == ahorcadoFrames.Length - 1)
                {
                    texto.text = "You are a looser";
                }
                else
                {
                    button.interactable = true;
                }
            }
            else
            {
                Debug.Log("LO PUTO AMO");
                button.interactable = true;
            }
        }
    }


    IEnumerator PlayLetter(string letter)
    {
        Debug.Log("PlayLetter");
        /*        
        PlayLetter pl = new PlayLetter();
        pl.letter = letter;

        string data = JsonUtility.ToJson(pl);
        Debug.Log("Json a enviar: " + data);
        */
        string jsondata = "";

        
        WWWForm form = new WWWForm();
        form.AddField("letter", letter);

        using (UnityWebRequest webRequest = UnityWebRequest.Post(url +"playletter", form))
        {
            yield return webRequest.SendWebRequest();

            if (webRequest.isNetworkError || webRequest.isHttpError)
            {
                Debug.Log(webRequest.error);
                Debug.Log("Ha pasado algo");
                Debug.Log(webRequest.downloadHandler.text);
            }
            else
            {
                Debug.Log("Form upload complete!");
                jsondata = webRequest.downloadHandler.text;
            }
        }
        // Procesar el JSON y obtener la palabra cifrada
        if (jsondata != "")
        {
            Debug.Log(jsondata);
            AhorcadoWord aw = AhorcadoWord.CreateFromJSON(jsondata);

            // Actualizar el valor de la palabracifrada en el frontend
            palabraCifrada.text = aw.word;

            if(aw.status != "OK")
            {
                nframeActual++;
                frameActual.sprite = ahorcadoFrames[nframeActual];

                if (nframeActual == ahorcadoFrames.Length - 1)
                {
                    texto.text = "You are a looser";
                }
                else
                {
                    button.interactable = true;
                }
            }
            else
            {
                button.interactable = true;
            }
        }       
    }
}
