using System;
using System.Threading;
using Controller;
using Model;

namespace RaceSimulator
{
    class Program
    {
        static void Main(string[] args)
        {
            Competition competition = new Competition();
            Data.Initialize(competition);
            Data.NextRace();
            Visualisation.Initialize(Data.CurrentRace);
           
            for (; ; )
            {
                Thread.Sleep(100);
            }
        }
    }
}
