using UnityEngine;
using UnityEngine.UI;

public class MainMenuView : BaseView
{
    [SerializeField] private Button _menuButton;
    public Button MenuButton => _menuButton;
}
