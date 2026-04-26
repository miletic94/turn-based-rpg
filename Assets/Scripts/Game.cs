using UnityEngine;

public class Game : MonoBehaviour
{
    private void Start()
    {

        var characters = new CharacterDeserializer().Deserialize();
        var knight = characters[0];
        var witch = characters[1];
        var executor = new MoveExecutor();

        Debug.Log($@"[STARTING STATE]
        knight: {knight.ToString()};
        witch: {witch.ToString()};
        ");

        executor.Execute(knight, witch, knight.Moves[0]);
        executor.Execute(witch, knight, witch.Moves[0]);
        Debug.Log($@"[AFTER MOVE 1]
        kinght's move: {knight.Moves[0].Name}
        witch's move: {witch.Moves[0].Name}
        knight: {knight.ToString()};
        witch: {witch.ToString()};
        ");
        executor.Execute(knight, witch, knight.Moves[2]);
        executor.Execute(witch, knight, witch.Moves[1]);
        Debug.Log($@"[AFTER MOVE 2]
        kinght's move: {knight.Moves[2].Name}
        witch's move: {witch.Moves[1].Name}
        knight: {knight.ToString()};
        witch: {witch.ToString()};
        ");
        executor.Execute(knight, witch, knight.Moves[0]);
        executor.Execute(witch, knight, witch.Moves[1]);
        Debug.Log($@"[AFTER MOVE 3]
        kinght's move: {knight.Moves[0].Name}
        witch's move: {witch.Moves[0].Name}
        knight: {knight.ToString()};
        witch: {witch.ToString()};
        ");
        executor.Execute(knight, witch, knight.Moves[0]);
        executor.Execute(witch, knight, witch.Moves[1]);
        Debug.Log($@"[AFTER MOVE 4]
        kinght's move: {knight.Moves[0].Name}
        witch's move: {witch.Moves[1].Name}
        knight: {knight.ToString()};
        witch: {witch.ToString()};
        ");
        executor.Execute(knight, witch, knight.Moves[0]);
        executor.Execute(witch, knight, witch.Moves[0]);
        Debug.Log($@"[AFTER MOVE 5]
        kinght's move: {knight.Moves[0].Name}
        witch's move: {witch.Moves[0].Name}
        knight: {knight.ToString()};
        witch: {witch.ToString()};
        ");
    }
}