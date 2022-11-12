using CandidateInfo.Models;
using CsvHelper;
using CsvHelper.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace CandidateInfo.Services.Csv
{
    public class CandidateService : ICandidateService
    {
        private readonly string filePath;
        public CandidateService()
        {
            filePath = System.IO.Directory.GetCurrentDirectory() + "\\StoredData\\data.csv";
        }

        public async Task<IEnumerable<Candidate>> GetCandidates()
        {
            List<Candidate> result = new List<Candidate>();

            if (!File.Exists(filePath)) return await Task.FromResult(result);

            using (TextReader fileReader = File.OpenText(filePath))
            {
                var config = new CsvConfiguration(CultureInfo.InvariantCulture)
                {
                    PrepareHeaderForMatch = args => args.Header.ToLower(),
                    HasHeaderRecord = true,
                    HeaderValidated = null,
                    Delimiter = ",",
                    MissingFieldFound = null,
                };

                using (var csv = new CsvReader(fileReader, config))
                {
                    csv.Context.TypeConverterOptionsCache.GetOptions<DateTime?>().NullValues.AddRange(new[] { "NULL" });

                    csv.Read();
                    csv.ReadHeader();
                    result = csv.GetRecords<Candidate>().ToList();
                }
            }

            return await Task.FromResult(result);
        }

        public async Task<bool> UpsertCandidateInfo(Candidate candidate)
        {
            var isSuccess = false;

            var candidates = (List<Candidate>) await GetCandidates();

            candidates = candidates.Where(c => c.Email.ToLower() != candidate.Email.ToLower()).ToList();
            candidates.Add(candidate);

            if (!Directory.Exists(filePath)) Directory.CreateDirectory(Path.GetDirectoryName(filePath));

            using (var writer = new StreamWriter(filePath))
            using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
            {
                csv.Context.TypeConverterOptionsCache.GetOptions<DateTime?>().NullValues.AddRange(new[] { "NULL" });

                csv.WriteHeader<Candidate>();
                csv.NextRecord();
                csv.WriteRecords(candidates);
                
                isSuccess = true;
            }

            return await Task.FromResult(isSuccess);
        }
    }
}
