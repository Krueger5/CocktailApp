using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "WildesAlec", menuName = "Cocktail")]

public class Cocktail : ScriptableObject
{
    public List<ingredient> ingredients = new List<ingredient>();
    public List<ingredient> addOns = new List<ingredient>();
    public List<string> description = new List<string>();
}
