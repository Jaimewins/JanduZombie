using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayLetter
{
    public string letter;

    public static PlayLetter CreateFromJSON(string jsonString)
    {
        return JsonUtility.FromJson<PlayLetter>(jsonString);
    }

    public static string CreateJSON(PlayLetter p)
    {
        return JsonUtility.ToJson(p);
    }
}
