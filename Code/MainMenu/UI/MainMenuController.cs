using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour
{
    [SerializeField] private SaveGameData saveData;
    [SerializeField] private MainSave testSave;
    
    [SerializeField] private Button newGameButton;
    [SerializeField] private TMP_InputField saveName;

    private List<Button> loadButtons = new List<Button>(); 
    void Start()
    {
       newGameButton.onClick.AddListener(NewGame);

       CreateLoadButtons();
    }

    void CreateLoadButtons()
    {
        foreach (var loadButton in loadButtons)
        {
            GameObject.Destroy(loadButton.gameObject);
        }
        
        loadButtons.Clear();

        foreach (var slot in GameInstance.GetSaveSlots())
        {
            var nB = Instantiate(newGameButton);
            newGameButton.transform.parent.AddChild(nB);
            nB.transform.Reset();
            nB.GetComponentInChildren<TextMeshProUGUI>().text = slot;
            var slotName = slot;
            nB.onClick.AddListener(() => LoadGame(slotName));
            loadButtons.Add(nB);
        }
    }

    private void NewGame()
    {
        GameInstance.NewGame(saveName.text);
        CreateLoadButtons();
    }
    
    private void LoadGame(string slotName)
    {
        GameInstance.LoadGame(slotName);
        testSave = GameInstance.GetCurrentSave();
    }

}
