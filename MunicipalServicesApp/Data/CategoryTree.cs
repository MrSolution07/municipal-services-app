using System;
using System.Collections.Generic;
using MunicipalServicesApp.Models;

namespace MunicipalServicesApp.Data
{
    /// <summary>
    /// One node in the basic (n-ary) category tree. The root is "All services".
    /// A department node has a department and no category. A category node has both.
    /// </summary>
    public class CategoryNode
    {
        public CategoryNode()
        {
            Children = new List<CategoryNode>();
        }

        public string Name { get; set; }

        public string Department { get; set; }

        public string Category { get; set; }

        public List<CategoryNode> Children { get; private set; }
    }

    /// <summary>
    /// Builds the department-then-category tree from the requests currently stored.
    /// Children are ordered with <see cref="StringComparison.Ordinal"/> so the tree is stable.
    /// </summary>
    public static class CategoryTreeBuilder
    {
        public static CategoryNode Build(IEnumerable<ServiceRequest> requests)
        {
            SortedDictionary<string, SortedSet<string>> grouped = new SortedDictionary<string, SortedSet<string>>(StringComparer.Ordinal);

            foreach (ServiceRequest request in requests)
            {
                if (string.IsNullOrEmpty(request.Department) || string.IsNullOrEmpty(request.Category))
                {
                    continue;
                }

                SortedSet<string> categories;
                if (!grouped.TryGetValue(request.Department, out categories))
                {
                    categories = new SortedSet<string>(StringComparer.Ordinal);
                    grouped.Add(request.Department, categories);
                }

                categories.Add(request.Category);
            }

            CategoryNode root = new CategoryNode { Name = "All services" };
            foreach (KeyValuePair<string, SortedSet<string>> pair in grouped)
            {
                CategoryNode department = new CategoryNode
                {
                    Name = pair.Key,
                    Department = pair.Key
                };

                foreach (string category in pair.Value)
                {
                    department.Children.Add(new CategoryNode
                    {
                        Name = category,
                        Department = pair.Key,
                        Category = category
                    });
                }

                root.Children.Add(department);
            }

            return root;
        }
    }
}
