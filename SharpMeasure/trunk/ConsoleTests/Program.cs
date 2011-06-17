using System;
using SharpMeasure;
using SharpMeasure.Length;
using SharpMeasure.Time;

namespace ConsoleTests
{
    // Alias pour les unités composites
    // Pas obligatoire, mais plus pratique...
    using Kmh = FractionalUnit<Kilometer, Hour>;
    using Mph = FractionalUnit<Mile, Hour>;

    class Program
    {
        static void Main(string[] args)
        {
            // distance est en mètres (Measure<Meter>)
            var distance = 100.Unit<Meter>();

            Console.WriteLine("Distance:");
            Console.WriteLine(distance);
            Console.WriteLine(distance.Convert<Foot>());
            Console.WriteLine();

            // distance est en secondes (Measure<Second>)
            var time = 9.58.Unit<Second>();

            Console.WriteLine("Time:");
            Console.WriteLine(time);
            Console.WriteLine(time.Convert<Minute>());
            Console.WriteLine();

            // speed est en m/s (Measure<FractionalUnit<Meter, Second>>)
            var usainBoltSpeed = distance.DivideBy(time);
            var speedKmh = usainBoltSpeed.Convert<Kmh>();
            var speedMph = speedKmh.Convert<Mph>();

            Console.WriteLine("Usain Bolt's speed:");
            Console.WriteLine(usainBoltSpeed);
            Console.WriteLine(speedKmh);
            Console.WriteLine(speedMph);
            Console.WriteLine();

            try
            {
                Console.WriteLine("Incompatible units:");
                Console.WriteLine(distance.Convert<Hour>());
                Console.WriteLine();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine();
            }

            Console.WriteLine("Reduction of composite units");
            Console.WriteLine(distance.DivideBy(usainBoltSpeed).Reduce());
            Console.WriteLine(usainBoltSpeed.DivideBy(distance).Reduce());
            Console.WriteLine(time.MultiplyBy(usainBoltSpeed).Reduce());
            Console.WriteLine(usainBoltSpeed.MultiplyBy(time).Reduce());
            var two = 2.Unit<NoUnit>();
            var twiceAsFast = usainBoltSpeed.MultiplyBy(two);
            Console.WriteLine(twiceAsFast);
            Console.WriteLine(twiceAsFast.Reduce());
            var halfAsFast = usainBoltSpeed.DivideBy(two);
            Console.WriteLine(halfAsFast);
            Console.WriteLine(halfAsFast.Reduce());
            Console.ReadLine();
        }
    }
}