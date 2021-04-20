using UnityEngine;

namespace SpaceProject.Objects.Spaceship.Subsystems
{
    public class EngineSubsystem : SubsystemBase
    {
        private Rigidbody rigidbody;

        private float currentSpeed;
        private float targetSpeed;
        private float speedMod;

        //TODO
        private bool isFlightAssist = true;


        public EngineSubsystem(SpaceshipController _spaceship) : base(_spaceship)
        {
            ExecuteType = EnumSubsystemExecuteType.FixedUpdate;

            rigidbody = _spaceship.Rigidbody;
        }


        public override void Execute(float _deltaTime)
        {
            targetSpeed += data.GetMaxChangeSpeedDelta() * speedMod * _deltaTime;
            targetSpeed = Mathf.Clamp(targetSpeed, 0.0f, data.maxSpeed);


            currentSpeed = Mathf.MoveTowards(currentSpeed, targetSpeed, data.GetMaxChangeSpeedDelta() * _deltaTime);

            Debug.LogError("Speed : " + currentSpeed + "/" + targetSpeed);

            //TODO
        }

        public void ChangeSpeedMod(float _speedMod)
        {
            speedMod = _speedMod;
        }
    }
}
