using SpaceProject.Enums;
using SpaceProject.Objects.Spaceship;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceProject.UI.Windows.Test
{
    [Serializable]
    public class SpaceshipUIElementTarget
    {
        public EnumSpaceshipUIElement uiElement;
        public Transform element;
        //public Transform target;
    }


    public class SpaceShipTest3DUIWindow : WindowBase
    {
        public const string prefabPath = @"Prefabs\UI\Windows\Test\SpaceShipTest3DUIWindow";

        [Header("UI")]
        [SerializeField] private List<SpaceshipUIElementPrefab> uiPrefabs = new List<SpaceshipUIElementPrefab>();

        private PlayerSpaceship playerSpaceship;
        private List<SpaceshipUIElementTarget> uiElements = new List<SpaceshipUIElementTarget>();


        private void LateUpdate()
        {
            //foreach (var element in uiElements)
                //element.element.SetPositionAndRotation(element.target.position, element.target.rotation);
        }


        public override void Initialize(object _data)
        {
            playerSpaceship = (PlayerSpaceship)_data;

            Create3DUI();

            base.Initialize(_data);
        }


        private void Create3DUI()
        {
            uiElements.Clear();

            foreach (var point in playerSpaceship.Spaceship.GetUI3DElementPositions())
            {
                var prefab = uiPrefabs.Find(x => x.uiElement == point.uiElement)?.prefab;
                if (prefab != null)
                {
                    var newElement = Instantiate(prefab, transform);
                    uiElements.Add(new SpaceshipUIElementTarget() 
                    { 
                        uiElement = point.uiElement, 
                        element = newElement.transform, 
                        //target = point.position,
                    });
                }
            }
        }
    }
}
