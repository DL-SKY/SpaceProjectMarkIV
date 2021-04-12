using SpaceProject.Constants;
using SpaceProject.Events;
using SpaceProject.Services;
using UnityEngine;

namespace SpaceProject.Objects.Spaceship
{
    [RequireComponent(typeof(SpaceshipController))]
    public class PlayerSpaceship : MonoBehaviour
    {
        public SpaceshipController Spaceship { get; private set; }


        private void Start()
        {
            ComponentLocator.Register(this);

            Spaceship = GetComponent<SpaceshipController>();

            EventManager.DispatchEvent(ConstantEventsName.ON_PLAYER_SPACESHIP_CREATE, this);
        }

        private void OnDestroy()
        {
            ComponentLocator.Unregister(this.GetType());

            EventManager.DispatchEvent(ConstantEventsName.ON_PLAYER_SPACESHIP_DESTROY);
        }
    }
}
