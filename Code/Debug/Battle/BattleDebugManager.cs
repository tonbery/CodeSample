using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleDebugManager : MonoBehaviour
{
    [SerializeField] private CharacterDebugMenu characterDebugMenuPrefab;

    private Canvas _canvas;
    private BattleManager _battleManager;
    private GameObject _backGround;

    private CharacterDebugMenu _characterDebugMenu;

    private IEnumerator Start()
    {
        yield return new WaitForSeconds(1);

        _canvas = FindObjectOfType<Canvas>();
        _battleManager = GetComponent<BattleManager>();
        _backGround = GameObject.Find("Background");

        _backGround.AddComponent<BoxCollider>();
        var debugBackground = _backGround.AddComponent<DebugObjectTarget>();
        debugBackground.OnClick.AddListener(HideDebugs);

        foreach (var character in _battleManager.PlayerCharacters)
        {
            AddCharacterDebugStuff(character);
        }
        
        foreach (var character in _battleManager.EnemyCharacters)
        {
            AddCharacterDebugStuff(character);
        }
        
        _battleManager.OnCharacterSpawn.AddListener(AddCharacterDebugStuff);
    }

    private void HideDebugs(DebugObjectTarget arg0)
    {
        if (_characterDebugMenu) _characterDebugMenu.gameObject.SetActive(false);
    }

    private void AddCharacterDebugStuff(BattleCharacter character)
    {
        var charac = character;
        var capsule = charac.gameObject.AddComponent<CapsuleCollider>();
        capsule.radius = 1;
        capsule.height = 3;
        capsule.center = new Vector3(0, 2, 0);
        var target = charac.gameObject.AddComponent<DebugObjectTarget>();
        target.OnClick.AddListener(arg0 => OnCharacterClick(charac));
    }

    private void OnCharacterClick(BattleCharacter charac)
    {
        if (!_characterDebugMenu)
        {
            _characterDebugMenu = Instantiate(characterDebugMenuPrefab);
            _canvas.transform.AddChild(_characterDebugMenu);
            _characterDebugMenu.transform.Reset();
        }
        
        _characterDebugMenu.gameObject.SetActive(true);
        
        _characterDebugMenu.SetCharacter(charac);
    }
}
