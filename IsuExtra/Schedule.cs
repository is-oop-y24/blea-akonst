using System.Collections.Generic;
using System.Linq;
using Isu;
using Isu.Tools;

namespace IsuExtra
{
    public class Schedule
    {
        private List<Couple> _schedule = new List<Couple>();

        public void AddCouple(Couple couple)
        {
            if (_schedule.Exists(cp => cp.StartCoupleNumber.Equals(couple.StartCoupleNumber)
                                                && cp.StartDay.Equals(couple.StartDay)))
            {
                throw new IsuException("This time is busy!");
            }

            _schedule.Add(couple);
        }

        public bool RemoveCouple(Couple couple)
        {
            return _schedule.Remove(couple);
        }

        public bool IsTimeBusy(Couple couple)
        {
            Couple result = _schedule.FirstOrDefault(c => c.StartDay.Equals(couple.StartDay)
                                                          && c.StartCoupleNumber.Equals(couple.StartCoupleNumber));

            return result != default(Couple);
        }

        public bool IsSuitableSchedules(Schedule schedule)
        {
            foreach (Couple cp in schedule._schedule)
            {
                Couple patternCouple = _schedule.FirstOrDefault(couple =>
                                                                couple.StartDay.Equals(cp.StartDay) &&
                                                                couple.StartCoupleNumber.Equals(cp.StartCoupleNumber));

                if (patternCouple != default)
                {
                    return false;
                }
            }

            return true;
        }

        public int CoupleCount()
        {
            return _schedule.Count;
        }
    }
}