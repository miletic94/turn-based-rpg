using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using UnityEngine;

public class CharacterDeserializer
{
    public List<CharacterDTO> DeserializeCharacter()
    {
        string charactersPath = Path.Combine(Application.streamingAssetsPath, "Characters.json");
        string charactersJson = File.ReadAllText(charactersPath);

        List<CharacterDTO> characters = JsonConvert.DeserializeObject<List<CharacterDTO>>(charactersJson);
        return characters;
    }

    public HeroDTO DeserializeHero()
    {
        string heroPath = Path.Combine(Application.streamingAssetsPath, "Hero.json");
        string heroJson = File.ReadAllText(heroPath);

        HeroDTO hero = JsonConvert.DeserializeObject<HeroDTO>(heroJson);

        return hero;
    }
}