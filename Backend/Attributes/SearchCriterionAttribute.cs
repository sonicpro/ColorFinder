namespace Backend.Attributes
{
    /// <summary>
    /// Marker for incoming message properties that participate in search.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public class SearchCriterionAttribute : Attribute
    {
        public string ReplacementPropertyName { get; private set; }

        public SearchCriterionAttribute(string replacementPropertyName = null)
        {
            this.ReplacementPropertyName = replacementPropertyName;
        }
    }
}