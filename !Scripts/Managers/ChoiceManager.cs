using Singletons;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ChoiceManager : Singleton<ChoiceManager>
{
    public void InputSelectionRock() => GameManager.Instance.OnPlayerChoiceSelected?.Invoke(ChoiceType.Rock);
    public void InputSelectionPaper() => GameManager.Instance.OnPlayerChoiceSelected?.Invoke(ChoiceType.Paper);
    public void InputSelectionScissors() => GameManager.Instance.OnPlayerChoiceSelected?.Invoke(ChoiceType.Scissors);
}
