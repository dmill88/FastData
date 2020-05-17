using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using CsvHelper;

namespace FastData.Util
{
    public class CSVService
    {
        public async Task<IEnumerable<T>> DownloadAndParseCsvAsync<T>(Uri uri)
        {
            IEnumerable<T> result = new List<T>();
            WebClient client = new WebClient();
            MemoryStream ms = null;
            TextReader tr = null;

            try
            {
                byte[] data = await client.DownloadDataTaskAsync(uri);
                ms = new MemoryStream(data);
                tr = new StreamReader(ms);
                result = ConvertDataToList<T>(tr);
            }
            finally
            {
                if (client != null) client.Dispose();
                if (tr != null) tr.Dispose();
                if (ms != null) ms.Dispose();
            }

            return result;
        }

        public IEnumerable<T> OpenAndParseCsvFileAsync<T>(string fullpath)
        {
            IEnumerable<T> result = new List<T>();
            TextReader tr = null;
            try
            {
                tr = new StreamReader(fullpath);
                result = ConvertDataToList<T>(tr);
            }
            finally
            {
                if (tr != null) tr.Dispose();
            }

            return result;
        }

        protected IEnumerable<T> ConvertDataToList<T>(TextReader tr)
        {
            IEnumerable<T> result = new List<T>();
            CsvReader csvReader = null;

            try
            {
                CsvHelper.Configuration.CsvConfiguration cnfg = new CsvHelper.Configuration.CsvConfiguration(CultureInfo.InvariantCulture)
                {
                    HasHeaderRecord = true,
                    IgnoreBlankLines = true,
                    TrimOptions = CsvHelper.Configuration.TrimOptions.Trim
                };
                cnfg.RegisterClassMap(cnfg.AutoMap<T>());
                csvReader = new CsvReader(tr, cnfg);

                var list = csvReader.GetRecords<T>().ToList();
                result = list;
            }
            finally
            {
                if (csvReader != null) csvReader.Dispose();
            }

            return result;
        }


    }
}
