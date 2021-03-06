using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Backups
{
    public class RestorePoint
    {
        private List<Storage> _storagesList;
        public RestorePoint(int number, List<Storage> storages)
        {
            PointNumber = number;
            _storagesList = storages;
            RestorePointCreateDate = DateTime.Today;
        }

        public int PointNumber { get; }
        public DateTime RestorePointCreateDate { get; }
        public ReadOnlyCollection<Storage> GetStorages()
        {
            return _storagesList.AsReadOnly();
        }
    }
}