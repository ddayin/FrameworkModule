using System;

namespace Framework
{
    public static class SimpleSystemRandom
    {
        private static System.Random _random = new System.Random();
        
        
        /// <summary>
        /// 임의의 정수를 반환하는 메서드(멤버 함수) (음수는 나오지 않음, n>=0)
        /// min~max 사이의 임의의 랜덤한 수를 반환하는 메서드 (min이상, max미만)
        /// </summary>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <returns></returns>
        public static int Range(int min, int max)
        {
            return _random.Next(min, max);
        }
        
        /// <summary>
        /// ax보다 작은 임의의 정수를 반환하는 메서드 입니다. (음수는 나오지 않음, n>=0)
        /// </summary>
        /// <param name="max"></param>
        /// <returns></returns>
        public static int Range(int max)
        {
            return _random.Next(max);
        }
        
        /// <summary>
        /// 0.0~1.0 사이의 double 타입 수를 random 하게
        /// </summary>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <returns></returns>
        public static float Range(float min, float max)
        {
            return (float)_random.NextDouble() * (max - min) + min;
        }
    }
}