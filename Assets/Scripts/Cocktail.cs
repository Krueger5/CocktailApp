using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "WildesAlec", menuName = "Cocktail")]

public class Cocktail : ScriptableObject
{
    public List<string> ingredients = new List<string>();
}
