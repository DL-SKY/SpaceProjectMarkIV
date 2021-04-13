using System;

namespace SpaceProject.Data.Objects.Spaceship
{
    [Serializable]
    public class SpaceshipData
    {
        [Obsolete]
        public float stopFactor = 10.0f;
        [Obsolete]
        public float angularStopFactor = 10.0f;

        public float pitchSpeed = 0.25f;            // \
        public float rollSpeed = 0.25f;             //  -- 1,0f - примерно 360 град. за 1 сек при angularStopFactor = 10,0f
        public float yawSpeed = 0.25f;              // /

        public float strafeSpeed = 2.5f;
    }
}
