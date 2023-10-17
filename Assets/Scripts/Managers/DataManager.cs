using UnityEngine;

public class DataManager : Singleton<DataManager>
{
    string basePath = Application.dataPath + "/Tables/";

    private PlayerAbilityManager _playerAbilityManager;
    private EnemyManager _enemyManager;

    protected override void Awake()
    {
        base.Awake();   // singleton

        // Awake���� ȣ���ϴ°� �������� ������ Ȯ�� �ʿ� (relative order of initialisations matter)
        _playerAbilityManager = GetComponent<PlayerAbilityManager>();
        _enemyManager = GetComponent<EnemyManager>();
    }
    
    private void Start()
    {
        _playerAbilityManager.Init(basePath + "PlayerAbilitiesTable.csv");
        _enemyManager.Init(basePath + "EnemyDataTable.csv");
    }
}
