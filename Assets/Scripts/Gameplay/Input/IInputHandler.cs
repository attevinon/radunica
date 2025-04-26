using System;

namespace Scripts.Input
{
    public interface IInputHandler
    {
        public event Action Done;
    }
}