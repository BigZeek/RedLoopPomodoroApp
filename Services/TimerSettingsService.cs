namespace PomodoroApp.Services
{
    public class TimerSettingsService{
        public int WorkTimeMinutes {get; set;} = 25;
        public int BreakTimeMinutes {get; set;} = 5;
        public int? WorkCycles {get; set;} = null;
        public event Action? OnSettingsChanged;
        public void UpdateTimes(int workTime, int breakTime){
            WorkTimeMinutes = workTime;
            BreakTimeMinutes = breakTime;
            OnSettingsChanged?.Invoke();
        }
        public void UpdateCycles(int? cycles) {
            WorkCycles = cycles;
        }
        
    }
}