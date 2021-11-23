using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    public class Car : IEquipment
    {
        public int Quantity { get; set; }
        public int Performance { get; set; }
        public int Speed { get; set; }
        public bool IsBroken { get; set; }

        public Car(int quantity, int performance, int speed, bool isBroken)
        {
            this.Quantity = quantity;
            this.Performance = performance;
            this.Speed = speed;
            this.IsBroken = isBroken;
        }
    }
}
