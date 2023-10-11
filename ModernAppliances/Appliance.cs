using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModernAppliances
{
    internal class Appliance
    {
        public string ItemNumber { get; set; }
        public string Brand { get; set; }
        public int Quantity { get; set; }
        public int Wattage { get; set; }
        public string Color { get; set; }
        public double Price { get; set; }

        public Appliance(string[] data)
        {
            ItemNumber = data[0];
            Brand = data[1];
            Quantity = int.Parse(data[2]);
            Wattage = int.Parse(data[3]);
            Color = data[4];
            Price = double.Parse(data[5]);
        }

        public virtual string ToFileString()
        {
            return $"{ItemNumber};{Brand};{Quantity};{Wattage};{Color};{Price}";
        }

        public override string ToString()
        {
            return $"Item Number: {ItemNumber}\nBrand: {Brand}\nQuantity: {Quantity}\nWattage: {Wattage}\nColor: {Color}\nPrice: {Price:C}";
        }
    }

    class Refrigerator : Appliance
    {
        public int NumberOfDoors { get; set; }
        public int Height { get; set; }
        public int Width { get; set; }

        public Refrigerator(string[] data) : base(data)
        {
            NumberOfDoors = int.Parse(data[6]);
            Height = int.Parse(data[7]);
            Width = int.Parse(data[8]);
        }

        public override string ToFileString()
        {
            return base.ToFileString() + $";{NumberOfDoors};{Height};{Width}";
        }

        public override string ToString()
        {
            return base.ToString() + $"\nNumber of Doors: {NumberOfDoors}\nHeight: {Height} inches\nWidth: {Width} inches";
        }
    }

    class Vacuum : Appliance
    {
        public string Grade { get; set; }
        public int BatteryVoltage { get; set; }

        public Vacuum(string[] data) : base(data)
        {
            Grade = data[6];
            BatteryVoltage = int.Parse(data[7]);
        }

        public override string ToFileString()
        {
            return base.ToFileString() + $";{Grade};{BatteryVoltage}";
        }

        public override string ToString()
        {
            return base.ToString() + $"\nGrade: {Grade}\nBattery Voltage: {BatteryVoltage} V";
        }
    }

    class Microwave : Appliance
    {
        public double Capacity { get; set; }
        public string RoomType { get; set; }

        public Microwave(string[] data) : base(data)
        {
            Capacity = double.Parse(data[6]);
            RoomType = data[7];
        }

        public override string ToFileString()
        {
            return base.ToFileString() + $";{Capacity};{RoomType}";
        }

        public override string ToString()
        {
            return base.ToString() + $"\nCapacity: {Capacity} cu. ft\nRoom Type: {RoomType}";
        }
    }

    class Dishwasher : Appliance
    {
        public string Feature { get; set; }
        public string SoundRating { get; set; }

        public Dishwasher(string[] data) : base(data)
        {
            Feature = data[6];
            SoundRating = data[7];
        }

        public override string ToFileString()
        {
            return base.ToFileString() + $";{Feature};{SoundRating}";
        }

        public override string ToString()
        {
            return base.ToString() + $"\nFeature: {Feature}\nSound Rating: {SoundRating}";
        }
    }
}
