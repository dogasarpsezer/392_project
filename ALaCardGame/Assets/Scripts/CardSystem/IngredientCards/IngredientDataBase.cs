using System;
using System.Collections.Generic;
using UnityEngine;

namespace CardSystem
{
    [Serializable]
    public struct IngredientCardWorldData
    {
        [SerializeField] private string ingredientName;
        [SerializeField] private GameObject ingredientObject;
        [SerializeField] private Sprite ingredientCardSprite;

        public Sprite cardSprite => ingredientCardSprite;
    }
    
    [CreateAssetMenu(fileName = "Ingredient DataBase", menuName = "Create Card/Create Ingredient DataBase")]
    public class IngredientDataBase : ScriptableObject
    {
        [SerializeField] private IngredientCardWorldData[] IngredientCardWorldDataBase;
        public IngredientCardWorldData[] cards => IngredientCardWorldDataBase;
    }
}