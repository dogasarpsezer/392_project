using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace WanderCard
{
    public class CardDisplay : MonoBehaviour,IPointerEnterHandler, IPointerExitHandler, 
        IPointerDownHandler,IPointerUpHandler
    {
        [SerializeField] private Image cardImage;
        private Card cardToDisplay;
        private CardWorldData cardWorldData;
        public Card Card => cardToDisplay;
        public CardWorldData CardWorldData => cardWorldData;

        private UnityEvent removeCard;
        private UnityEvent onPointerEnterEvent;
        private UnityEvent onPointerExitEvent;
        private UnityEvent holdCardEvent;
        private UnityEvent releaseCardEvent;
        public void SetCard(Card card, CardWorldData worldData)
        {
            cardToDisplay = card;
            cardWorldData = worldData;
            cardImage.sprite = worldData.CardSprite;
        }

        public void AddPointerEnterEvent(UnityAction unityAction)
        {
            onPointerEnterEvent = new UnityEvent();
            onPointerEnterEvent.AddListener(unityAction);
        }

        public void AddRemoveCardEvent(UnityAction unityAction)
        {
            removeCard = new UnityEvent();
            removeCard.AddListener(unityAction);
        }
        
        public void AddPointerExitEvent(UnityAction unityAction)
        {
            onPointerExitEvent = new UnityEvent();
            onPointerExitEvent.AddListener(unityAction);
        }

        public void AddHoldEvent(UnityAction unityAction)
        {
            holdCardEvent = new UnityEvent();
            holdCardEvent.AddListener(unityAction);
        }
        
        public void AddReleaseEvent(UnityAction unityAction)
        {
            releaseCardEvent = new UnityEvent();
            releaseCardEvent.AddListener(unityAction);
        }
        
        public void OnPointerEnter(PointerEventData eventData)
        {
            onPointerEnterEvent.Invoke();
        }
        
        public void OnPointerExit(PointerEventData eventData)
        {
            onPointerExitEvent.Invoke();
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            holdCardEvent.Invoke();
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            removeCard.Invoke();
            releaseCardEvent.Invoke();
        }
        
    }
}