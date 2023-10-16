using Singletons;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public enum GameState { NameState, ChoiceState, OutcomeState }
public enum RestartLevel { Replay, Total }

public class GameManager : PersistentSingleton<GameManager>
{
    #region Properties
    public string PlayerName { get; private set; }
    public ChoiceType PlayerChoice { get; private set; }
    public ChoiceType OpponentChoice { get; private set; }
    public bool? GameOutcome { get; private set; }

    #endregion

    [Header("UI Elements: Player Submission")]
    [SerializeField] private TMP_InputField _playerNameInput;
    [SerializeField] private TextMeshProUGUI _invalidNameText;
    [SerializeField] private GameObject _nameSubmitBtn;
    [SerializeField] private GameObject _choiceContainer;

    [Header("UI Elements: Outcomes")]
    [SerializeField] private TextMeshProUGUI _gameOutcomeText;
    [SerializeField] private GameObject _headerBackground;
    [SerializeField] private GameObject _winLoseOptionContainer, _drawOptionContainer;

    [Space(30)]
    public UnityEvent<ChoiceType> OnPlayerChoiceSelected;

    public void ResetGameState(RestartLevel restartLevel)
    {
        
    }
    void SetPlayerName(string name) => PlayerName = name;

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

        _headerBackground.SetActive(false);
        _invalidNameText.gameObject.SetActive(false);
        _playerNameInput.gameObject.SetActive(false);
        _nameSubmitBtn.gameObject.SetActive(false);

        _choiceContainer.SetActive(true);
    }

    #endregion

    #region OutcomeState

    public void HandlePlayerChoiceSelection(ChoiceType choice)
    {
        _choiceContainer.SetActive(false);

        ChoiceType computerChoice = ChoiceTypeUtils.GenerateRandomChoice();

        bool? matchOutcome = choice.EvaluateOutcome(computerChoice);

        string eval =  matchOutcome switch
        {
            true => PlayerName + " wins!",
            false => PlayerName + " loses!",
            null => "It's a draw!"
        };

        string outcomeText = string.Format("{0}: {1}\nEnemy: {2}\n{3}", PlayerName, choice, computerChoice, eval);

        _headerBackground.SetActive(true);
        _gameOutcomeText.text = outcomeText;
        _gameOutcomeText.gameObject.SetActive(true);

        if (!matchOutcome.HasValue)
            HandleDraw();
        else HandleWinLose();

        Debug.Log(outcomeText);
    }

    void HandleDraw()
    { 
        
    }

    public void HandleForfeit()
    {

    }

    void HandleWinLose()
    {

    }

    #endregion
}