using PomodoroApp.Services;

public class PomodoroTimerService
{
    private Timer? _timer;
    public event Action? OnTick;
    public event Action? OnMessageChanged;
    public event Action? OnIntervalChanged;
    public bool IsRunning { get; set; } = false;
    public bool IsWorkInterval { get; set; } = true;
    public int WorkTimeSeconds { get; set; }
    public int BreakTimeSeconds { get; set; }
    public int CompletedCycles { get; set; } = 0;
    public int? DesiredWorkCycles { get; set; }
    public int TimeLeft { get; set; }
    private string _currentMessage = "";
    public string CurrentMessage
    {
        get => _currentMessage;
        private set
        {
            _currentMessage = value;
            OnMessageChanged?.Invoke();
        }
    }
    readonly string[] motivationalMessages = new string[]
    {
        "Let's get to work!",
        "You’ve got this!",
        "Focus and crush it!",
        "Time to build momentum!",
        "One step closer!",
        "Make this count!",
        "Keep pushing!",
        "Stay sharp, stay strong!",
        "Your goals need you now!",
        "Dial in and go!",
    };

    string[] breakMessages = new string[]
    {
        "Great job — take a breather!",
        "Break time! You've earned it.",
        "Stretch, hydrate, relax.",
        "Time to recharge.",
        "Rest now, crush it later.",
        "Deep breath — you're doing great.",
        "Let your mind wander a bit.",
        "Reset and refocus.",
        "Enjoy this moment of calm.",
        "Short break, big gains.",
    };

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
        CompletedCycles = 0;
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
            OnIntervalChanged?.Invoke();
            TimeLeft = IsWorkInterval ? WorkTimeSeconds : BreakTimeSeconds;
            if (IsWorkInterval)
            {
                DisplayMotivationalMessage();
            }
            else
            {
                DisplayBreakMessage();
            }

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

    public string DisplayMotivationalMessage()
    {
        var random = new Random();
        var message = motivationalMessages[random.Next(motivationalMessages.Length)];
        CurrentMessage = message;
        OnMessageChanged?.Invoke();
        return message;
    }

    public string DisplayBreakMessage()
    {
        var random = new Random();
        var message = breakMessages[random.Next(breakMessages.Length)];
        CurrentMessage = message;
        OnMessageChanged?.Invoke();
        return message;
    }
}
