namespace Scripts.Animations
{
    public interface IAnimatableDuringInput : IAnimatable
    {
        public void AnimateDuringInput(float progress);
    }
}