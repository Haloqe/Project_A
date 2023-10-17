using System.Collections.Generic;
using System.Globalization;
using System.IO;
using Unity.VisualScripting;
using UnityEngine;

namespace Managers
{
    public class DataManager : Singleton<DataManager>
    {
        public bool ShouldGenerateScripts = false;
        string basePath = Application.dataPath + "/Tables/";
        private PlayerAbilityManager _playerAbilityManager;

        protected override void Awake()
        {
            base.Awake();   // singleton

            // Awake���� ȣ���ϴ°� �������� ������ Ȯ�� �ʿ� (relative order of initialisations matter)
            _playerAbilityManager = GetComponent<PlayerAbilityManager>();
        }
        
        private void Start()
        {
            if (!ShouldGenerateScripts) return;

            _playerAbilityManager.Init(basePath + "PlayerAbilitiesTable.csv");
        }
    }
}