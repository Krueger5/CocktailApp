using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CocktailLoader : MonoBehaviour
{
    private List<string> ingredients = new List<string>();

    public void Load(string ingredient)
    {

    }

    public void addIngredient(string ingredient)
    {
        ingredients.Add(ingredient);
    }
}
