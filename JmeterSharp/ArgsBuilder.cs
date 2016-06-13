using System;
using System.Collections.Generic;
using System.Linq;

namespace JmeterSharp
{
    public class ArgsBuilder
    {
        private string _arguments;
        private Dictionary<string, string> _properties;
 
        public ArgsBuilder NonGui()
        {
            this._arguments += "-n ";
            return this;
        }
        
        public ArgsBuilder WithTestPlan(string testPlan)
        {
            this._arguments += string.Format("-t \"{0}\" ", testPlan);
            return this;
        }

        public ArgsBuilder WithAssersionReport()
        {
            const string assertionResults = "jmeter.save.saveservice.assertion_results=true";
            this._arguments += string.Format("-J{0} ", assertionResults);
            return this;
        } 
        
        public ArgsBuilder WithFailedAssersionReport()
        {
            const string assertionResults = "jmeter.save.saveservice.assertion_results_failure_message=true";
            this._arguments += string.Format("-J{0} ", assertionResults);
            return this;
        } 

        public ArgsBuilder CollectReportableData(string fileName)
        {
            this._arguments += string.Format("-l \"{0}\" ", fileName);
            return this;
        }

        public ArgsBuilder WithProperty(string key, string value)
        {
            if (this._properties.Count == 0)
            {
                 this._properties = new Dictionary<string, string>();   
            }
            this._properties.Add(key, value);
            return this;
        }

        public ArgsBuilder SetPropertiesFile(string filePath)
        {
            this._arguments += string.Format("-p \"{0}\" ", filePath);
            return this;
        }

        public ArgsBuilder LogTo(string filePath)
        {
            this._arguments += string.Format("-l \"{0}\" ", filePath);
            return this;
        }

        public ArgsBuilder BuildProperties()
        {
            this._arguments +=  _properties.Aggregate(string.Empty, (current, pair) => current + ("-J" + pair.Key + "=" + pair.Value + " "));
            return this;
        }

        public string Build()
        {
            return this._arguments.TrimEnd();
        }
    }
}
