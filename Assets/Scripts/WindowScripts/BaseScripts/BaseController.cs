/// <summary>
/// Basic class for controllers. Need inject Config
/// </summary>
public abstract class BaseController <C> where C : BaseControllerConfig 
{
    public C Config { get; protected set; }

    public bool IsOpen { get; private set; }

    public virtual void Init(C config)
    {
        Config = config;
    }

    public virtual void OpenWindow()
    {
        IsOpen = true;
        Config.View.Holder.SetActive(true);
    }

    public virtual void CloseWindow()
    {
        IsOpen = false;
        Config.View.Holder.SetActive(false);
    }

    protected virtual void UpdateView()
    {
        
    }

    /// <summary>
    /// Some  Update actions because its not a Monobehaviour
    /// </summary>
    public abstract void Update();
}
