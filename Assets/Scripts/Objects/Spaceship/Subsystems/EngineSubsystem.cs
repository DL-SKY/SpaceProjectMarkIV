using UnityEngine;

namespace SpaceProject.Objects.Spaceship.Subsystems
{
    public class EngineSubsystem : SubsystemBase
    {
        private Rigidbody rigidbody;

        private float currentSpeed;
        private float targetSpeed;

        //TODO
        private bool isFlightAssist = true;


        public EngineSubsystem(SpaceshipController _spaceship) : base(_spaceship)
        {
            ExecuteType = EnumSubsystemExecuteType.FixedUpdate;

            rigidbody = _spaceship.Rigidbody;
        }


        public override void Execute()
        {
            currentSpeed = Mathf.MoveTowards(currentSpeed, targetSpeed, data.GetMaxChangeSpeedDelta() * Time.fixedDeltaTime);

            //TODO
        }

        public void ChangeSpeed(float _speedMod)
        {
            targetSpeed += data.GetMaxChangeSpeedDelta() * _speedMod;
            Mathf.Clamp(targetSpeed, 0.0f, data.maxSpeed);
        }
    }
}
