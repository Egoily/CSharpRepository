using System;

namespace ee.Utility.Player
{
    public interface IPlayer : IDisposable
    {
        void Play();

        void Stop();

        void Pause();
    }
}