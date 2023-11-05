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
    }

    private void OnEnd() {
        Debug.Log("Timer End");
    }
}
