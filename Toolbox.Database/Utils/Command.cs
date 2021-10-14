using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Toolbox.Database.Utils
{
    public class Command
    {
        private Dictionary<string, object> _Parameters;

        public string Query { get; init; }
        public bool IsProcedure { get; init; }
        public ReadOnlyDictionary<string, object> Parameters
        {
            get { return new ReadOnlyDictionary<string, object>(_Parameters); }
        }

        public Command(string query, bool isProcedure = false)
        {
            Query = query;
            IsProcedure = isProcedure;
            _Parameters = new Dictionary<string, object>();
        }

        public void AddParameter(string name, object value)
        {
            if (string.IsNullOrWhiteSpace(name) || _Parameters.ContainsKey(name))
            {
                throw new ArgumentException("Bad parameter !");
            }

            _Parameters.Add(name, value);
        }
    }
}
