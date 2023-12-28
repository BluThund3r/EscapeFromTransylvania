    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    [SerializeField] private Text _timerText;
    [SerializeField] private Image _timerFill;
    public Text GasState;
    private int _remainingDuration;
    public GameObject poisonGas;
    public int poisonGasTime = 30; // the time is 2 seconds longer than the animation of the particles
    public int noGasTime = 10; // the full duration of the timer until the poison gas starts (in seconds)
    private bool isGasActive = false;

    private void Start() {
        Begin(noGasTime);
    }

    private void Begin(int duration) {
        if(isGasActive) {
            GasState.text = "of GAS";
        } else {
            GasState.text = "till GAS";
        }
        StartCoroutine(UpdateTimer(duration));
    }

    private IEnumerator UpdateTimer(int duration) {
        _remainingDuration = duration;
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
