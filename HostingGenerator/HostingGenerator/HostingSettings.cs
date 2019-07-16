using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HostingGenerator
{
    class HostingSettings
    {
        public int RoomsCount { get; }
        public int MinLoadRooms { get; }
        public int MaxLoadRooms { get; }
        public int LivingLimitDays { get; }
        public DateTime StartDate { get; }
        public int StaysCount { get; }

        public static HostingSettings DefaultSettings {
            get => new HostingSettings(5, 3, 4, 3, new DateTime(2000, 1, 1), 10);
        }

        public HostingSettings(int roomsCount,
                               int minLoadRooms,
                               int maxLoadRooms,
                               int livingLimitDays,
                               DateTime startDate,
                               int staysCount)
        {
            RoomsCount = roomsCount;
            MinLoadRooms = minLoadRooms;
            MaxLoadRooms = maxLoadRooms;
            LivingLimitDays = livingLimitDays;
            StartDate = startDate;
            StaysCount = staysCount;
        }
    }
}
