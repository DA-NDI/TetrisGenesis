using UnityEngine;

public class ControllerManager : BaseControllerManager
{
    [SerializeField] private MainMenuControllerConfig _mainMenuConfig;
    [SerializeField] private InGameControllerConfig _inGameMenuConfig;
    
    public MainMenuController MainMenuController { get; private set; }
    public InGameController InGameController { get; private set; }

    public void Awake()
    {
        InitControllers();
    }

    public override void InitControllers()
    {
        InGameController = new InGameController();
        InGameController.Init(_inGameMenuConfig);

        MainMenuController = new MainMenuController(InGameController);
        MainMenuController.Init(_mainMenuConfig);


        MainMenuController.OpenWindow();
    }

    public void Update()
    {
        if (MainMenuController != null && MainMenuController.IsOpen)
        {
            MainMenuController.Update();
        }

        if (InGameController != null && InGameController.IsOpen)
        {
            InGameController?.Update();
        }
    }
}
