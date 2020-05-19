using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class AhorcadoWord
{
    public string word;
    public string status;

    public static AhorcadoWord CreateFromJSON(string jsonString)
    {
        return JsonUtility.FromJson<AhorcadoWord>(jsonString);
    }
}
