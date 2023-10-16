using Singletons;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ChoiceManager : Singleton<ChoiceManager>
{
    public void RockInputSelection() => GameManager.Instance.OnPlayerChoiceSelected?.Invoke(ChoiceType.Rock);
    public void PaperInputSelection() => GameManager.Instance.OnPlayerChoiceSelected?.Invoke(ChoiceType.Paper);
    public void ScissorsInputSelection() => GameManager.Instance.OnPlayerChoiceSelected?.Invoke(ChoiceType.Scissors);
}
