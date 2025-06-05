using Microsoft.JSInterop;

public class SoundService
{
    private readonly IJSRuntime js;

    public SoundService(IJSRuntime js)
    {
        this.js = js;
    }

    public async Task PlayMusicAsync()
    {
        await js.InvokeVoidAsync("soundControl.playMusic");
    }

    public async Task PauseMusicAsync()
    {
        await js.InvokeVoidAsync("soundControl.pauseMusic");
    }

    public async Task PlayAmbientAsync()
    {
        await js.InvokeVoidAsync("soundControl.playAmbient");
    }

    public async Task PauseAmbientAsync()
    {
        await js.InvokeVoidAsync("soundControl.pauseAmbient");
    }

    public async Task PlayEffectAsync()
    {
        await js.InvokeVoidAsync("soundControl.playEffect");
    }

    public async Task PauseEffectAsync()
    {
        await js.InvokeVoidAsync("soundControl.pauseEffect");
    }
}