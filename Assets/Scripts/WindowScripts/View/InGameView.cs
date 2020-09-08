using UnityEngine;
using UnityEngine.UI;

public class InGameView : BaseView
{
    [SerializeField] private Text _scoreText;
    public Text ScoreText => _scoreText;

    [SerializeField] private Button _pauseButton;
    public Button PauseButton => _pauseButton;
}
