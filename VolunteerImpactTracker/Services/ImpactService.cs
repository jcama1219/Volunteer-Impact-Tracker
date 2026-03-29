using System;
using System.Collections.Generic;
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
    }
}