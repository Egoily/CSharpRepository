namespace ee.Utility.Player
{
    public static class Enums
    {
        /// <summary>
        /// 播放状态
        /// </summary>
        public enum PlayerStates
        {
            Default = 0,

            /// <summary>
            /// 播放
            /// </summary>
            Play = 1,

            /// <summary>
            /// 暂停
            /// </summary>
            Pause = 2,

            /// <summary>
            /// 停止或未播放
            /// </summary>
            Stop = 3,
        }
    }
}