using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoomController : MonoBehaviour
{
    public GameEvent RoomCreated;
    public GameEvent itensCreated;
    public GameEvent itemEquipped;
    public GameEvent StartBattle;

    public GameObjectVariable Player;
    public GameObjectVariable CurrentRoom;

    public List<GameObjectArrayVariable> EasyRooms = new List<GameObjectArrayVariable>();
    public List<GameObjectArrayVariable> MediumRooms = new List<GameObjectArrayVariable>();
    public List<GameObjectArrayVariable> HardRooms = new List<GameObjectArrayVariable>();
    public List<GameObjectArrayVariable> BossRooms = new List<GameObjectArrayVariable>();

    public GameObject roomPrefab;
    public GameObject playerPrefab;
    public SpriteRenderer roomIconLeft, roomIconRight;
    public Sprite[] doorIcon;

    public Buttons buttons;

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
    public int bossDefeated;

    public bool cameFromRight;

    void Start()
    {
        Restart();
    }

    public void EnterDoor1()
    {
        cameFromRight = true;
        CreateRoom(roomOfDoor1);
    }

    public void EnterDoor2()
    {
        cameFromRight = false;
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
                AttackItensRoom();
                break;
            case RoomType.healItensRoom:
                HealItensRoom();
                break;
            case RoomType.bossRoom:
                BossRoom(BossRooms);
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
        r.cameFromRight = cameFromRight;
        r.CreateEnemies();
        CurrentRoom.Value = a;
        StartBattle.Raise();
    }

    public void BossRoom(List<GameObjectArrayVariable> rooms)
    {
        var a = Instantiate(roomPrefab);
        Room r = a.GetComponent<Room>();
        r.enemiesOrder = rooms[bossDefeated].Value;
        r.cameFromRight = cameFromRight;
        r.CreateEnemies();
        CurrentRoom.Value = a;
        bossDefeated++;
        StartBattle.Raise();
    }

    public void AttackItensRoom()
    {
        var a = Instantiate(roomPrefab);
        Room r = a.GetComponent<Room>();
        r.CreateAttackItens();
        buttons.item1 = r.itensOrder[0];
        buttons.item2 = r.itensOrder[1];
        CurrentRoom.Value = a;
        itensCreated.Raise();
    }

    public void HealItensRoom()
    {
        var a = Instantiate(roomPrefab);
        Room r = a.GetComponent<Room>();
        r.CreateHealItens();
        buttons.item1 = r.itensOrder[0];
        buttons.item2 = r.itensOrder[1];
        CurrentRoom.Value = a;
        itensCreated.Raise();
    }

    public void EquipItem1()
    {
        CurrentRoom.Value.GetComponent<Room>().itensOrder[0].transform.SetParent(Player.Value.transform);
        CurrentRoom.Value.GetComponent<Room>().itensOrder[0].SetActive(true);
        itemEquipped.Raise();
    }

    public void EquipItem2()
    {
        CurrentRoom.Value.GetComponent<Room>().itensOrder[1].transform.SetParent(Player.Value.transform);
        CurrentRoom.Value.GetComponent<Room>().itensOrder[1].SetActive(true);
        itemEquipped.Raise();
    }

    public void SetNextRooms()
    {
        switch (roomNumber)
        {
            case 0:
                RewardDoors();
                break;
            case 3:
                RewardDoors();
                break;
            case 5:
                BossDoors();
                break;
            case 6:
                RewardDoors();
                break;
            case 9:
                RewardDoors();
                break;
            case 11:
                BossDoors();
                break;
            case 12:
                RewardDoors();
                break;
            case 15:
                RewardDoors();
                break;
            case 17:
                BossDoors();
                break;
            case 18:
                RewardDoors();
                break;
            case 21:
                RewardDoors();
                break;
            case 23:
                BossDoors();
                break;
            case 24:
                RewardDoors();
                break;
            case 27:
                RewardDoors();
                break;
            case 29:
                BossDoors();
                break;
            default:
                NextDoorBattleRoom();
                break;
        }
    }

    public void RewardDoors()
    {
        roomOfDoor1 = RoomType.attackItensRoom;
        roomOfDoor2 = RoomType.healItensRoom;
        roomIconLeft.sprite = doorIcon[3];
        roomIconRight.sprite = doorIcon[4];
    }

    public void BossDoors()
    {
        roomOfDoor1 = RoomType.bossRoom;
        roomOfDoor2 = RoomType.bossRoom;
        roomIconLeft.sprite = doorIcon[5];
        roomIconRight.sprite = doorIcon[5];
    }

    public void NextDoorBattleRoom()
    {
        int a = Random.Range(0, 3);
        switch (a)
        {
            case 0:
                roomOfDoor1 = RoomType.easyRoom;
                roomIconLeft.sprite = doorIcon[0];
                break;
            case 1:
                roomOfDoor1 = RoomType.mediumRoom;
                roomIconLeft.sprite = doorIcon[1];
                break;
            case 2:
                roomOfDoor1 = RoomType.hardRoom;
                roomIconLeft.sprite = doorIcon[2];
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
                roomIconRight.sprite = doorIcon[0];
                break;
            case 1:
                roomOfDoor2 = RoomType.mediumRoom;
                roomIconRight.sprite = doorIcon[1];
                break;
            case 2:
                roomOfDoor2 = RoomType.hardRoom;
                roomIconRight.sprite = doorIcon[2];
                break;
        }
    }

    public void Restart()
    {
        if (Player.Value != null)
        {
            Destroy(Player.Value.GetComponent<BattleEntity>().entityHud.gameObject);
            Destroy(Player.Value);
        }
        roomNumber = -1;
        bossDefeated = 0;
        Player.Value = Instantiate(playerPrefab, new Vector3(-3.2f, -1.6f, 0), transform.rotation);
        CreateRoom(RoomType.none);
    }
}
