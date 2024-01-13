public class TimerData {
    public int timeRemaining;
    public bool isGasActive;

    public TimerData(Timer timer) {
        timeRemaining = timer._remainingDuration;
        isGasActive = timer.isGasActive;
    }
}