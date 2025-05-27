using UnityEngine;

namespace Game.GameUI.Root
{
    public class AttractionButtonLogic:MonoBehaviour
    {
        public GameObject Attraction;

        public void GetMethod()
        {
            var Find = GameObject.FindFirstObjectByType<AttractionList>();
            Find.swap(Attraction);
        }
    }
    
}