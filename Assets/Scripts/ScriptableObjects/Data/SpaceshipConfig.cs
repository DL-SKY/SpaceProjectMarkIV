using SpaceProject.Data.Objects.Spaceship;
using SpaceProject.Objects.Spaceship;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceProject.ScriptableObjects.Data
{
    [CreateAssetMenu(fileName = "SpaceshipConfig", menuName = "Data/Spaceship/SpaceshipConfig")]
    public class SpaceshipConfig : ScriptableObject
    {
        public SpaceshipData data;

        [Space()]
        public List<SpaceshipUIElementPosition> uiElement3DPositions;
        public List<SpaceshipUIControlPosition> uiControl2DPosition;
    }
}
