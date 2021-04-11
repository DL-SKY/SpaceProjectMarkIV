using SpaceProject.UI.Components;

namespace SpaceProject.UI.Windows.Test
{
    public class TestShootingProgressBar : ProgressBar
    {
        public void AddFillAmount(float _delta)
        {
            FillAmount += _delta;
        }
    }
}
