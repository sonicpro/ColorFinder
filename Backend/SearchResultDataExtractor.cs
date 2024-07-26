using Qognify.QVA.Common.Attributes;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Backend
{
    public static class SearchResultDataExtractor
    {
        /// <summary>
        /// Parses an object using reflection. Gets the values and the paths of all the properties marked by <see cref="SearchCriterionAttribute"/>.
        /// </summary>
        /// <param name="object">The object to parse.</param>
        /// <returns>A string path to the property, the property value, and the property type in <see cref="SearchResultData"/> instance.</returns>
        public static IEnumerable<SearchResultData> GetSearchCriteriaPathsAndValues(object @object)
        {
            var type = @object.GetType();
            var queue = new Queue<(string ParentPath, PropertyInfo Property, object ContainingObject, Type ContainingObjecType)>();
            queue.Enqueue((string.Empty, null, @object, type));
            while (queue.Any())
            {
                var (parentPath, property, containingObject, containingObjectType) = queue.Dequeue();
                if (containingObjectType.IsClass)
                {
                    if (property != null && Attribute.IsDefined(property, typeof(SearchCriterionAttribute), false))
                    {
                        yield return new SearchResultData(parentPath, containingObject);
                    }
                    else
                    {
                        foreach (var prop in containingObjectType.GetProperties())
                        {
                            // Skip properties defined in .NET assemblies. Also skip not initialized properties.
                            if (containingObjectType.Assembly.FullName != type.Assembly.FullName ||
                                containingObject == null)
                            {
                                continue;
                            }

                            // Skip properties without a getter.
                            if (!prop.CanRead)
                            {
                                continue;
                            }

                            queue.Enqueue(($"{(parentPath == string.Empty ? "" : parentPath + ".")}{prop.Name}",
                                prop,
                                prop.GetValue(containingObject),
                                prop.PropertyType));
                        }
                    }
                }
                else if (containingObjectType.IsArray)
                {
                    Type elementType = containingObjectType.GetElementType();
                    foreach (var element in (Array)containingObject)
                    {
                        queue.Enqueue((parentPath, null, element, elementType));
                    }
                }
                else if (containingObjectType.IsGenericType && Nullable.GetUnderlyingType(containingObjectType) == null)
                {
                    if (containingObjectType.GetGenericTypeDefinition() != typeof(IEnumerable<>).GetGenericTypeDefinition())
                    {
                        throw new InvalidOperationException("Only generics typed as IEnumerable<T> are supported.");
                    }
                    // The actual collection referenced by containingObject must implement IList.
                    // Other enumerables like HasSet, SortedSet, Dictionary, etc. are not supported!
                    var length = ((IList)containingObject).Count;
                    for (int i = 0; i < length; i++)
                    {
                        queue.Enqueue((parentPath, null, ((IList)containingObject)[i], containingObjectType.GetGenericArguments()[0]));
                    }
                }
                else if (Attribute.IsDefined(property, typeof(SearchCriterionAttribute), false))
                {
                    // A Struct property marked by SearchCriterionAttribure.
                    yield return new SearchResultData(parentPath, containingObject);
                }
            }
        }
    }
}
