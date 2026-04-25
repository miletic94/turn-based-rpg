using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using UnityEngine;

public class CharacterDeserializer
{
    public void Deserialize()
    {
        string path = Path.Combine(Application.streamingAssetsPath, "Moves.json");
        string json = File.ReadAllText(path);
        List<Move> moves = JsonConvert.DeserializeObject<List<Move>>(json);
        Debug.Log(moves[0].Name);
    }
}