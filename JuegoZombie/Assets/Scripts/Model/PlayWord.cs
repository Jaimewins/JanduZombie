using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayWord
{
    public string word;

    public static PlayWord CreateFromJSON(string jsonString)
    {
        return JsonUtility.FromJson<PlayWord>(jsonString);
    }

    public static string CreateJSON(PlayWord p)
    {
        return JsonUtility.ToJson(p);
    }
}
