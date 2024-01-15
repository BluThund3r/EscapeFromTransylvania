using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.Rendering;

public class SaveSlots : MonoBehaviour
{
    private PersistanceManager persistanceManager;
    List<TextMeshProUGUI> saveSlotsText = new List<TextMeshProUGUI>();
    List<Button> saveSlotsButtons = new List<Button>();
    private string selectedSaveName;
    private const string EMPTY_SLOT_TEXT = "*Empty Slot*";
    private GameManager gameManager;

    private void Awake() {
        persistanceManager = GameObject.Find("PersistanceManager").GetComponent<PersistanceManager>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        foreach(Transform child in transform) {
            var childButton = child.GetComponent<Button>();
            childButton.onClick.AddListener(delegate { SelectButton(childButton); });
            saveSlotsButtons.Add(childButton);
            saveSlotsText.Add(child.GetComponentInChildren<TextMeshProUGUI>());
        }
        UpdateSaveSlots();
    }

    private void UpdateSaveSlots() {
        var savesNames = persistanceManager.GetSavesNames();
        savesNames.Sort();
        for(int i = 0; i < savesNames.Count; i++) {
            saveSlotsText[i].text = savesNames[i];
        }

        for(int i = savesNames.Count; i < saveSlotsText.Count; i++) {
            saveSlotsText[i].text = EMPTY_SLOT_TEXT;
        }
    }

    public void SelectButton(Button pressedButton) {
        selectedSaveName = pressedButton.GetComponentInChildren<TextMeshProUGUI>().text;
    }
    
    public void PlaySelectedGame() {
        if(selectedSaveName == EMPTY_SLOT_TEXT) {
            return;
        }
        gameManager.LoadSavedGameScene(selectedSaveName);
    }

    public void DeleteSelectedGame() {
        if(selectedSaveName == EMPTY_SLOT_TEXT) {
            return;
        }
        persistanceManager.DeleteSaveFile(selectedSaveName);
        UpdateSaveSlots();
    }

    public void Back() {
        gameManager.LoadPreviousScene();
    }
}
