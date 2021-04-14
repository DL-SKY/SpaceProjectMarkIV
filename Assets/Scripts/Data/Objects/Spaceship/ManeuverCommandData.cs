using UnityEngine;

namespace SpaceProject.Data.Objects.Spaceship
{
    public enum StrafeMode
    {
        ToUp,
        ToDown,
        ToLeft,
        ToRight,
    }

    public enum YawMode
    {
        ToLeft,
        ToRight,
    }

    public enum RollMode
    {
        ToLeft,
        ToRight,
    }

    public enum PitchMode
    {
        ToDown,
        ToUp,
    }

    public enum ManeuverCommand
    {
        OnPitch,
        OnRoll,
        OnYaw,
        OnStrafeX,
        OnStrafeY,


        None = 999,
    }

    public enum ManeuverType
    {
        Rotate,
        Move,
    }

    public class ManeuverCommandData
    {
        public ManeuverCommand command;
        public ManeuverType type;
        public Vector3 direction;

        private Vector3 targetDirection;


        public ManeuverCommandData(ManeuverCommand _command, ManeuverType _type, Vector3 _direction)
        {
            command = _command;
            type = _type;
            direction = _direction;
            targetDirection = _direction;
        }


        public void SetTargetDirection(Vector3 _direction)
        {
            targetDirection = _direction;
        }

        public Vector3 CalculateDirection(float _controllability)
        {
            direction = Vector3.Lerp(direction, targetDirection, _controllability);
            SetTargetDirection(direction);
            return direction;
        }

        public bool CheckComplete()
        {
            return direction == targetDirection;
        }
    }
}
