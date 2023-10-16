using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RPSOpponent : MonoBehaviour
{
    [SerializeField] private ChoiceType _choiceType;
    public ChoiceType Choice { get { return  _choiceType; } }

    public void GenerateNewChoice() => _choiceType = ChoiceTypeUtils.GenerateRandomChoice();
}
