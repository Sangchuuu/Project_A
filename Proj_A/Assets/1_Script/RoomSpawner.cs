using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomSpawner : MonoBehaviour
{
    [SerializeField]
    private Transform[] bigZones;
    [SerializeField]
    private Transform[] middleZones;
    [SerializeField]
    private Transform[] smallZones;
    [SerializeField]
    private GameObject[] bigRooms;
    [SerializeField]
    private GameObject[] middleRooms;
    [SerializeField]
    private GameObject[] smallRooms;
    List<Transform> bigList = new List<Transform>();
    List<Transform> middleList = new List<Transform>();
    List<Transform> smallList = new List<Transform>();
    List<GameObject> bigRoomList = new List<GameObject>();
    List<GameObject> middleRoomList = new List<GameObject>();
    List<GameObject> smallRoomList = new List<GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        RandomList();
        BigRoomSpawn();
        MiddleRoomSpawn();
        SmallRoomSpawn();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void RandomList()
    {
        bigList.AddRange(bigZones);
        bigRoomList.AddRange(bigRooms);
        middleList.AddRange(middleZones);
        middleRoomList.AddRange(middleRooms);
        smallList.AddRange(smallZones);
        smallRoomList.AddRange(smallRooms);
    }

    void BigRoomSpawn()
    {
        if (bigList.Count != 0 && bigRoomList.Count != 0)
        {
            for (int i = 0; i < 3; i++)
            {
                int ranPoint = Random.Range(0, bigList.Count);
                int ranRoom = Random.Range(0, bigRoomList.Count);
                int ranAngle = Random.Range(0, 2);
                switch (ranAngle)
                {
                    case 0:
                        bigList[ranPoint].Rotate(Vector3.up * 180);
                        break;
                    case 1:
                        break;
                }
                Instantiate(bigRoomList[ranRoom],
                            bigList[ranPoint].position,
                            bigList[ranPoint].rotation);
                bigList.RemoveAt(ranPoint);
                bigRoomList.RemoveAt(ranRoom);
            }
        }
    }

    void MiddleRoomSpawn()
    {
        if (middleList.Count != 0 && middleRoomList.Count != 0)
        {
            for (int i = 0; i < 5; i++)
            {
                int ranPoint = Random.Range(0, middleList.Count);
                int ranRoom = Random.Range(0, middleRoomList.Count);
                int ranAngle = Random.Range(0, 2);
                switch (ranAngle)
                {
                    case 0:
                        middleList[ranPoint].Rotate(Vector3.up * 180);
                        break;
                    case 1:
                        break;
                }
                Instantiate(middleRoomList[ranRoom],
                            middleList[ranPoint].position,
                            middleList[ranPoint].rotation);
                middleList.RemoveAt(ranPoint);
                middleRoomList.RemoveAt(ranRoom);
            }
        }
    }

    void SmallRoomSpawn()
    {
        if (smallList.Count != 0 && smallRoomList.Count != 0)
        {
            for (int i = 0; i < 5; i++)
            {
                int ranPoint = Random.Range(0, smallList.Count);
                int ranRoom = Random.Range(0, smallRoomList.Count);
                int ranAngle = Random.Range(0, 2);
                switch (ranAngle)
                {
                    case 0:
                        smallList[ranPoint].Rotate(Vector3.up * 180);
                        break;
                    case 1:
                        break;
                }
                Instantiate(smallRoomList[ranRoom],
                            smallList[ranPoint].position,
                            smallList[ranPoint].rotation);
                smallList.RemoveAt(ranPoint);
                smallRoomList.RemoveAt(ranRoom);
            }
        }
    }
}
