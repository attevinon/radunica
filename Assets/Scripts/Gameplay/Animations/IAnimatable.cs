using System;

namespace Scripts.Animations
{
    public interface IAnimatable
    {
        public void Animate(Action callback);
    }
}