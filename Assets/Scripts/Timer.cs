using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    [SerializeField] private Text _timerText;
    [SerializeField] private Image _timerFill;
    public Text GasState;
    public int _remainingDuration;
    public GameObject poisonGas;
    public int poisonGasTime = 30; // the time is 2 seconds longer than the animation of the particles
    public int noGasTime = 10; // the full duration of the timer until the poison gas starts (in seconds)
    public bool isGasActive = false;
    private bool isDataLoaded = false;

    private void Start() {
        Begin(noGasTime);
    }

    public void LoadData(TimerData timerData) {
        isGasActive = timerData.isGasActive;
        var gasController = poisonGas.GetComponent<ParticleSystem>().main;
        if(isGasActive) {
            if(timerData.timeRemaining <= poisonGasTime - 1) {
                gasController.duration = timerData.timeRemaining;
                gasController.startDelay = 0;
            }

            poisonGas.SetActive(true);
        }
        isDataLoaded = true;
        Begin(timerData.timeRemaining);
    }

    public void Begin(int remaining) {
        int duration;
        if(isGasActive) {
            duration = poisonGasTime;
            GasState.text = "of GAS";
        } else {
            duration = noGasTime;
            GasState.text = "till GAS";
        }
        StartCoroutine(UpdateTimer(duration, remaining));
    }

    private IEnumerator UpdateTimer(int duration, int remaining) {
        _remainingDuration = remaining;

        while(_remainingDuration >= 0) {
            _timerText.text = $"{_remainingDuration / 60:00}:{_remainingDuration % 60:00}"; 
            _timerFill.fillAmount = Mathf.InverseLerp(0, duration, _remainingDuration);
            _remainingDuration --;
            yield return new WaitForSeconds(1f);
        }
        yield return new WaitForSeconds(1f);
        OnEnd();
        yield break;
    }

    private void OnEnd() {
        if(isDataLoaded) {
            isDataLoaded = false;
            var gasController = poisonGas.GetComponent<ParticleSystem>().main;
            gasController.duration = poisonGasTime - 2;
            gasController.startDelay = 1;
        }
        
        isGasActive = !isGasActive;
        if(isGasActive) {
            poisonGas.SetActive(true);
            Begin(poisonGasTime);
        }
        else {
            poisonGas.SetActive(false);
            Begin(noGasTime);
        }
    }
}
