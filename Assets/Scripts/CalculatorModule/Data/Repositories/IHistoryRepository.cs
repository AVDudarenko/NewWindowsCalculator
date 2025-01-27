using System.Collections.Generic;

namespace Data
{
    public interface IHistoryRepository
    {
        void SaveRecord(CalculationRecord record);
        List<CalculationRecord> GetAllRecords();
        void Clear();
    }
}