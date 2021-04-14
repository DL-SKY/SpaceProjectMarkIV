using System;

namespace SpaceProject.Data.Objects.Spaceship
{
    [Serializable]
    public class SpaceshipData
    {
        public float controllability = 10.0f;       //Управляемость

        public float maxSpeed = 150.0f;

        public float pitchSpeed = 0.25f;
        public float rollSpeed = 0.25f;
        public float yawSpeed = 0.25f;

        public float strafeSpeed = 2.5f;
    }
}
