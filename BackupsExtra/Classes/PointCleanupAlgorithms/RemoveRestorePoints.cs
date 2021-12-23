using System.Collections.Generic;
using Backups;
using Backups.Tools;

namespace BackupsExtra.Classes.PointCleanupAlgorithms
{
    public class RemoveRestorePoints
    {
        public static List<RestorePoint> TryToRemove(List<RestorePoint> points, List<RestorePointNumber> numbers)
        {
            if (points.Count.Equals(numbers.Count))
            {
                throw new BackupsException("You can't delete all of the points!");
            }

            List<RestorePoint> result = points;

            foreach (RestorePointNumber number in numbers)
            {
                result.RemoveAll(point => point.PointNumber.Equals(number.Value));
            }

            return result;
        }
    }
}