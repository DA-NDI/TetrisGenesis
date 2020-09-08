using UnityEngine;

/// <summary>
/// Holder of all references for Controllers
/// </summary>
[System.Serializable]
public class BaseControllerConfig
{
    [SerializeField] protected BaseView _view;
    public BaseView View => _view;
}
