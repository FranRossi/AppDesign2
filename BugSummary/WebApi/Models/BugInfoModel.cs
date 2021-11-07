using ExternalReader;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Models
{
    public class BugInfoModel
    {
        public string Name { get; set; }
        public IEnumerable<Parameter> Parameters { get; set; }

        public static IEnumerable<BugInfoModel> ToModel(IEnumerable<Tuple<string, IEnumerable<Parameter>>> bugReaderInfo)
        {
            List<BugInfoModel> modelList = new List<BugInfoModel>();
            foreach (Tuple<string, IEnumerable<Parameter>> bugInfo in bugReaderInfo)
            {
                BugInfoModel model = new BugInfoModel();
                model.Parameters = bugInfo.Item2;
                model.Name = bugInfo.Item1;
                modelList.Add(model);
            }

            return modelList;
        }
    }
}
