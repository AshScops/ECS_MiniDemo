using UnityEngine;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    private EntityManager _manager;

    //blobAssetStore是一个提供缓存的类，缓存能让你对象创建时更快。
    private BlobAssetStore _blobAssetStore;
    public GameObjectConversionSettings _settings;

    public GameObject enemyPrefab;
    public GameObject bulletPrefab;

    public Entity enemyEntity;
    public Entity bulletEntity;

    public static int enemyMaxCount = 10;
    public static float fakeTimeFlowMult = 1f;
    public static int health = -1;
    public static int score = -1;

    private GameState gameState = GameState.running;

    void Start()
    {
        if (instance == null)
            instance = this;
        else
            return;

        _manager = World.DefaultGameObjectInjectionWorld.EntityManager;
        _blobAssetStore = new BlobAssetStore();
        _settings = GameObjectConversionSettings.FromWorld(World.DefaultGameObjectInjectionWorld, _blobAssetStore);
        enemyEntity = GameObjectConversionUtility.ConvertGameObjectHierarchy(enemyPrefab, _settings);
        bulletEntity = GameObjectConversionUtility.ConvertGameObjectHierarchy(bulletPrefab, _settings);
    }

    private void OnDestroy()
    {
        _blobAssetStore.Dispose();
    }

    public void UpdatePlayerState(int h , int s , CharacterState cs)
    {
        if (gameState == GameState.running)
        {
            if (health != h)
            {
                health = h;
                //EventDriveUI
                EventManager.instance.updateHealthEvent.Invoke(health);
            }

            if (score != s)
            {
                score = s;
                //EventDriveUI
                EventManager.instance.updateScoreEvent.Invoke(score);
            }

            if (cs == CharacterState.dead)
            {
                gameState = GameState.over;
                GameOver();
            }
        }
    }

    public void GameOver()
    {
        //EventDriveUI
        EventManager.instance.showResultEvent.Invoke(score);
    }

}

enum GameState
{
    running = 0,
    over
}