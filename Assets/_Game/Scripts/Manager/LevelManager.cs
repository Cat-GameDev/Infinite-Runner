using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.Rendering;
using Debug = UnityEngine.Debug;
using Random = UnityEngine.Random;

public class LevelManager : Singleton<LevelManager>
{
    [SerializeField] private LaneData laneData;
    [SerializeField] private Transform startPoint;
    [SerializeField] private Transform endPoint;
    [SerializeField] private Vector3 playerTransform;
    [SerializeField] private float evnMoveSpeed = 5f;

    [Header("Buildings")]
    [SerializeField] private Transform[] buildingSpawnPoints;
    [SerializeField] private Vector2 buildingSpawnScaleRange = new Vector2(0.6f, 0.8f);

    [Header("Street Light")]
    [SerializeField] private Transform[] streetLightSpawnPoints;

    [SerializeField] private Coin coin;
    [SerializeField] private Train train;
    [SerializeField] private Player player;

    float hightSpeed = 20f;





    PoolType[] poolTypeThreats = new PoolType[]
    {
        PoolType.Threat_Car_Normal,
        PoolType.Threat_Road_Block,
        PoolType.Threat_CarRoad_Block,
    };

    private bool isIncreseSpeed = false;
    private Vector3 moveDirection;

    public Vector3 MoveDirection { get => moveDirection;}
    public float EvnMoveSpeed { get => evnMoveSpeed;}
    public Vector3 PlayerTransform { get => playerTransform; }


    private void Start() 
    {
        UIManager.Instance.OpenUI<MainMenu>();
        
        //OnInit();
        
    }

    private void Update() 
    {
        if(player.Coin < 100 || isIncreseSpeed)
            return;
        
        CheckScore();
        
    }

    public Transform GetPlayer()
    {
        return player.transform;
    }

    private void CheckScore()
    {
        if(player.Coin > 100 && player.Coin < 200)
        {
            evnMoveSpeed = 14;
            hightSpeed = 21;
            
        }
        else if(player.Coin > 200 && player.Coin < 300)
        {
            evnMoveSpeed = 15;
            hightSpeed = 22;
        }
        else if(player.Coin > 300 && player.Coin < 500)
        {
            evnMoveSpeed = 16;
            hightSpeed = 23;
        }
        else if(player.Coin > 500)
        {
            evnMoveSpeed = 17;
            hightSpeed = 24;
        }
    }



    public void OnInit()
    {
        Vector3 nextBlockPosition = startPoint.position;
        float endPointDistant = Vector3.Distance(startPoint.position, endPoint.position);
        moveDirection = (endPoint.position - startPoint.position).normalized;
        ObjectOnlyType newBlock;
        while(Vector3.Distance(nextBlockPosition, startPoint.position) < endPointDistant)
        {
            // Road
            newBlock = SpawnNewBlock(nextBlockPosition, MoveDirection);
            float blockLength = newBlock.GetComponent<BoxCollider>().bounds.size.z;   
            nextBlockPosition +=  MoveDirection * blockLength;
            // building
            SpawnBuilding(nextBlockPosition, MoveDirection);
            SpawnStreetLight(nextBlockPosition, MoveDirection);
        }

        player = SimplePool.Spawn<Player>(PoolType.Player);
        
        
        StartSpawnElements();
        player.OnInit();
    }

    private void StartSpawnElements()
    {
        StartCoroutine(SpawnThreatCoroutine());
        StartCoroutine(SpawnCoinCoroutine());
        StartCoroutine(SpawnTrainCoroutine());
        StartCoroutine(SpawnSpeedUpCoroutine());
    }

    IEnumerator SpawnSpeedUpCoroutine()
    {
        SpeedUp newSpeedUp = null;
        while(GameManager.Instance.IsState(GameState.Gameplay))
        {
            newSpeedUp = SimplePool.Spawn<SpeedUp>(PoolType.SpeedUp, SpawnRandomPoint(), Quaternion.identity);
            SetDataObject(newSpeedUp);
            yield return new WaitForSeconds(newSpeedUp.SpawnInternalCoin);
        }
    }

    IEnumerator SpawnThreatCoroutine( )
    {
        ThreatMutiType newThreat = null;
        Coin newCoin = null;
        while(GameManager.Instance.IsState(GameState.Gameplay))
        {
            
            int randomIndex = Random.Range(0, poolTypeThreats.Length);
            PoolType selectedPoolType = poolTypeThreats[randomIndex];
            if(selectedPoolType is PoolType.Threat_CarRoad_Block)
            {
                newThreat = SimplePool.Spawn<ThreatMutiType>(selectedPoolType, laneData.RandomTwoLane() +  new Vector3(0f,0f,startPoint.position.z), Quaternion.identity);
                newThreat.RandomMeshCar();
                //newCoin = coin.SpawnCoinArcShape(newThreat.transform.position, moveDirection, endPoint, evnMoveSpeed);
            }
            else if(selectedPoolType is PoolType.Threat_Road_Block)
            {
                newThreat = SimplePool.Spawn<ThreatMutiType>(selectedPoolType, SpawnRandomPoint(), Quaternion.identity);
                newCoin = coin.SpawnCoin(newThreat.transform.position, moveDirection, endPoint, evnMoveSpeed);
            }
            else
            {
                newThreat = SimplePool.Spawn<ThreatMutiType>(selectedPoolType, SpawnRandomPoint(), Quaternion.identity);
                newThreat.RandomMeshCar();
                newCoin = coin.SpawnCoinArcShape(newThreat.transform.position, moveDirection, endPoint, evnMoveSpeed);
            }
            SetDataObject(newCoin);
            SetDataObject(newThreat);

            yield return new WaitForSeconds(newThreat.SpawnInternal);
        }
    }

    IEnumerator SpawnCoinCoroutine( )
    {
        Coin newCoin = null;
        while(GameManager.Instance.IsState(GameState.Gameplay))
        {
            newCoin = coin.SpawnCoin(SpawnRandomPoint(), MoveDirection, endPoint, EvnMoveSpeed);
            yield return new WaitForSeconds(coin.SpawnInternalCoin);
        }
    }

    IEnumerator SpawnTrainCoroutine()
    {
        Train newTrain = null;
        Trashcan newTrashcan = null;
        while(GameManager.Instance.IsState(GameState.Gameplay))
        {
            newTrain =  train.SpawnTrain(SpawnRandomPoint(), MoveDirection, endPoint, EvnMoveSpeed);
            newTrashcan = SimplePool.Spawn<Trashcan>(PoolType.Trashcan, SpawnRandomPoint() , Quaternion.identity);
            SetDataObject(newTrashcan);
            yield return new WaitForSeconds(newTrain.SpawnInternal);
        }
    }



    private Vector3 SpawnRandomPoint()
    {
        return laneData.RandomLane() +  new Vector3(0f,0f,startPoint.position.z);
    }

    public ObjectOnlyType SpawnNewBlock(Vector3 spawnPosition, Vector3 moveDirection)
    {
        
        ObjectOnlyType newBlock = SimplePool.Spawn<ObjectOnlyType>(PoolType.Road);

        newBlock.transform.position = spawnPosition;
        SetDataObject(newBlock);
        return newBlock;
    }

    public void SpawnBuilding(Vector3 spawPosition, Vector3 moveDirection)
    {
        ObjectMutiType newBuliding = null;
        for(int i =0; i< buildingSpawnPoints.Length; i++)
        {
            Vector3 buildingSpawnLoc = spawPosition + (buildingSpawnPoints[i].position - startPoint.position);
            int rotationOffsetBy90 = Random.Range(0, 3);
            Quaternion buildingSpawnRotation = Quaternion.Euler(0f, rotationOffsetBy90*90, 0f);
            Vector3 buildingSpawnSize = Vector3.one * Random.Range(buildingSpawnScaleRange.x, buildingSpawnScaleRange.y);

            newBuliding = SimplePool.Spawn<ObjectMutiType>(PoolType.Building, buildingSpawnLoc, buildingSpawnRotation);
            newBuliding.RandomMeshBuilding();
            newBuliding.transform.localScale = buildingSpawnSize;
            SetDataObject(newBuliding);
        }
    }

    public void SpawnStreetLight(Vector3 spawPosition, Vector3 moveDirection)
    {
        ObjectOnlyType newStreetLight = null;
        for(int i =0; i< streetLightSpawnPoints.Length; i++)
        {
            Vector3 streetLightSpawnLoc = spawPosition + (streetLightSpawnPoints[i].position - startPoint.position);
            Quaternion SpawnRotation = Quaternion.LookRotation((startPoint.position - streetLightSpawnPoints[i].position).normalized, Vector3.up);
            Quaternion RotationOffset = Quaternion.Euler(0f,-90f,0f);
            newStreetLight = SimplePool.Spawn<ObjectOnlyType>(PoolType.Street_Light, streetLightSpawnLoc, SpawnRotation*RotationOffset);
            
            SetDataObject(newStreetLight);
        }
    }


    public void SetDataObject(Object @object)
    {
        if(@object != null)
        {
            //@object.SetMoveSpeed(EvnMoveSpeed);
            @object.SetDestination(endPoint.position);
            @object.SetMoveDir(MoveDirection);
        }
    }

    public void OnReset()
    {
        SimplePool.CollectAll();
    }

    
    public void OnStartGame()
    {
        GameManager.Instance.ChangeState(GameState.Gameplay);
        AudioManager.Instance.PlayMusic("GamePlay");
        OnInit();
    }


    public void OnMenu()
    {
        DespawnAllObject();
        GameManager.Instance.ChangeState(GameState.MainMenu);
        OnReset();
        UIManager.Instance.CloseAll();
        UIManager.Instance.OpenUI<MainMenu>();
        AudioManager.Instance.PlayMusic("MainMenu");
    }

    public void DespawnAllObject()
    {
        player.OnDespawn();
    }

    public void OnRetry()
    {
        DespawnAllObject();
        OnReset();
        UIManager.Instance.CloseAll();
        UIManager.Instance.OpenUI<GamePlay>();
        OnStartGame();
    }

    private void PauseGame()
    {
        GameManager.Instance.ChangeState(GameState.Pause);
    }

    private void UpdateRanking()
    {
        UIManager.Instance.OpenUI<Victory>().ShowScore(player.Coin);
        SaveDataManager.SaveNewRankEntry(SaveDataManager.GetPlayerName(), DateTime.Now, player.Coin);
    }

    public void GameOver()
    {
        UpdateRanking();
        Invoke(nameof(PauseGame), 2f);
        UIManager.Instance.CloseAll();
        DespawnAllObject();
        
        UIManager.Instance.OpenUI<Victory>().ActiveButtonAfterTime();
    }

    public void InscreaseEvnSpeed()
    {
        isIncreseSpeed = true;
        evnMoveSpeed = hightSpeed;
        Invoke(nameof(ResetEvnSpeed), 2f);
    }

    public void ResetEvnSpeed()
    {
        isIncreseSpeed = false;
        evnMoveSpeed = 13f;
    }


    


}
