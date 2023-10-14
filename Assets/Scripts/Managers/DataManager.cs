using System.Collections.Generic;
using System.Globalization;
using System.IO;
using Unity.VisualScripting;
using UnityEngine;

namespace Managers
{
    public class DataManager : Singleton<DataManager>
    {
        private PlayerAbilityManager _playerAbilityManager;

        // �ѱ����� path �����ϴ°� ���ҵ��Ͽ� datamanager -> init(path)������ ®�µ�
        // ���� manager���� �����ϴ½����� ����������
        // Ŭ���� �� ��ü�� xxxTable�� �ϰ� �����͸� �����ϵ����Ͽ� path�� �ڵ�ȭ�ϴ� ��ȵ� �����
        string basePath = Application.dataPath + "/Tables/";

        protected override void Awake()
        {
            base.Awake();   // singleton

            // Awake���� ȣ���ϴ°� �������� ������ Ȯ�� �ʿ� (relative order of initialisations differ)
            _playerAbilityManager = GetComponent<PlayerAbilityManager>();
        }
        
        private void Start()
        {
            _playerAbilityManager.Init(basePath + "PlayerAbilitiesTable.csv");
        }
    }
}