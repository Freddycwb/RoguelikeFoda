using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomController : MonoBehaviour
{
    public GameEvent RoomCreated;

    public GameObjectVariable Player;
    public GameObjectVariable CurrentRoom;

    public List<GameObjectArrayVariable> EasyRooms = new List<GameObjectArrayVariable>();
    public List<GameObjectArrayVariable> HardRooms = new List<GameObjectArrayVariable>();

    public GameObject roomPrefab;
    public GameObject playerPrefab;

    void Start()
    {
        Restart();
    }

    public void CreateRoom()
    {
        if (CurrentRoom.Value != null)
        {
            Destroy(CurrentRoom.Value);
        }
        var a = Instantiate(roomPrefab);
        Room r = a.GetComponent<Room>();
        r.enemiesOrder = EasyRooms[Random.Range(0, EasyRooms.Count)].Value;
        r.CreateEnemies();
        CurrentRoom.Value = a;
        RoomCreated.Raise();
    }

    public void Restart()
    {
        Destroy(Player.Value);
        Player.Value = Instantiate(playerPrefab, new Vector3(-3.2f, -1.6f, 0), transform.rotation);
        CreateRoom();
    }
}
