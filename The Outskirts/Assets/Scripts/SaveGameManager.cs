using AsyncDataLibrary.Infrastructure;
using AsyncDataLibrary.Models;
using AsyncDataLibrary.Repositories;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SaveGameManager : MonoBehaviour
{
    public static SaveGameManager Instance { get; private set; }
    public Vector3 LastCheckpointPosition { get; set; }

    private JsonRepository<GameSave> _saveRepository;
    
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        var fileProvider = new FileStorageProvider();
        var serializer = new JsonDataSerializer();
        string filePath = Path.Combine(Application.persistentDataPath, "game_saves.json");
        _saveRepository = new JsonRepository<GameSave>(filePath, serializer, fileProvider);
    }

    public async Task SaveProgressAsync(PlayerHealth playerHealth, PlayerInventory playerInventory)
    {
        var newSave = new GameSave
        {
            CurrentLevelName = SceneManager.GetActiveScene().name,
            SaveTime = DateTime.Now,
            PlayerPositionX = LastCheckpointPosition.x + 2f,
            PlayerPositionY = LastCheckpointPosition.y - 2f,
            CollectedCoins = playerInventory.GetCoins(),
            CurrentHealth = playerHealth.GetCurrentLives(),
        };

        await _saveRepository.AddAsync(newSave);
    }
    public async Task LoadLatestSaveAsync()
    {
        var allSaves = await _saveRepository.GetAllAsync();

        if (allSaves == null || allSaves.Count == 0)
        {
            Debug.LogWarning("Файлів збереження не знайдено!");
            return;
        }

        var latestSave = allSaves.OrderByDescending(s => s.Id).First();
        await ApplySaveDataAsync(latestSave);
    }

    private async Task ApplySaveDataAsync(GameSave save)
    {
        if (!string.IsNullOrEmpty(save.CurrentLevelName))
        {
            var asyncLoad = SceneManager.LoadSceneAsync(save.CurrentLevelName);

            while (!asyncLoad.isDone)
            {
                await Task.Delay(100);
            }
        }
        else
        {
            Debug.LogWarning("У файлі збереження немає назви рівня!");
            return;
        }

        HeroKnight player = FindObjectOfType<HeroKnight>();
        PlayerHealth playerHealth = FindObjectOfType<PlayerHealth>();
        PlayerInventory playerInventory = FindObjectOfType<PlayerInventory>();
        if (player != null)
        {
            player.transform.position = new Vector3(save.PlayerPositionX, save.PlayerPositionY, 0f);
            playerInventory.LoadCoins(save.CollectedCoins);
            playerHealth.LoadCurrentLives(save.CurrentHealth);
            playerHealth.UpdateHeartsDisplay();
        }
    }
}
