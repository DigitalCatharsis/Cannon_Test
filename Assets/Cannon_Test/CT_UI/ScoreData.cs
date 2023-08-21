using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New ScriptableObject", menuName = "Cannon_Test/Score/ScoreBoard")]

public class ScoreData : ScriptableObject
{
    public Dictionary<int, string> scoreDictionary = new Dictionary<int, string>()
    {
        {9999, "DIO JACKSON"},
        {6666, "MICHAEL BRANDO"},
        {200, "JONARHAN JACKSTAR"},
        {150,"JHOZEF JACKSTAR"},
        {100,"JOTARO KUKSON"},
        {10, "JOLENE KUKSON"},
        {69, "IH8 JOJO"},
        {50, "GIORNO JACKVANA"},
    };
}
