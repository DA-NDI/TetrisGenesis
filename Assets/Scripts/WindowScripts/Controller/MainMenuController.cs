using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuController : BaseController<MainMenuControllerConfig>
{
    private MainMenuView _view;

    public MainMenuModel MainMenuModel { get; private set; }

    private InGameController _inGameController;

    public MainMenuController(InGameController inGame)
    {
        _inGameController = inGame;
    }

    public override void Init(MainMenuControllerConfig config)
    {
        base.Init(config);

        _view = config.View as MainMenuView;

        _view.MenuButton.onClick.AddListener(OnMenuButton);

        MainMenuModel = new MainMenuModel();
    }

    public override void Update()
    {
    }

    private void OnMenuButton()
    {
        CloseWindow();
        _inGameController.OpenWindow();
    }
}
