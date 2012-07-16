using System;
using System.Windows;
using System.Windows.Media.Animation;

namespace Thriple.Easing
{
	/// <summary>
	/// A DoubleAnimation with support for using Penner easing equations to provide
	/// a more natural transition between the From and To values.
	/// </summary>
    public class EasingDoubleAnimation : DoubleAnimation
    {
        public EasingEquation Equation { get; set; }

        protected override Freezable CreateInstanceCore()
        {
            return new EasingDoubleAnimation 
            { 
                Equation = this.Equation
            };
        }

        protected override double GetCurrentValueCore(double defaultOriginValue, double defaultDestinationValue, AnimationClock animationClock)
        {
            double time = animationClock.CurrentTime.HasValue ? animationClock.CurrentTime.Value.TotalMilliseconds : 0.0;
            double from = this.From.HasValue ? this.From.Value : defaultOriginValue;
            double to = this.To.HasValue ? this.To.Value : defaultDestinationValue;
            double delta = to - from;
            double duration = this.Duration.TimeSpan.TotalMilliseconds;
            return CalculateCurrentValue(time, from, delta, duration);
        }

        double CalculateCurrentValue(double time, double from, double delta, double duration)
        {
            switch (this.Equation)
            {
                case EasingEquation.BackEaseIn:
                    return EasingEquations.EaseInBack(time, from, delta, duration);

                case EasingEquation.BackEaseInOut:
                    return EasingEquations.EaseInOutBack(time, from, delta, duration);

                case EasingEquation.BackEaseOut:
                    return EasingEquations.EaseOutBack(time, from, delta, duration);

                case EasingEquation.BounceEaseIn:
                    return EasingEquations.EaseInBounce(time, from, delta, duration);

                case EasingEquation.BounceEaseInOut:
                    return EasingEquations.EaseInOutBounce(time, from, delta, duration);

                case EasingEquation.BounceEaseOut:
                    return EasingEquations.EaseOutBounce(time, from, delta, duration);

                case EasingEquation.CircEaseIn:
                    return EasingEquations.EaseInCirc(time, from, delta, duration);

                case EasingEquation.CircEaseInOut:
                    return EasingEquations.EaseInOutCirc(time, from, delta, duration);

                case EasingEquation.CircEaseOut:
                    return EasingEquations.EaseOutCirc(time, from, delta, duration);

                case EasingEquation.CubicEaseIn:
                    return EasingEquations.EaseInCubic(time, from, delta, duration);

                case EasingEquation.CubicEaseInOut:
                    return EasingEquations.EaseInOutCirc(time, from, delta, duration);

                case EasingEquation.CubicEaseOut:
                    return EasingEquations.EaseOutCirc(time, from, delta, duration);

                case EasingEquation.ElasticEaseIn:
                    return EasingEquations.EaseInElastic(time, from, delta, duration);

                case EasingEquation.ElasticEaseInOut:
                    return EasingEquations.EaseInOutElastic(time, from, delta, duration);

                case EasingEquation.ElasticEaseOut:
                    return EasingEquations.EaseOutElastic(time, from, delta, duration);

                case EasingEquation.ExpoEaseIn:
                    return EasingEquations.EaseInExpo(time, from, delta, duration);

                case EasingEquation.ExpoEaseInOut:
                    return EasingEquations.EaseInOutElastic(time, from, delta, duration);

                case EasingEquation.ExpoEaseOut:
                    return EasingEquations.EaseOutElastic(time, from, delta, duration);

                case EasingEquation.Linear:
                    return EasingEquations.Linear(time, from, delta, duration);

                case EasingEquation.QuadEaseIn:
                    return EasingEquations.EaseInQuad(time, from, delta, duration);

                case EasingEquation.QuadEaseInOut:
                    return EasingEquations.EaseInOutQuad(time, from, delta, duration);

                case EasingEquation.QuadEaseOut:
                    return EasingEquations.EaseOutQuad(time, from, delta, duration);

                case EasingEquation.QuartEaseIn:
                    return EasingEquations.EaseInQuart(time, from, delta, duration);

                case EasingEquation.QuartEaseInOut:
                    return EasingEquations.EaseInOutQuart(time, from, delta, duration);

                case EasingEquation.QuartEaseOut:
                    return EasingEquations.EaseOutQuart(time, from, delta, duration);

                case EasingEquation.QuintEaseIn:
                    return EasingEquations.EaseInQuint(time, from, delta, duration);

                case EasingEquation.QuintEaseInOut:
                    return EasingEquations.EaseInOutQuint(time, from, delta, duration);

                case EasingEquation.QuintEaseOut:
                    return EasingEquations.EaseOutQuint(time, from, delta, duration);

                case EasingEquation.SineEaseIn:
                    return EasingEquations.EaseInSine(time, from, delta, duration);

                case EasingEquation.SineEaseInOut:
                    return EasingEquations.EaseInOutSine(time, from, delta, duration);

                case EasingEquation.SineEaseOut:
                    return EasingEquations.EaseOutSine(time, from, delta, duration);

                default:
                    return Double.MinValue;
            }
        }        
    }
}