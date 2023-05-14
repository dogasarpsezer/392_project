using UnityEngine;

namespace WanderCard
{
    [CreateAssetMenu(fileName = "Card", menuName = "Create Card/Create Card")]

    public class Card : ScriptableObject
    {
        [SerializeField] private int cardID;
        public int ID => cardID;
    } 
}

