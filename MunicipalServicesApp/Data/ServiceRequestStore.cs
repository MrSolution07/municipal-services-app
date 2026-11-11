using System;
using System.Collections.Generic;
using MunicipalServicesApp.Models;

namespace MunicipalServicesApp.Data
{
    /// <summary>
    /// Single in-memory catalogue for service requests. The master list and the seed
    /// dependency pairs are the source of truth. Every other structure is rebuilt from them,
    /// except the binary search tree, which is insert-only so its right spine stays intact.
    /// </summary>
    public static class ServiceRequestStore
    {
        private static readonly List<ServiceRequest> Master = new List<ServiceRequest>();
        private static readonly Dictionary<string, ServiceRequest> ById = new Dictionary<string, ServiceRequest>(StringComparer.Ordinal);
        private static readonly List<Dependency> Dependencies = new List<Dependency>();

        private static readonly BinarySearchTree SearchTree = new BinarySearchTree();
        private static AvlTree referenceTree = new AvlTree();
        private static RedBlackTree urgencyTree = new RedBlackTree();
        private static MaxHeap assignHeap = new MaxHeap();
        private static ServiceGraph graph = new ServiceGraph();
        private static CategoryNode categoryRoot = new CategoryNode { Name = "All services" };
        private static string binaryPreorder = string.Empty;
        private static List<RouteEdge> crewRoute = new List<RouteEdge>();
        private static int crewKilometres;

        private static bool loaded;

        public static int OpenCount
        {
            get
            {
                EnsureLoaded();
                int count = 0;
                for (int i = 0; i < Master.Count; i++)
                {
                    if (Master[i].IsOpen)
                    {
                        count++;
                    }
                }

                return count;
            }
        }

        public static int BstHeight
        {
            get
            {
                EnsureLoaded();
                return SearchTree.Height;
            }
        }

        public static int AvlHeight
        {
            get
            {
                EnsureLoaded();
                return referenceTree.Height;
            }
        }

        public static CategoryNode CategoryRoot
        {
            get
            {
                EnsureLoaded();
                return categoryRoot;
            }
        }

        public static string BinaryPreorder
        {
            get
            {
                EnsureLoaded();
                return binaryPreorder;
            }
        }

        public static IList<RouteEdge> CrewRoute
        {
            get
            {
                EnsureLoaded();
                return crewRoute;
            }
        }

        public static int CrewKilometres
        {
            get
            {
                EnsureLoaded();
                return crewKilometres;
            }
        }

        public static void Load()
        {
            if (loaded)
            {
                return;
            }

            Seed();
            for (int i = 0; i < Master.Count; i++)
            {
                SearchTree.Insert(Master[i].Reference);
            }

            Rebuild();
            if (SearchTree.Height != 12)
            {
                throw new InvalidOperationException("The seeded binary search tree should be a spine of height 12.");
            }

            loaded = true;
        }

        public static void AddFromReport(IssueReport report)
        {
            if (report == null)
            {
                throw new ArgumentNullException("report");
            }

            EnsureLoaded();
            if (ById.ContainsKey(report.ReferenceNumber))
            {
                return;
            }

            string department;
            string category;
            MapReportCategory(report.Category, out department, out category);

            ServiceRequest request = new ServiceRequest
            {
                Reference = report.ReferenceNumber,
                Department = department,
                Category = category,
                Location = report.Location,
                Area = MatchArea(report.Location),
                Description = report.Description,
                Status = RequestStatus.Submitted,
                Priority = 3,
                SubmittedAt = report.SubmittedAt,
                Source = RequestSource.Resident
            };

            Master.Add(request);
            ById.Add(request.Reference, request);
            SearchTree.Insert(request.Reference);
            Rebuild();
        }

        /// <summary>The only status writer. Rebuilds every derived structure except the binary search tree.</summary>
        public static void SetStatus(string reference, RequestStatus status)
        {
            EnsureLoaded();
            ServiceRequest request;
            if (!ById.TryGetValue(reference, out request))
            {
                return;
            }

            if (request.Status == status)
            {
                return;
            }

            request.Status = status;
            Rebuild();
        }

        public static bool TryAdvance(string reference)
        {
            EnsureLoaded();
            ServiceRequest request;
            if (!ById.TryGetValue(reference, out request) || request.Status == RequestStatus.Resolved)
            {
                return false;
            }

            SetStatus(reference, Next(request.Status));
            return true;
        }

        /// <summary>Peeks the assign heap, then sets that request to Assigned. Does not dequeue.</summary>
        public static ServiceRequest AssignNext()
        {
            EnsureLoaded();
            ServiceRequest next = assignHeap.Peek();
            if (next == null)
            {
                return null;
            }

            SetStatus(next.Reference, RequestStatus.Assigned);
            return Get(next.Reference);
        }

        public static ServiceRequest PeekUrgent()
        {
            EnsureLoaded();
            return assignHeap.Peek();
        }

        public static ServiceRequest Get(string reference)
        {
            EnsureLoaded();
            ServiceRequest request;
            if (reference != null && ById.TryGetValue(reference, out request))
            {
                return request;
            }

            return null;
        }

        public static ReferenceSearch Find(string reference)
        {
            EnsureLoaded();
            if (string.IsNullOrEmpty(reference))
            {
                return new ReferenceSearch();
            }

            return SearchTree.Search(reference.Trim());
        }

        public static List<ServiceRequest> ByReference()
        {
            EnsureLoaded();
            return Lookup(referenceTree.InOrder());
        }

        public static List<ServiceRequest> ByUrgency()
        {
            EnsureLoaded();
            List<UrgencyKey> keys = urgencyTree.InOrder();
            List<ServiceRequest> requests = new List<ServiceRequest>();
            for (int i = 0; i < keys.Count; i++)
            {
                ServiceRequest request = Get(keys[i].Reference);
                if (request != null)
                {
                    requests.Add(request);
                }
            }

            return requests;
        }

        public static List<RelatedStep> RelatedWork(string reference)
        {
            EnsureLoaded();
            return graph.BreadthFirst(reference, OpenReferences());
        }

        public static List<string> DependencyChain(string reference)
        {
            EnsureLoaded();
            return graph.DependencyChain(reference);
        }

        /// <summary>Figures for the readme. Taken from the catalogue after Load.</summary>
        public static string SeedSummary()
        {
            EnsureLoaded();
            ReferenceSearch search = Find("SR-07");
            ServiceRequest urgent = PeekUrgent();
            List<string> chain = DependencyChain("SR-03");
            List<RelatedStep> related = RelatedWork("SR-07");

            string route = crewRoute.Count + " edges, " + crewKilometres + " km";
            for (int i = 0; i < crewRoute.Count; i++)
            {
                route += Environment.NewLine + crewRoute[i].From + " - " + crewRoute[i].To + " (" + crewRoute[i].Kilometres + " km)";
            }

            string relatedText = related.Count == 0 ? "none" : string.Empty;
            for (int i = 0; i < related.Count; i++)
            {
                if (i > 0)
                {
                    relatedText += ", ";
                }

                relatedText += related[i].Reference + " step " + related[i].Steps;
            }

            string urgency = string.Empty;
            List<ServiceRequest> urgentList = ByUrgency();
            for (int i = 0; i < urgentList.Count; i++)
            {
                if (i > 0)
                {
                    urgency += ", ";
                }

                urgency += urgentList[i].Reference;
            }

            return "Open " + OpenCount + Environment.NewLine +
                   "BST height " + BstHeight + Environment.NewLine +
                   "AVL height " + AvlHeight + Environment.NewLine +
                   "SR-07 path " + string.Join(", ", search.Visited.ToArray()) + Environment.NewLine +
                   "Urgent peek " + (urgent == null ? "none" : urgent.Reference) + Environment.NewLine +
                   "Urgency " + urgency + Environment.NewLine +
                   "Chain " + string.Join(", ", chain.ToArray()) + Environment.NewLine +
                   "Related SR-07 " + relatedText + Environment.NewLine +
                   route + Environment.NewLine +
                   "Preorder " + binaryPreorder;
        }

        private static void EnsureLoaded()
        {
            if (!loaded)
            {
                Load();
            }
        }

        private static void Rebuild()
        {
            categoryRoot = CategoryTreeBuilder.Build(Master);
            binaryPreorder = BinaryEncoding.Preorder(BinaryEncoding.FromCategory(categoryRoot));

            referenceTree = new AvlTree();
            List<ServiceRequest> ordered = new List<ServiceRequest>(Master);
            ordered.Sort(delegate(ServiceRequest left, ServiceRequest right)
            {
                return string.Compare(left.Reference, right.Reference, StringComparison.Ordinal);
            });

            for (int i = 0; i < ordered.Count; i++)
            {
                referenceTree.Insert(ordered[i].Reference);
            }

            urgencyTree = new RedBlackTree();
            assignHeap = new MaxHeap();
            for (int i = 0; i < ordered.Count; i++)
            {
                ServiceRequest request = ordered[i];
                if (request.IsOpen)
                {
                    urgencyTree.Insert(new UrgencyKey
                    {
                        Priority = request.Priority,
                        SubmittedAt = request.SubmittedAt,
                        Reference = request.Reference
                    });
                }

                if (request.IsWaitingToAssign)
                {
                    assignHeap.Insert(request);
                }
            }

            graph = BuildGraph();
            List<string> places = RoutePlaces();
            int kilometres;
            crewRoute = MinimumSpanningTree.Prim(places, out kilometres);
            crewKilometres = kilometres;

            referenceTree.Validate();
            urgencyTree.Validate();
        }

        private static ServiceGraph BuildGraph()
        {
            ServiceGraph built = new ServiceGraph();
            for (int i = 0; i < Dependencies.Count; i++)
            {
                built.AddEdge(Dependencies[i].From, Dependencies[i].To, ServiceEdgeKind.DependsOn);
            }

            for (int i = 0; i < Master.Count; i++)
            {
                for (int j = i + 1; j < Master.Count; j++)
                {
                    if (Master[i].Area == Master[j].Area)
                    {
                        built.AddEdge(Master[i].Reference, Master[j].Reference, ServiceEdgeKind.Related);
                        built.AddEdge(Master[j].Reference, Master[i].Reference, ServiceEdgeKind.Related);
                    }
                }
            }

            return built;
        }

        private static List<string> RoutePlaces()
        {
            List<string> places = new List<string>();
            places.Add(AreaGraph.Depot);
            for (int i = 0; i < Master.Count; i++)
            {
                if (!Master[i].IsOpen)
                {
                    continue;
                }

                if (!places.Contains(Master[i].Area))
                {
                    places.Add(Master[i].Area);
                }
            }

            return places;
        }

        private static HashSet<string> OpenReferences()
        {
            HashSet<string> open = new HashSet<string>(StringComparer.Ordinal);
            for (int i = 0; i < Master.Count; i++)
            {
                if (Master[i].IsOpen)
                {
                    open.Add(Master[i].Reference);
                }
            }

            return open;
        }

        private static List<ServiceRequest> Lookup(List<string> references)
        {
            List<ServiceRequest> requests = new List<ServiceRequest>();
            for (int i = 0; i < references.Count; i++)
            {
                ServiceRequest request = Get(references[i]);
                if (request != null)
                {
                    requests.Add(request);
                }
            }

            return requests;
        }

        private static RequestStatus Next(RequestStatus status)
        {
            switch (status)
            {
                case RequestStatus.Submitted:
                    return RequestStatus.InReview;
                case RequestStatus.InReview:
                    return RequestStatus.Assigned;
                case RequestStatus.Assigned:
                    return RequestStatus.InProgress;
                case RequestStatus.InProgress:
                    return RequestStatus.Resolved;
                default:
                    return RequestStatus.Resolved;
            }
        }

        private static void MapReportCategory(string reportCategory, out string department, out string category)
        {
            switch (reportCategory)
            {
                case "Sanitation and refuse removal":
                    department = "Sanitation";
                    category = "Sanitation";
                    return;
                case "Roads and potholes":
                    department = "Roads";
                    category = "Roads";
                    return;
                case "Water and leaks":
                    department = "Utilities";
                    category = "Water";
                    return;
                case "Electricity and street lights":
                    department = "Utilities";
                    category = "Electricity";
                    return;
                case "Stormwater and drainage":
                    department = "Roads";
                    category = "Stormwater";
                    return;
                case "Parks and public spaces":
                    department = "Community";
                    category = "Parks";
                    return;
                default:
                    department = "Governance";
                    category = "Accounts";
                    return;
            }
        }

        private static string MatchArea(string location)
        {
            string[] areas = { "Durbanville", "Brackenfell", "Bellville", "Parow", "CBD" };
            if (!string.IsNullOrEmpty(location))
            {
                for (int i = 0; i < areas.Length; i++)
                {
                    if (location.IndexOf(areas[i], StringComparison.OrdinalIgnoreCase) >= 0)
                    {
                        return areas[i];
                    }
                }
            }

            return "Bellville";
        }

        private static void Seed()
        {
            DateTime start = new DateTime(2026, 11, 1, 8, 0, 0);
            AddSeed("SR-01", "Utilities", "Electricity", "CBD substation", "CBD", "The CBD substation is off while crews isolate a fault.", RequestStatus.InReview, 5, start);
            AddSeed("SR-02", "Utilities", "Electricity", "CBD main road", "CBD", "Street lights on the main road are out after the outage.", RequestStatus.Submitted, 4, start.AddHours(1));
            AddSeed("SR-03", "Utilities", "Electricity", "Civic hall annex", "CBD", "Follow-up inspection once the lights are restored.", RequestStatus.Submitted, 2, start.AddHours(2));
            AddSeed("SR-04", "Utilities", "Water", "Bellville water works", "Bellville", "The water main is shut so the burst pipe can be repaired.", RequestStatus.InProgress, 5, start.AddHours(3));
            AddSeed("SR-05", "Utilities", "Water", "Voortrekker Road, Bellville", "Bellville", "Pipe repair starts after the main is shut.", RequestStatus.Assigned, 4, start.AddHours(4));
            AddSeed("SR-06", "Roads", "Roads", "Voortrekker Road, Parow", "Parow", "A pothole on Voortrekker Road needs a patch.", RequestStatus.Submitted, 3, start.AddHours(5));
            AddSeed("SR-07", "Sanitation", "Sanitation", "Durbanville collection point", "Durbanville", "Refuse has backed up at the collection point.", RequestStatus.InReview, 3, start.AddHours(6));
            AddSeed("SR-08", "Utilities", "Electricity", "Willowbridge Park, Durbanville", "Durbanville", "Park lights in Durbanville are dark.", RequestStatus.Submitted, 2, start.AddHours(7));
            AddSeed("SR-09", "Roads", "Stormwater", "Brackenfell storm channel", "Brackenfell", "The storm drain in Brackenfell is blocked.", RequestStatus.Submitted, 4, start.AddHours(8));
            AddSeed("SR-10", "Governance", "Accounts", "Parow municipal office", "Parow", "An account query has already been answered.", RequestStatus.Resolved, 1, start.AddHours(9));
            AddSeed("SR-11", "Community", "Buildings", "Bellville Public Library", "Bellville", "A leak in the library roof needs a builder.", RequestStatus.Submitted, 2, start.AddHours(10));
            AddSeed("SR-12", "Sanitation", "Sanitation", "Parow taxi rank", "Parow", "Illegal dumping at the taxi rank is being cleared.", RequestStatus.InProgress, 3, start.AddHours(11));

            Dependencies.Add(new Dependency("SR-02", "SR-01"));
            Dependencies.Add(new Dependency("SR-03", "SR-02"));
            Dependencies.Add(new Dependency("SR-05", "SR-04"));
        }

        private static void AddSeed(string reference, string department, string category, string location, string area, string description, RequestStatus status, int priority, DateTime submittedAt)
        {
            ServiceRequest request = new ServiceRequest
            {
                Reference = reference,
                Department = department,
                Category = category,
                Location = location,
                Area = area,
                Description = description,
                Status = status,
                Priority = priority,
                SubmittedAt = submittedAt,
                Source = RequestSource.Seed
            };

            Master.Add(request);
            ById.Add(reference, request);
        }

        private sealed class Dependency
        {
            public Dependency(string from, string to)
            {
                From = from;
                To = to;
            }

            public string From { get; private set; }

            public string To { get; private set; }
        }
    }
}
