using Unity.Cinemachine;
using UnityEngine;

public class Bootstrap : MonoBehaviour
{
    [SerializeField] private PlayerConfig _playerConfig;
    [SerializeField] private EnemyConfig _enemyConfig;
    [SerializeField] private GameModeConfig _gameModeConfig;

    [SerializeField] private Transform _playerSpawnPosition;
    [SerializeField] private Transform[] _enemySpawnPoints;

    [SerializeField] private CinemachineCamera _playerCamera;
    [SerializeField] private GameResultView _gameResultView;

    private ControllersUpdateService _controllersUpdateService;
    private GameMode _gameMode;

    private void Awake()
    {
        _controllersUpdateService = new ControllersUpdateService();

        CharactersFactory charactersFactory = new CharactersFactory();
        PlayerFactory playerFactory = new PlayerFactory(_controllersUpdateService, charactersFactory);
        EnemyFactory enemyFactory = new EnemyFactory(_controllersUpdateService, charactersFactory);

        Hero player = playerFactory.Create(_playerConfig, _playerSpawnPosition.position, _playerCamera.transform);
        _playerCamera.Follow = player.CameraTarget;

        ReactiveList<Enemy> enemies = new ReactiveList<Enemy>();

        EnemySpawner enemySpawner = new EnemySpawner(
            enemyFactory,
            _enemyConfig,
            _enemySpawnPoints,
            player,
            enemies,
            _gameModeConfig.EnemySpawnCooldown);

        GameConditionsFactory conditionsFactory = new GameConditionsFactory(_gameModeConfig, player, enemies);

        _gameMode = new GameMode(
            enemySpawner,
            enemies,
            conditionsFactory.CreateWinCondition(),
            conditionsFactory.CreateDefeatCondition());

        _gameMode.Win += OnWin;
        _gameMode.Defeat += OnDefeat;

        _gameMode.Start();
    }

    private void Update()
    {
        _controllersUpdateService.Update(Time.deltaTime);
        _gameMode.Update(Time.deltaTime);
    }

    private void OnDestroy()
    {
        if (_gameMode == null)
            return;

        _gameMode.Win -= OnWin;
        _gameMode.Defeat -= OnDefeat;
        _gameMode.Dispose();
    }

    private void OnWin() => _gameResultView.Show("Победа!");

    private void OnDefeat() => _gameResultView.Show("Поражение...");
}