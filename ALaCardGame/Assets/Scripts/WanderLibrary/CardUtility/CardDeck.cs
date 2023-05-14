using System.Collections.Generic;
using CardSystem;
using TMPro;
using UnityEngine;
using WanderExtension;
using WanderTween;

namespace WanderCard
{
    public class CardDeck : MonoBehaviour
    {
        [Header("Basic Components")] 
        [SerializeField] private KitchenManager kitchenManager;
        [SerializeField] private CardHand cardHand;
        [SerializeField] private CardDataBase cardDataBase;
        [SerializeField] private TextMeshProUGUI stackCounterText;
        [SerializeField] private Transform cardCanvasTransform;
        
        [Header("Card Stack")]
        [SerializeField] private Transform cardStackTransform;
        [SerializeField] private Vector3 cardStackPunchScale;
        [SerializeField] private Vector3 cardStackOriginalScale;
        [Min(0.01f)][SerializeField] private float cardStackPunchTime;
        [SerializeField] private List<Card> cardsInDeck;

        
        [Header("Card Object Pool")]
        [SerializeField] private GameObject cardObject;
        [SerializeField] private int cardPoolSize;
        
        private Queue<Card> cards;
        private Queue<CardDisplay> displayCardPool;
        
        private void Start()
        {
            //Data Load
            cards = cardsInDeck.ListToShuffleQueue();
            UpdateIngredientDeckCounter();
            InitiateIngredientPool();
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                DrawCard();
            }
        }

        public void InitiateIngredientPool()
        {
            displayCardPool = new Queue<CardDisplay>();
            for (int i = 0; i < cardPoolSize; i++)
            {
                var newCardObject = Instantiate(cardObject,cardCanvasTransform);
                var cardDisplayComponent = newCardObject.GetComponent<CardDisplay>();
                cardDisplayComponent.AddRemoveCardEvent(delegate { BackIntoPool(cardDisplayComponent); });
                displayCardPool.Enqueue(cardDisplayComponent);
                newCardObject.SetActive(false);
            }
        }

        public bool isTwo;
        public void BackIntoPool(CardDisplay cardDisplay)
        {
            if (!isTwo)
            {
                kitchenManager.CreateKitchenObject(((IngredientCard)cardDisplay.Card).SpawnCount,
                    cardDisplay.CardWorldData.CardSpawnObject);
            }

            displayCardPool.Enqueue(cardDisplay);
            cardDisplay.gameObject.SetActive(false);
        }
        
        public void DrawCard()
        {
            if (cardHand.CardHandFull) { return; }

            if (cards.Count == 0) {return; }
            var cardWorldObject = displayCardPool.Dequeue();
            var cardToDisplay = cards.Dequeue();
            
            cardWorldObject.SetCard(cardToDisplay,cardDataBase.cards[cardToDisplay.ID]);
            cardWorldObject.gameObject.SetActive(true);
            
            cardHand.AddToHandDeck(cardWorldObject);
            TweenManager.RegisterPunchTween(cardStackTransform,cardStackOriginalScale,
                    cardStackPunchScale,cardStackPunchTime,Easing.QuadEaseIn);
            UpdateIngredientDeckCounter();
        }
        
        public void UpdateIngredientDeckCounter()
        {
            stackCounterText.text = "" + cards.Count;
        }
    }
}