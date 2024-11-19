using System;
using System.Text;
using UnityEngine;
using Random = System.Random;


namespace Framework.Test
{
    public class RandomTest : MonoBehaviour
    {
        private void Awake()
        {
            //TestMersennTwister();
            
            TestNoramlDistribuition();
        }

        private void TestMersennTwister()
        {
            MersenneTwister random = new MersenneTwister(Guid.NewGuid().GetHashCode());

            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < 100; i++)
            {
                uint result = random.NextUInt32(1, 100);
                sb.Append(result + " ");
            }

            Debug.Log(sb.ToString());
        }

        private void TestNoramlDistribuition()
        {
            Random random = new Random();
            StringBuilder sb = new StringBuilder();
            
            for (int i = 0; i < 100; i++)
            {
                double input = random.NextDouble();
                double result = UnityNormalDistribution.stdnormal_cdf(input);
                sb.Append(result + " ");
            }
            
            Debug.Log(sb.ToString());
        }
    }
}
