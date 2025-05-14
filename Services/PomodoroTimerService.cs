using PomodoroApp.Services;

public class PomodoroTimerService
{
    private Timer? _timer;
    public event Action? OnTick;
    public bool IsRunning { get; set; } = false;
    public bool IsWorkInterval { get; set; } = true;
    public int WorkTimeSeconds { get; set; }
    public int BreakTimeSeconds { get; set; }
    public int CompletedCycles { get; set; } = 0;
    public int? DesiredWorkCycles { get; set; }
    public int TimeLeft { get; set; }
    private readonly TimerSettingsService _settings;

    public PomodoroTimerService(TimerSettingsService settings)
    {
        _settings = settings;
        SetOptionsFromSettings();

        _settings.OnSettingsChanged += HandleSettingsChanged;
    }

    private void SetOptionsFromSettings()
    {
        WorkTimeSeconds = _settings.WorkTimeMinutes * 60;
        BreakTimeSeconds = _settings.BreakTimeMinutes * 60;
        DesiredWorkCycles = _settings.WorkCycles;
        TimeLeft = WorkTimeSeconds;
    }

    public void Reset()
    {
        SetOptionsFromSettings();
        IsWorkInterval = true;
        IsRunning = false;
        _timer?.Dispose();
        OnTick?.Invoke();
    }

    public void Start()
    {
        if (IsRunning)
            return;
        IsRunning = true;
        _timer = new Timer(Tick, null, 0, 1000);
    }

    public void Pause()
    {
        _timer?.Dispose();
        IsRunning = false;
    }

    private void Tick(object? state)
    {
        if (TimeLeft > 0)
        {
            TimeLeft--;
            OnTick?.Invoke();
        }
        else
        {
            if (IsWorkInterval)
            {
                CompletedCycles++;

                if (DesiredWorkCycles.HasValue && CompletedCycles == DesiredWorkCycles.Value)
                {
                    Pause(); //Change function to some sort of completion sound/animation/message
                    return;
                }
            }

            IsWorkInterval = !IsWorkInterval;
            //CompletedCycles++;
            TimeLeft = IsWorkInterval ? WorkTimeSeconds : BreakTimeSeconds;
            OnTick?.Invoke();
        }
    }

    private void HandleSettingsChanged()
    {
        if (!IsRunning)
        {
            SetOptionsFromSettings();
            OnTick?.Invoke();
        }
    }
}
