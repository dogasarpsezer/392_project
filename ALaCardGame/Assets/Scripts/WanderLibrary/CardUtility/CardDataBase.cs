using System;
using UnityEngine;

namespace WanderCard
{
    [Serializable]
    public struct CardWorldData
    {
        [SerializeField] private string cardName;
        [SerializeField] private Sprite cardSprite;
        [SerializeField] private GameObject spawnObject;
        public Sprite CardSprite => cardSprite;
        public GameObject CardSpawnObject => spawnObject;
    }
    
    [CreateAssetMenu(fileName = "Card DataBase", menuName = "Create Card/Create Card DataBase")]
    public class CardDataBase : ScriptableObject
    {
        [SerializeField] private CardWorldData[] CardWorldDataBase;
        public CardWorldData[] cards => CardWorldDataBase;
    }
}