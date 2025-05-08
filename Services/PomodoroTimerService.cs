using PomodoroApp.Services;

public class PomodoroTimerService {
    private Timer? _timer;
    public event Action? OnTick;

    public bool IsRunning {get; set;} = false;
    public bool IsWorkInterval {get; set;} = false;

    public int WorkTimeSeconds {get; set;}
    public int BreakTimeSeconds {get; set;}

    public int TimeLeft {get; set;}

    private readonly TimerSettingsService _settings;

    public PomodoroTimerService(TimerSettingsService settings) {
        _settings = settings;
        setTimesFromSettings();
    }

    private void setTimesFromSettings() {
        WorkTimeSeconds = _settings.WorkTimeMinutes * 60;
        BreakTimeSeconds = _settings.BreakTimeMinutes * 60;
        TimeLeft = WorkTimeSeconds;
    }

    public void Start() {
        if (IsRunning) return;
        IsRunning = true;
        _timer = new Timer(Tick, null, 0, 1000);
    }

    public void Pause() {
        _timer?.Dispose();
        IsRunning = false;
    }

    private void Tick(object? state) {
        if(TimeLeft > 0) {
            TimeLeft--;
            OnTick?.Invoke();
        } 
        else {
            IsWorkInterval = !IsWorkInterval;
            TimeLeft = IsWorkInterval ? WorkTimeSeconds : BreakTimeSeconds;
            OnTick?.Invoke();
        }
    }
}