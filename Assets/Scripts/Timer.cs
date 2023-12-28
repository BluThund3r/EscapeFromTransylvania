    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    [SerializeField] private Text _timerText;
    [SerializeField] private Image _timerFill;
    private int _remainingDuration;
    public int Duration = 60;
    public GameObject poisonGas;
    public float poisonGasTime = 30f; // the time is 2 seconds longer than the animation of the particles

    private void Start() {
        Begin(Duration);
    }

    private void Begin(int Second) {
        _remainingDuration = Second;
        StartCoroutine(UpdateTimer());
    }

    private IEnumerator UpdateTimer() {
        while(_remainingDuration >= 0) {
            _timerText.text = $"{_remainingDuration / 60:00}:{_remainingDuration % 60:00}"; 
            _timerFill.fillAmount = Mathf.InverseLerp(0, Duration, _remainingDuration);
            _remainingDuration --;
            yield return new WaitForSeconds(1f);
        }
        OnEnd();
        yield break;
    }

    private void OnEnd() {
        StartCoroutine(StartPoisonGas());
    }
    
    //! TODO: count down during the poison gas as well!!!
    private IEnumerator StartPoisonGas() {
        poisonGas.SetActive(true);
        yield return new WaitForSeconds(poisonGasTime);
        poisonGas.SetActive(false);
        Begin(Duration);
        yield break;
    }
}
