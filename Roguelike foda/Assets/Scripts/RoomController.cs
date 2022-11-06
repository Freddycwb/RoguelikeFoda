using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomController : MonoBehaviour
{
    public GameEvent RoomCreated;
    public GameEvent StartBattle;

    public GameObjectVariable Player;
    public GameObjectVariable CurrentRoom;

    public List<GameObjectArrayVariable> EasyRooms = new List<GameObjectArrayVariable>();
    public List<GameObjectArrayVariable> MediumRooms = new List<GameObjectArrayVariable>();
    public List<GameObjectArrayVariable> HardRooms = new List<GameObjectArrayVariable>();
    public List<GameObjectArrayVariable> AttackItensRooms = new List<GameObjectArrayVariable>();
    public List<GameObjectArrayVariable> HealItensRooms = new List<GameObjectArrayVariable>();
    public List<GameObjectArrayVariable> BossRooms = new List<GameObjectArrayVariable>();

    public GameObject roomPrefab;
    public GameObject playerPrefab;

    public enum RoomType
    {
        none = 0,
        easyRoom,
        mediumRoom,
        hardRoom,
        attackItensRoom,
        healItensRoom,
        bossRoom
    }

    public RoomType roomOfDoor1;
    public RoomType roomOfDoor2;

    public int roomNumber;

    void Start()
    {
        Restart();
    }

    public void EnterDoor1()
    {
        CreateRoom(roomOfDoor1);
    }

    public void EnterDoor2()
    {
        CreateRoom(roomOfDoor2);
    }

    public void CreateRoom(RoomType type)
    {
        roomNumber++;
        if (CurrentRoom.Value != null)
        {
            Destroy(CurrentRoom.Value);
        }
        switch (type)
        {
            case RoomType.none:
                CurrentRoom.Value = Instantiate(roomPrefab);
                break;
            case RoomType.easyRoom:
                EnemiesRoom(EasyRooms);
                break;
            case RoomType.mediumRoom:
                EnemiesRoom(MediumRooms);
                break;
            case RoomType.hardRoom:
                EnemiesRoom(HardRooms);
                break;
            case RoomType.attackItensRoom:
                EnemiesRoom(AttackItensRooms);
                break;
            case RoomType.healItensRoom:
                EnemiesRoom(HealItensRooms);
                break;
            case RoomType.bossRoom:
                EnemiesRoom(BossRooms);
                break;
            default:
                CurrentRoom.Value = Instantiate(roomPrefab);
                break;
        }
        SetNextRooms();
        RoomCreated.Raise();
    }

    public void EnemiesRoom(List<GameObjectArrayVariable> rooms)
    {
        var a = Instantiate(roomPrefab);
        Room r = a.GetComponent<Room>();
        r.enemiesOrder = rooms[Random.Range(0, rooms.Count)].Value;
        r.CreateEnemies();
        CurrentRoom.Value = a;
        StartBattle.Raise();
    }

    public void SetNextRooms()
    {
        switch (roomNumber)
        {
            case 0:
                roomOfDoor1 = RoomType.bossRoom;
                roomOfDoor2 = RoomType.bossRoom;
                break;
            case 5:
                roomOfDoor1 = RoomType.bossRoom;
                roomOfDoor2 = RoomType.bossRoom;
                break;
            default:
                NextDoorBattleRoom();
                break;
        }
    }

    public void NextDoorBattleRoom()
    {
        int a = Random.Range(0, 3);
        switch (a)
        {
            case 0:
                roomOfDoor1 = RoomType.easyRoom;
                break;
            case 1:
                roomOfDoor1 = RoomType.mediumRoom;
                break;
            case 2:
                roomOfDoor1 = RoomType.hardRoom;
                break;
        }
        int b = Random.Range(0, 3);
        while (b == a)
        {
            b = Random.Range(0, 3);
        }
        switch (b)
        {
            case 0:
                roomOfDoor2 = RoomType.easyRoom;
                break;
            case 1:
                roomOfDoor2 = RoomType.mediumRoom;
                break;
            case 2:
                roomOfDoor2 = RoomType.hardRoom;
                break;
        }
    }

    public void Restart()
    {
        Destroy(Player.Value);
        roomNumber = -1;
        Player.Value = Instantiate(playerPrefab, new Vector3(-3.2f, -1.6f, 0), transform.rotation);
        CreateRoom(RoomType.none);
    }
}
