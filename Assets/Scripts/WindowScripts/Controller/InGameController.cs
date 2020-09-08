using UnityEngine;

public class InGameController : BaseController<InGameControllerConfig>
{
    private InGameView _view;

    public InGameModel InGameModel { get; private set; }

    public bool IsPaused { get; private set; }

    public override void Init(InGameControllerConfig config)
    {
        base.Init(config);

        _view = config.View as InGameView;

        _view.PauseButton.onClick.AddListener(OnPauseButton);

        InGameModel = new InGameModel();
        ResetScore();
    }

    public override void Update()
    {
        
    }

    public void IncreaseScore(int amount)
    {
        InGameModel.IncreaseScore(amount);
        UpdateView();
    }

    public void ResetScore()
    {
        InGameModel.ResetScore();
        UpdateView();
    }

    protected override void UpdateView()
    {
        base.UpdateView();
        _view.ScoreText.text = "Score = " + InGameModel.Score.ToString();
    }

    private void OnPauseButton()
    {
        IsPaused = !IsPaused;

        if (IsPaused)
        {
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = 1;
        }
    }
}
