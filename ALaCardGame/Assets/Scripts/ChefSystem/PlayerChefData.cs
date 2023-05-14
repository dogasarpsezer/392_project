using UnityEngine;

namespace ChefSystem
{
    [CreateAssetMenu(fileName = "Chef Data", menuName = "Create Chef/Create Chef Data")]
    public class PlayerChefData : ScriptableObject
    {
        [SerializeField] private int handCardLimit;
        public int handLimit => handCardLimit;
    }
}