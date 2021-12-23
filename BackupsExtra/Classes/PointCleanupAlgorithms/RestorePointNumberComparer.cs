using System.Collections.Generic;

namespace BackupsExtra.Classes.PointCleanupAlgorithms
{
    public class RestorePointNumberComparer : IEqualityComparer<RestorePointNumber>
    {
        public bool Equals(RestorePointNumber x, RestorePointNumber y)
        {
            if (ReferenceEquals(x, y)) return true;
            if (ReferenceEquals(x, null)) return false;
            if (ReferenceEquals(y, null)) return false;
            if (x.GetType() != y.GetType()) return false;
            return x.Value == y.Value;
        }

        public int GetHashCode(RestorePointNumber obj)
        {
            return obj.Value;
        }
    }
}