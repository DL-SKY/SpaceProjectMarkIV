using SpaceProject.Data.Objects.Spaceship;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceProject.Objects.Spaceship
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

    public enum Command
    { 
        OnPitch,
        OnRoll,
        OnYaw,
        OnStrafe,
    }

    public enum ForceType
    {
        Torque,
        Force,
    }

    public class CommandData
    {
        public Command command;
        public ForceType type;
        public Vector3 direction;
        public ForceMode forceMode;
    }


    [RequireComponent(typeof(Rigidbody))]
    public class SpaceshipController : MonoBehaviour
    {
        [SerializeField] private SpaceshipData data;
        [SerializeField] private bool isFlightAssist = true;

        private new Rigidbody rigidbody;
        private Dictionary<Command, CommandData> commands = new Dictionary<Command, CommandData>();
        private Dictionary<Command, CommandData> prevCommands = new Dictionary<Command, CommandData>();


        private void Awake()
        {
            rigidbody = GetComponent<Rigidbody>();
        }

        private void FixedUpdate()
        {
            //Debug.LogError("vel : " + rigidbody.angularVelocity);

            foreach (var command in commands)
            {
                switch (command.Value.type)
                {
                    case ForceType.Force:
                        rigidbody.AddForce(command.Value.direction, command.Value.forceMode);
                        break;
                    case ForceType.Torque:
                        rigidbody.AddTorque(command.Value.direction, command.Value.forceMode);
                        break;
                }
            }

            //TODO: переделать FlightAssist!!!
            //Обнуляем ускорение. По факту - это FlightAssist
            if (isFlightAssist)
            {
                rigidbody.velocity = Vector3.Lerp(rigidbody.velocity, Vector3.zero, data.stopFactor * Time.fixedDeltaTime);
                rigidbody.angularVelocity = Vector3.Lerp(rigidbody.angularVelocity, Vector3.zero, data.angularStopFactor * Time.fixedDeltaTime);

                //rigidbody.AddTorque(, ForceMode.VelocityChange);
            }
            
            commands.Clear();
        }


        //тангаж (pitch)
        public void OnPitch(PitchMode _mode, float _speedMod = 1.0f)
        {
            var direction = Vector3.zero;
            switch (_mode)
            {
                case PitchMode.ToDown:
                    direction = transform.right;
                    break;
                case PitchMode.ToUp:
                    direction = -transform.right;
                    break;
            }

            //rigidbody.AddTorque(direction * data.pitchSpeed * _speedMod, ForceMode.VelocityChange);
            AddCommand(new CommandData()
            {
                command = Command.OnPitch,
                type = ForceType.Torque,
                direction = direction * data.pitchSpeed * _speedMod,
                forceMode = ForceMode.VelocityChange
            });
        }

        //крен (roll)
        public void OnRoll(RollMode _mode, float _speedMod = 1.0f)
        {
            var direction = Vector3.zero;
            switch (_mode)
            {
                case RollMode.ToLeft:
                    direction = transform.forward;
                    break;
                case RollMode.ToRight:
                    direction = -transform.forward;
                    break;
            }

            //rigidbody.AddTorque(direction * data.rollSpeed * _speedMod, ForceMode.VelocityChange);
            AddCommand(new CommandData()
            {
                command = Command.OnRoll,
                type = ForceType.Torque,
                direction = direction * data.rollSpeed * _speedMod,
                forceMode = ForceMode.VelocityChange
            });
        }

        //рыскание (yaw)
        public void OnYaw(YawMode _mode, float _speedMod = 1.0f)
        {
            var direction = Vector3.zero;
            switch (_mode)
            {
                case YawMode.ToLeft:
                    direction = -transform.up;
                    break;
                case YawMode.ToRight:
                    direction = transform.up;
                    break;
            }

            //rigidbody.AddTorque(direction * data.yawSpeed * _speedMod, ForceMode.VelocityChange);
            AddCommand(new CommandData()
            {
                command = Command.OnYaw,
                type = ForceType.Torque,
                direction = direction * data.yawSpeed * _speedMod,
                forceMode = ForceMode.VelocityChange
            });
        }

        //вверх/вниз
        //влево/вправо
        public void OnStrafe(StrafeMode _mode, float _speedMod = 1.0f)
        {
            var direction = Vector3.zero;
            switch (_mode)
            {
                case StrafeMode.ToUp:
                    direction = transform.up;
                    break;

                case StrafeMode.ToDown:
                    direction = -transform.up;
                    break;

                case StrafeMode.ToLeft:
                    direction = -transform.right;
                    break;

                case StrafeMode.ToRight:
                    direction = transform.right;
                    break;
            }

            //rigidbody.AddForce(direction * data.strafeSpeed * _speedMod, ForceMode.VelocityChange);
            AddCommand(new CommandData()
            {
                command = Command.OnStrafe,
                type = ForceType.Force,
                direction = direction * data.strafeSpeed * _speedMod,
                forceMode = ForceMode.VelocityChange
            });
        }


        private void AddCommand(CommandData _data)
        {
            if (commands.ContainsKey(_data.command))
                commands[_data.command] = _data;
            else
                commands.Add(_data.command, _data);
        }
    }
}
