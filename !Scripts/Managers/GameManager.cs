using Singletons;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public enum GameState { NameState, ChoiceState, OutcomeState }
public enum RestartType { Replay, New }

public class GameManager : PersistentSingleton<GameManager>
{
    #region Properties
    public string PlayerName { get; private set; }
    public ChoiceType PlayerChoice { get; private set; }
    public ChoiceType OpponentChoice { get; private set; }
    public bool? MatchOutcome { get; private set; }
    public int CurrentPlayerWinCount { get; private set; }
    public int CurrentPlayerLossCount { get; private set; }

    #endregion

    [Header("UI Elements: Player Submission")]
    [SerializeField] private TMP_InputField _playerNameInput;
    [SerializeField] private TextMeshProUGUI _invalidNameText;
    [SerializeField] private GameObject _nameSubmitBtn;
    [SerializeField] private GameObject _choiceContainer;

    [Header("UI Elements: Outcomes")]
    [SerializeField] private TextMeshProUGUI _gameOutcomeText;
    [SerializeField] private TextMeshProUGUI _winLossCounter;
    [SerializeField] private GameObject _winLossContainer;
    [SerializeField] private GameObject _headerBackground;
    [SerializeField] private GameObject _winLoseOptionContainer, _drawOptionContainer;

    [Space(30)]
    public UnityEvent<ChoiceType> OnPlayerChoiceSelected;

    public void QuitGame() => Application.Quit();

    #region Player Name Handling

    public void SubmitName()
    {
        string playerName = _playerNameInput.text;
        bool validName = !string.IsNullOrEmpty(playerName);
        HandleNameSubmission(validName, playerName);
    }

    void HandleInvalidNameInput()
    {
        _headerBackground.SetActive(true);
        _invalidNameText.gameObject.SetActive(true);
    }

    void HandleNameSubmission(bool isNameValid, string name)
    {
        if (!isNameValid)
        {
            HandleInvalidNameInput();
            return;
        }

        SetPlayerName(name);

        _playerNameInput.text = "";
        _headerBackground.SetActive(false);
        _invalidNameText.gameObject.SetActive(false);
        _playerNameInput.gameObject.SetActive(false);
        _nameSubmitBtn.SetActive(false);

        _choiceContainer.SetActive(true);
    }

    void SetPlayerName(string name) => PlayerName = name;

    #endregion

    #region OutcomeState

    public void HandlePlayerChoiceSelection(ChoiceType choice)
    {
        _choiceContainer.SetActive(false);

        ChoiceType computerChoice = ChoiceTypeUtils.GenerateRandomChoice();

        MatchOutcome = choice.EvaluateOutcome(computerChoice);

        string eval = MatchOutcome switch
        {
            true => PlayerName + " wins!",
            false => PlayerName + " loses!",
            null => "It's a draw!"
        };

        string outcomeText = string.Format("{0}: {1}\nComputer: {2}\n{3}", PlayerName, choice, computerChoice, eval);

        _headerBackground.SetActive(true);
        _gameOutcomeText.text = outcomeText;
        _gameOutcomeText.gameObject.SetActive(true);

        if (!MatchOutcome.HasValue)
            HandleDraw();
        else HandleWinLose();

        Debug.Log(outcomeText);
    }

    void HandleDraw()
    {
        _drawOptionContainer.SetActive(true);
    }

    void HandleWinLose(bool isForfeit = false)
    {
        if (isForfeit)
        {
            MatchOutcome = false;
            _drawOptionContainer.SetActive(false);
            _gameOutcomeText.text = string.Format("{0} forfeits!\n Computer wins!", PlayerName);
        }

        _winLoseOptionContainer.SetActive(true);
        _ = MatchOutcome ?? false ? CurrentPlayerWinCount++ : CurrentPlayerLossCount++;
        _winLossCounter.text = string.Format("Wins: {0} \nLosses: {1}", CurrentPlayerWinCount, CurrentPlayerLossCount);

        if (!_winLossContainer.activeInHierarchy)
        {
            _winLossContainer.SetActive(true);
            _winLossCounter.gameObject.SetActive(true);
        }
    }

    #endregion

    #region Post-Game Input

    public void HandleForfeit()
    {
        HandleWinLose(true);
    }

    public void PlayAgain() => ResetGameState(RestartType.Replay);
    public void PlayNew() => ResetGameState(RestartType.New);

    void ResetGameState(RestartType restartType)
    {
        HandleResetCommon();

        switch (restartType)
        {
            case RestartType.Replay:
                HandleResetReplay();
                break;
            case RestartType.New:
                HandleResetNew();
                break;
        }
    }

    void HandleResetCommon()
    {
        MatchOutcome = null;

        _gameOutcomeText.text = "";
        _headerBackground.SetActive(false);
        _winLoseOptionContainer.SetActive(false);
        _drawOptionContainer.SetActive(false);
    }

    void HandleResetReplay()
    {
        MatchOutcome = null;

        _gameOutcomeText.text = "";
        _headerBackground.SetActive(false);
        _winLoseOptionContainer.SetActive(false);
        _drawOptionContainer.SetActive(false);

        _choiceContainer.SetActive(true);
    }

    void HandleResetNew()
    {
        MatchOutcome = null;
        PlayerName = null;
        CurrentPlayerLossCount = 0;
        CurrentPlayerWinCount = 0;

        _playerNameInput.text = "";
        _playerNameInput.gameObject.SetActive(true);
        _nameSubmitBtn.SetActive(true);

        _winLossCounter.text = "";
        _winLossContainer.SetActive(false);
    }

    #endregion
}