namespace PomodoroApp.Services
{
    public class TimerSettingsService{
        public int WorkTimeMinutes {get; set;} = 25;
        public int BreakTimeMinutes {get; set;} = 5;

        public void UpdateTimes(int workTime, int breakTime){
            WorkTimeMinutes = workTime;
            BreakTimeMinutes = breakTime;
        }

        /*
        Implement later - set number of pomodoro cycles (optionally)
        UpdateCycles()
        */
    }
}