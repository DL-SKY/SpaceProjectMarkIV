using TMPro;
using UnityEngine;

namespace SpaceProject.UI.Windows.FPS
{
    public class FPSWindow : WindowBase
    {
        public const string prefabPath = @"Prefabs\UI\Windows\FPS\FPSWindow";

        [Header("Link")]
        [SerializeField] private TextMeshProUGUI fpsText;

        private float deltaTime;
        private float avgFPS = 0;
        private float newFPS = 0;
        private int frameCount = 0;
        private float minFPS = float.MaxValue;
        private float maxFPS = 0f;
        private float[] lastFPSProbes;
        private int probesArrayLength = 10;


        private void Awake()
        {
            ResetFPS();
        }

        private void Update()
        {
            UpdateFPS();
        }


        public void ResetFPS()
        {
            deltaTime = 0;
            avgFPS = 0;
            newFPS = 0;
            frameCount = 0;
            minFPS = float.MaxValue;
            maxFPS = 0f;

            lastFPSProbes = new float[probesArrayLength];
        }


        private void UpdateFPS()
        {
            deltaTime += (Time.unscaledDeltaTime - deltaTime) * 0.1f;

            newFPS = (1.0f / deltaTime);

            //frameCount++;
            //avgFPS += (newFPS - avgFPS) / frameCount;

            //if (frameCount < probesArrayLength)
            //    lastFPSProbes[frameCount] = newFPS;
            //else if (frameCount == probesArrayLength)
            //    maxFPS = Mathf.Min(lastFPSProbes);
            //else
            //    maxFPS = Mathf.Max(newFPS, maxFPS);

            //minFPS = Mathf.Min(newFPS, minFPS);

            //fpsText.text = string.Format("FPS : {0:0} \nMIN: {1:0} \nMAX: {2:0} \nAVG: {3:2}", newFPS, minFPS, maxFPS, avgFPS);

            fpsText.text = string.Format("FPS : {0:0}", newFPS);
        }
    }
}
