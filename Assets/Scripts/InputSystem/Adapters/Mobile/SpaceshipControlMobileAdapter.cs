using SpaceProject.Constants;
using SpaceProject.Events;
using SpaceProject.Objects.Spaceship;
using SpaceProject.Services;

namespace SpaceProject.InputSystem.Adapters
{
    public class SpaceshipControlMobileAdapter : InputAdapter
    {
        private PlayerSpaceship playerSpaceship;


        public SpaceshipControlMobileAdapter(bool _enabled) : base(_enabled)
        {
            EventManager.AddEventListener(ConstantEventsName.ON_PLAYER_SPACESHIP_CREATE, OnPlayerSpaceshipCreateHandler);
            EventManager.AddEventListener(ConstantEventsName.ON_PLAYER_SPACESHIP_DESTROY, OnPlayerSpaceshipDestroyHandler);

            playerSpaceship = ComponentLocator.Resolve<PlayerSpaceship>();
            if (playerSpaceship == null)               
                SetEnable(false);
        }

        ~SpaceshipControlMobileAdapter()
        {
            EventManager.RemoveEventListener(ConstantEventsName.ON_PLAYER_SPACESHIP_CREATE, OnPlayerSpaceshipCreateHandler);
            EventManager.RemoveEventListener(ConstantEventsName.ON_PLAYER_SPACESHIP_DESTROY, OnPlayerSpaceshipDestroyHandler);
        }


        protected override void CustomUpdate()
        { 
            //TODO

            //тангаж (pitch)
            //крен (roll)

            //рыскание (yaw)

            //вверх/вниз
            //влево/вправо
        }

        private void OnPlayerSpaceshipCreateHandler(CustomEvent _event)
        {
            playerSpaceship = (PlayerSpaceship)_event.EventData;            
            SetEnable(true);
        }

        private void OnPlayerSpaceshipDestroyHandler(CustomEvent _event)
        {
            playerSpaceship = null;
            SetEnable(false);
        }
    }
}
