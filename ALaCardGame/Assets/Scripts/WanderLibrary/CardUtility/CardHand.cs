using System.Collections.Generic;
using UnityEngine;
using WanderAttribute;
using WanderExtension;
using WanderMath;
using WanderTween;

namespace WanderCard
{
   public struct HandCardData
{
    private Vector3 cardPosition;
    public Vector3 CardPosition => cardPosition;
    private Quaternion rotation;
    public Quaternion CardRotation => rotation;

    public HandCardData(Vector3 cardPosition, Quaternion rotation)
    {
        this.rotation = rotation;
        this.cardPosition = cardPosition;
    }
}

public class CardHand : MonoBehaviour
{
    [Header("Hand Orientation")]
    [SerializeField] private RectTransform[] handDeckPosLR;
    [SerializeField] private RectTransform handDeckRotationPivot;
    [SerializeField] private RectTransform canvas;
    [SerializeField] private Camera canvasCamera;
    [SerializeField] private float cardDistance;
    [SerializeField] private float cardAngleMagnitude;
    [SerializeField] private int cardHandLimit;
    public int CardHandLimit => cardHandLimit;
    public bool CardHandFull => cardsInHands.Count == cardHandLimit;
    [Header("Card Spawn To Hand Tween")]
    [SerializeField] private Vector3 initCardScale;
    [Min(0.01f)][SerializeField] private float cardToHandScaleTime;
    [Min(0.01f)][SerializeField] private float cardToHandTime;
    
    [Header("Highlight Card")]
    [Min(0.01f)][SerializeField] private float pointerHoverOffset;
    [SerializeField] private Vector3 cardPointerEnterTargetScale;
    [Min(0.01f)][SerializeField] private float cardPointerTime;
    [SerializeField] private Vector3 pointerEnterPositionOffset;
    [SerializeField] private float highlightOffsetDeck;

    [Header("Drag Card")] 
    [SerializeField] private float smoothTime;
    private Vector3 dragVelocity;

    private List<CardDisplay> cardsInHands;
    private List<HandCardData> handCardDatas;
    private static float floatingPointConst = 0.001f;
    private int heldCardIndex = -1;
    private RectTransform deneme;
    private void Awake()
    {
        cardsInHands = new List<CardDisplay>();
    }

    private void Update()
    {
        if (heldCardIndex != -1)
        {
            var position = cardsInHands[heldCardIndex].transform.position;
            cardsInHands[heldCardIndex].transform.position = Vector3.SmoothDamp(position, Input.mousePosition,
                ref dragVelocity, smoothTime);
        }
    }

    public void AddToHandDeck(CardDisplay cardDisplay)
    {
        cardsInHands.Add(cardDisplay);
        var originalCardScale = cardDisplay.transform.localScale;
        
        TweenManager.RegisterScaleToTween(cardDisplay.transform,originalCardScale,
            initCardScale,cardToHandScaleTime,Easing.QuadEaseIn);
        
        UpdateHandCurve(cardsInHands);
    }
    
    public void HighLightDeck(int cardIndex)
    {
        HighLight(cardIndex,initCardScale,cardPointerEnterTargetScale,
            handCardDatas[cardIndex].CardPosition + pointerEnterPositionOffset,1);
    }
    
    public void RemoveHighlightDeck(int cardIndex)
    {
        HighLight(cardIndex,cardPointerEnterTargetScale,initCardScale,
            handCardDatas[cardIndex].CardPosition,-1);
    }

    public void HighLight(int index, Vector3 scaleOrigin, Vector3 scaleTarget, Vector3 positionTarget, 
        int deckDir)
    {
        if (heldCardIndex != -1)
        {
            return;
        }
        //Highlighted Card
        var cardTransform = cardsInHands[index].transform;
        var left = Vector3.Distance(cardTransform.localScale, scaleOrigin);
        var normalized = left / Vector3.Distance(scaleOrigin, scaleTarget);
        normalized = Mathf.Clamp(normalized, 0f, pointerHoverOffset);
        
        TweenManager.RegisterScaleToTween(cardTransform,cardTransform.localScale,scaleTarget,
            cardPointerTime - (cardPointerTime * normalized),Easing.QuadEaseIn);
        TweenManager.RegisterMoveToTween(cardTransform,cardTransform.position, 
            positionTarget,cardPointerTime - (cardPointerTime * normalized),Easing.QuadEaseIn);
        //Rest of the Deck
        for (int i = 0; i < cardsInHands.Count; i++)
        {
            if (i == index)
            {
                continue;
            }
            var direction = handCardDatas[i].CardPosition.x < handCardDatas[index].CardPosition.x ?
                Vector3.left : Vector3.right;
            var target = handCardDatas[i].CardPosition + direction.normalized * (deckDir * highlightOffsetDeck);
            if (Vector3.Distance(positionTarget,handCardDatas[index].CardPosition) <= floatingPointConst)
            {
                target = handCardDatas[i].CardPosition;
            }
            TweenManager.RegisterMoveToTween(cardsInHands[i].transform,cardsInHands[i].transform.position,
                target,cardPointerTime - (cardPointerTime * normalized),Easing.QuadEaseIn);
        }
    }

    public void SetHeldCard(int cardIndex)
    {
        heldCardIndex = cardIndex;
        deneme = cardsInHands[cardIndex].GetComponent<RectTransform>();
    }

    public void RemoveCardFromHand(int cardIndex)
    {
        cardsInHands.RemoveAt(cardIndex);
        UpdateHandCurve(cardsInHands);
    }

    
    public void SetReleaseCard(int cardIndex)
    {
        heldCardIndex = -1;
        RemoveCardFromHand(cardIndex);
    }
    
    public void UpdateHandCurve(List<CardDisplay> ingredientCards)
    {
        handCardDatas = CreateHandCurve(ingredientCards.Count);
        
        for (int i = 0; i < handCardDatas.Count; i++)
        {
            var cardDisplayTransform = ingredientCards[i].transform;
            TweenManager.RegisterMoveToTween(cardDisplayTransform,cardDisplayTransform.position,
                handCardDatas[i].CardPosition,cardToHandTime,Easing.QuadEaseIn);
            TweenManager.RegisterRotateToTween(cardDisplayTransform,cardDisplayTransform.rotation,
                handCardDatas[i].CardRotation,cardToHandTime,Easing.QuadEaseIn);
            int index = i;
            ingredientCards[i].AddPointerEnterEvent(delegate { HighLightDeck(index);});
            ingredientCards[i].AddPointerExitEvent(delegate { RemoveHighlightDeck(index);});
            ingredientCards[i].AddHoldEvent(delegate { SetHeldCard(index);});
            ingredientCards[i].AddReleaseEvent(delegate { SetReleaseCard(index);});
        }
    }
    
    public List<HandCardData> CreateHandCurve(int cardCount)
    {
        var midPos = MathUtility.QuadraticBezierPositionAtTime(handDeckPosLR[0].position,
            handDeckPosLR[1].position, handDeckPosLR[2].position, 0.5f);
        float step = 0.5f - (cardCount - 1) * cardDistance / 2f;
        var cardPositions = new List<HandCardData>();
        for (int i = 0; i < cardCount; i++)
        {
            var pos = MathUtility.QuadraticBezierPositionAtTime(handDeckPosLR[0].position,
                handDeckPosLR[1].position, handDeckPosLR[2].position, step);

            var angle = Vector3.SignedAngle((pos - handDeckRotationPivot.position), Vector3.up, Vector3.forward);
            cardPositions.Add(new HandCardData(pos,Quaternion.Euler(0f,0f,-angle * cardAngleMagnitude)));
            step += cardDistance;
        }

        return cardPositions;
    }
    
#if UNITY_EDITOR
    [SerializeField] private List<Vector3> handCurve = new List<Vector3>();

    [Button("Show Hand Deck Arc")]
    public void CreateHandDeckArc()
    {
        handCurve = MathUtility.QuadraticBezierPositions(handDeckPosLR[0].position, handDeckPosLR[1].position
            , handDeckPosLR[2].position, cardDistance);
    }

    private void OnDrawGizmos()
    {
        handCurve.DrawGizmos(Color.green);
        Gizmos.color = Color.magenta;
        Gizmos.DrawSphere(handDeckRotationPivot.position,25f);
        
        Gizmos.color = Color.yellow;
        foreach (var rectTransform in handDeckPosLR)
        {
            Gizmos.DrawSphere(rectTransform.position,25f);
        }
    }

#endif

} 
}

