using SpaceProject.Data.Objects.Spaceship;
using SpaceProject.Enums;
using SpaceProject.InputSystem;
using SpaceProject.InputSystem.Adapters;
using SpaceProject.Services;
using UnityEngine;
using UnityEngine.EventSystems;

namespace SpaceProject.UI.SpaceshipControl
{
    [RequireComponent(typeof(Joystick))]
    public class SpaceshipJoystick : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {
        private bool isUsed;

        [Header("Settings")]
        public EnumSpaceshipUIElement joystickType;
        public ManeuverCommand axisXCommandType = ManeuverCommand.None;
        public ManeuverCommand axisYCommandType = ManeuverCommand.None;

        private Joystick joystick;
        private SpaceshipControlMobileAdapter adapter;


        private void Awake()
        {
            joystick = GetComponent<Joystick>();
            adapter = (SpaceshipControlMobileAdapter)ComponentLocator.Resolve<InputController>()?.GetAdapter(Enums.EnumInputAdapters.SpaceshipControl);
        }

        private void LateUpdate()
        {
            if (isUsed)
            {
                adapter?.SendCommand(axisXCommandType, joystick.Direction.x);
                adapter?.SendCommand(axisYCommandType, joystick.Direction.y);
            }
        }


        public virtual void OnPointerDown(PointerEventData eventData)
        {
            isUsed = true;
        }

        public virtual void OnPointerUp(PointerEventData eventData)
        {
            isUsed = false;
        }
    }
}
