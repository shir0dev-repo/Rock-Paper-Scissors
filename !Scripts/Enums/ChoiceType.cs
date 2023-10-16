using System;
using Random = UnityEngine.Random;

[Serializable]
public enum ChoiceType {Rock, Paper, Scissors }

public static class ChoiceTypeUtils
{
    /// <summary>
    /// Evaluate the outcome between two choices.
    /// </summary>
    /// <param name="choice">Player choice.</param>
    /// <param name="other">Computer generated choice.</param>
    /// <returns>True for a win, false for a loss, and null for a draw.</returns>
    public static bool? EvaluateOutcome(this ChoiceType choice, ChoiceType other)
    {
        bool? outcome;
        if (choice == other) outcome = null;

        else
        {
            outcome =
                (choice, other) switch
                {
                    (ChoiceType.Rock, ChoiceType.Paper) => false,
                    (ChoiceType.Rock, ChoiceType.Scissors) => true,

                    (ChoiceType.Paper, ChoiceType.Rock) => true,
                    (ChoiceType.Paper, ChoiceType.Scissors) => false,

                    (ChoiceType.Scissors, ChoiceType.Rock) => false,
                    (ChoiceType.Scissors, ChoiceType.Paper) => true,

                    _ => null
                };
        }

        return outcome;
    }

    /// <summary>
    /// Generates a random ChoiceType, to be used by the computer.
    /// </summary>
    /// <returns>A new, randomly generated ChoiceType.</returns>
    public static ChoiceType GenerateRandomChoice()
    {
        
        Array choices = Enum.GetValues(typeof(ChoiceType));
        return (ChoiceType)choices.GetValue(Random.Range(0, choices.Length));
    }
}
