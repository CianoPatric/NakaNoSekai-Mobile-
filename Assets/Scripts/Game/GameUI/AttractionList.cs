using System;
using System.Collections.Generic;
using UnityEngine;

namespace Game.GameUI.Root
{
    public class AttractionList: MonoBehaviour
    {
        private static GameObject[] Attractions;
        
        [SerializeField] private GameObject ButtonPrefab;
        [SerializeField] private RectTransform Context;
        [SerializeField] private GameObject MenuSelector;
        [SerializeField] private GameObject CurrentObject;

        private void Awake()
        {
            Attractions = Resources.LoadAll<GameObject>("Prefabs");
            for (int i = 0; i < Attractions.Length; i++)
            {
                var Button = Instantiate(ButtonPrefab, Context);
                var get = Button.GetComponent<AttractionButtonLogic>();
                get.Attraction = Attractions[i];
            }
        }

        public void swap(GameObject prefab)
        {
            MenuSelector.SetActive(false);
            var get = CurrentObject.GetComponent<Building>();
            get.PrefabOnCard = prefab.GetComponent<Build>();
        }
        
    }
}