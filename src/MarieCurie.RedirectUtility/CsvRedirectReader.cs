using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace MarieCurie.RedirectUtility
{
    public class CsvRedirectReader : IRedirectReader
    {
        private string _fileName;

        public CsvRedirectReader(string fileName)
        {
            _fileName = fileName;
        }

        public IEnumerable<RedirectInstruction> GetRedirectItems()
        {
            var reader = new StreamReader(File.OpenRead(_fileName));
            var redirectList = new List<RedirectInstruction>();
            while (!reader.EndOfStream)
            {
                var line = reader.ReadLine();
                if (line == null)
                    continue;

                if (string.IsNullOrEmpty(line))
                    continue;

                if (string.IsNullOrWhiteSpace(line))
                    continue;
                
                var values = line.Split(',');

                var existingKey = redirectList.FirstOrDefault(r => r.OldUrl == values[0]);
                if (existingKey != null)
                    continue;

                var redirect = new RedirectInstruction {OldUrl = values[0], NewUrl = values[1]};
                redirectList.Add(redirect);
            }

            return redirectList.Where(r => !r.IsEmpty);
        }
    }
}