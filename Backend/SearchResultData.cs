using System;

namespace Backend
{
    /// <summary>
    /// Contains the actual data extracted from MongoDB that is related to QVA.MongoDB.SearchCriteriaBase class and its successors. />
    /// </summary>
    public class SearchResultData
    {
        public SearchResultData(string propertyPath, object value)
        {
            this.PropertyPath = propertyPath;
            this.PropertyValue = value;
        }

        public string PropertyPath { get; private set; }

        public object PropertyValue { get; private set; }
    }
}