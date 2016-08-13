namespace YouthLocationBooking.Business.Logic.Extensions
{
    public static class MathEx
    {
        public static float Clamp(float value, float min, float max)
        {
            return (value < min) ? min : (value > max) ? max : value;
        }
    }
}
