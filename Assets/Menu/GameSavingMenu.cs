using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameSavingMenu : SaveSlots
{
    TMP_InputField saveNameInput;
    private new void Awake() {
        base.Awake();
        saveNameInput = GameObject.Find("SaveNameInput").GetComponent<TMP_InputField>();
    }

    public void SaveAndBack() {
        
        if(saveNameInput.text == "" || selectedSaveName == null) {
            return;
        }

        if(selectedSaveName != EMPTY_SLOT_TEXT) {
            persistanceManager.DeleteSaveFile(selectedSaveName);
        }
        persistanceManager.SaveCurrentStateToFile(saveNameInput.text);
        Back();
    }
}
