using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HostingGenerator
{
    class Hosting
    {
        Dictionary<DateTime, int> _days;
        HostingSettings _settings;
        DateTime _periodStart;
        DateTime _periodEnd;
        DateTime _date;
        Random _rng;

        public Hosting(HostingSettings settings)
        {
            _settings = settings;
            _days = new Dictionary<DateTime, int>();
            _rng = new Random();
            _date = _settings.StartDate;
            SetFirstLoad();
        }

        private void SetFirstLoad()
        {
            for (int i = 0; i < _settings.LivingLimitDays + 1; i++)
            {
                _days.Add(_settings.StartDate.AddDays(i), GetLoad());
            }
        }

        private int GetLoad()
        {
            return _settings.MinLoadRooms + _rng.Next(_settings.MaxLoadRooms - _settings.MinLoadRooms + 1);
        }

        public IEnumerable<Stay> Stays()
        {
            int stays = 0;
            while (stays < _settings.StaysCount)
            {
                int count;
                Func<int> GetLivingDurationFunc;

                if (_days[_date] <= 0)
                {
                    count = 1 + _rng.Next(_settings.RoomsCount - _settings.MaxLoadRooms);
                    GetLivingDurationFunc = () => 1;
                }
                else
                {
                    count = _days[_date];
                    GetLivingDurationFunc = GetLivingDuration;
                }

                for (int i = 0; i < count; i++)
                {
                    int dur = GetLivingDurationFunc();

                    stays++;
                    yield return new Stay()
                    {
                        Id = stays,
                        Arrival = _date,
                        Departure = _date.AddDays(dur)
                    };
                }

                _days.Remove(_date);
                _date = _date.AddDays(1);
                _days.Add(_date.AddDays(_settings.LivingLimitDays), GetLoad());
            }
        }

        private int GetLivingDuration()
        {
            int duration = 1 + _rng.Next(_settings.LivingLimitDays);
            RefreshLoad(duration);

            return duration;
        }

        private void RefreshLoad(int duration)
        {
            for (int i = 0; i < duration; i++)
            {
                _days[_date.AddDays(i)]--;
            }
        }
    }
}
