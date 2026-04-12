using System;
using System.Collections.Generic;
using System.Linq;
using VolunteerImpactTracker.Data;
using VolunteerImpactTracker.Models;

namespace VolunteerImpactTracker.Services
{
    public class ImpactService
    {
        private readonly Repository _repository;

        public ImpactService() : this(new Repository())
        {
        }

        public ImpactService(Repository repository)
        {
            _repository = repository;
        }

        public string AddImpactRecord(ImpactRecord record)
        {
            if (string.IsNullOrWhiteSpace(record.Organization))
                return "Error: Organization is required.";

            if (string.IsNullOrWhiteSpace(record.ImpactType))
                return "Error: Impact type is required.";

            if (record.Quantity <= 0)
                return "Error: Quantity must be greater than 0.";

            _repository.SaveImpactRecord(record);
            return "Impact record saved successfully.";
        }

        public List<ImpactRecord> GetAllImpactRecords()
        {
            return _repository.GetAllImpactRecords();
        }

        public Dictionary<string, int> GetImpactSummary()
        {
            return _repository.GetAllImpactRecords()
                .GroupBy(r => r.ImpactType)
                .ToDictionary(g => g.Key, g => g.Sum(r => r.Quantity));
        }

        public string EditImpactRecord(int index, string organization, string impactType, int quantity, DateTime date)
        {
            var records = _repository.GetAllImpactRecords();

            if (index < 0 || index >= records.Count)
                return "Error: Invalid record selection.";

            if (string.IsNullOrWhiteSpace(organization))
                return "Error: Organization is required.";

            if (string.IsNullOrWhiteSpace(impactType))
                return "Error: Impact type is required.";

            if (quantity <= 0)
                return "Error: Quantity must be greater than 0.";

            records[index].Organization = organization;
            records[index].ImpactType = impactType;
            records[index].Quantity = quantity;
            records[index].Date = date;

            _repository.SaveAllImpactRecords(records);
            return "Impact record updated successfully.";
        }

        public string DeleteImpactRecord(int index)
        {
            var records = _repository.GetAllImpactRecords();

            if (index < 0 || index >= records.Count)
                return "Error: Invalid record selection.";

            records.RemoveAt(index);
            _repository.SaveAllImpactRecords(records);

            return "Impact record deleted successfully.";
        }
    }
}