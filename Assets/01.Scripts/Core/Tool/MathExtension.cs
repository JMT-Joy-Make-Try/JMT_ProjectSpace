using System.Collections.Generic;
using System.Globalization;
using UnityEngine;

namespace JMT.Core.Tool
{
    public static class MathExtension
    {
        /// <summary>
        /// 체크할 값이 최소값과 최대값 사이에 있는지 확인합니다.
        /// </summary>
        /// <param name="value">체크할 값</param>
        /// <param name="min">최소값</param>
        /// <param name="max">최대값</param>
        /// <returns>사이에 있으면 true, 아니면 false</returns>
        public static bool IsInRange(this float value, float min, float max)
        {
            return value >= min && value <= max;
        }

        /// <summary>
        /// 체크할 값이 최소값과 최대값 사이에 있는지 확인한 후, 사이에 있으면 반환값을 반환합니다.
        /// </summary>
        /// <param name="value">체크할 값</param>
        /// <param name="ranges">인식 범위</param>
        /// <returns>인식범위의 반환값</returns>
        public static float GetRangeValue(this float value, Range[] ranges)
        {
            for (int i = 0; i < ranges.Length; i++)
            {
                if (ranges[i].IsInRange(value))
                {
                    return ranges[i].ReturnValue;
                }
            }

            return -1;
        }
        
        /// <summary>
        /// 체크할 값이 최소값과 최대값 사이에 있는지 확인한 후, 사이에 있으면 반환값을 반환합니다.
        /// </summary>
        /// <param name="value">체크할 값</param>
        /// <param name="ranges">인식 범위</param>
        /// <returns>인식범위의 반환값</returns>
        public static float GetRangeValue(this float value, List<Range> ranges)
        {
            for (int i = 0; i < ranges.Count; i++)
            {
                if (ranges[i].IsInRange(value))
                {
                    return ranges[i].ReturnValue;
                }
            }

            return -1;
        }
        
        /// <summary>
        /// 체크할 값이 최소값과 최대값 사이에 있는지 확인한 후, 사이에 있으면 반환값을 반환합니다.
        /// </summary>
        /// <param name="value">체크할 값</param>
        /// <param name="ranges">인식 범위</param>
        /// <returns>인식범위의 반환값</returns>
        public static int GetRangeValue(this int value, Range[] ranges)
        {
            for (int i = 0; i < ranges.Length; i++)
            {
                if (ranges[i].IsInRange(value))
                {
                    return ranges[i].ReturnValue;
                }
            }

            return -1;
        }
        
        /// <summary>
        /// 체크할 값이 최소값과 최대값 사이에 있는지 확인한 후, 사이에 있으면 반환값을 반환합니다.
        /// </summary>
        /// <param name="value">체크할 값</param>
        /// <param name="ranges">인식 범위</param>
        /// <returns>인식범위의 반환값</returns>
        public static int GetRangeValue(this int value, List<Range> ranges)
        {
            for (int i = 0; i < ranges.Count; i++)
            {
                if (ranges[i].IsInRange(value))
                {
                    return ranges[i].ReturnValue;
                }
            }

            return -1;
        }
        
        /// <summary>
        /// 체크할 값의 퍼센트를 반환합니다.
        /// </summary>
        /// <param name="value">체크할 값</param>
        /// <param name="max"></param>
        /// <returns>퍼센트 계산한 값</returns>
        public static int GetPercent(this int value, int max)
        {
            return Mathf.RoundToInt(value * 100 / max);
        }
        /// <summary>
        /// 체크할 값의 퍼센트를 반환합니다.
        /// </summary>
        /// <param name="value">체크할 값</param>
        /// <param name="max"></param>
        /// <returns>퍼센트 계산한 값</returns>
        public static int GetPercent(this float value, float max)
        {
            return Mathf.RoundToInt(value * 100 / max);
        }
        
        /// <summary>
        /// 체크할 값의 퍼센트 값에 해당하는 값을 반환합니다.
        /// </summary>
        /// <param name="value">체크할 값</param>
        /// <param name="percentage">목표 퍼센트</param>
        /// <returns>퍼센트 값에 해당하는 값</returns>
       public static  float GetPercentageValue(this float value, float percentage)
        {
            return value * (percentage / 100f);
        }
       
        /// <summary>
        /// 체크할 값의 퍼센트 값에 해당하는 값을 반환합니다.
        /// </summary>
        /// <param name="value">체크할 값</param>
        /// <param name="percentage">목표 퍼센트</param>
        /// <returns>퍼센트 값에 해당하는 값</returns>
        public static  int GetPercentageValue(int value, int percentage)
        {
            return value * (percentage / 100);
        }

        /// <summary>
        /// 체크할 값이 0인지 확인합니다.
        /// </summary>
        /// <param name="value">체크할 값</param>
        /// <returns>0이면 true, 아니면 false</returns>
        public static bool IsZero(this float value)
        {
            return value == 0;
        }
        
        /// <summary>
        /// 체크할 값이 0인지 확인합니다.
        /// </summary>
        /// <param name="value">체크할 값</param>
        /// <returns>0이면 true, 아니면 false</returns>
        public static bool IsZero(this int value)
        {
            return value == 0;
        }
    }

    [System.Serializable]
    public struct Range
    {
        public int Min, Max, ReturnValue;

        public Range(int min, int max, int returnValue)
        {
            Min = min;
            Max = max;
            ReturnValue = returnValue;
        }

        public Range(float min, float max, float returnValue)
        {
            Min = (int)min;
            Max = (int)max;
            ReturnValue = (int)returnValue;
        }

        public bool IsInRange(float value)
        {
            return value >= Min && value <= Max;
        }
    }
}