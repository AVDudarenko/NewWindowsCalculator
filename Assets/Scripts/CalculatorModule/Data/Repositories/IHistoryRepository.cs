using System.Collections.Generic;

namespace Data
{
    /*
    * Interface for managing the history of calculations.
    * Defines methods to save, retrieve, and clear calculation records.
    */
    public interface IHistoryRepository
    {
        void SaveRecord(CalculationRecord record);
        List<CalculationRecord> GetAllRecords();
        void Clear();
    }
}