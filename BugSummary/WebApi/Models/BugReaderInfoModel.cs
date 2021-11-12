using ExternalReader;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Models
{
    public class BugReaderInfoModel
    {
        public string FileName { get; set; }
        public IEnumerable<Parameter> Parameters { get; set; }

        public static IEnumerable<BugReaderInfoModel> ToModel(IEnumerable<Tuple<string, IEnumerable<Parameter>>> bugReaderInfo)
        {
            List<BugReaderInfoModel> modelList = new List<BugReaderInfoModel>();
            foreach (Tuple<string, IEnumerable<Parameter>> bugInfo in bugReaderInfo)
            {
                BugReaderInfoModel model = new BugReaderInfoModel();
                model.Parameters = bugInfo.Item2;
                model.FileName = bugInfo.Item1;
                modelList.Add(model);
            }

            return modelList;
        }
    }
}
