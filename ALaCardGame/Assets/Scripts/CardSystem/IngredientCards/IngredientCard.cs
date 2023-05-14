using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WanderCard;

namespace CardSystem
{
    [CreateAssetMenu(fileName = "IngredientCard", menuName = "Create Card/Create Ingredient Card")]
    public class IngredientCard : Card
    {
        [SerializeField] private int ingredientCount;
        [SerializeField] private int ingredientID;

        public int ID => ingredientID;
        public int SpawnCount => ingredientCount;
    }
 
}

