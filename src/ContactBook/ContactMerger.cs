namespace ContactBook;

public class ContactMerger
{
    
    private class DuplicateSets
    {
        private int[] parents;

        public DuplicateSets(int n)
        {
            parents = new int[n];
            for (int i = 0; i < n; i++)
            {
                parents[i] = i; 
            }
        }

        public int FindRoot(int i)
        {
            if (parents[i] != i)
            {
            
                parents[i] = FindRoot(parents[i]);
            }
            return parents[i];
        }

        public void Union(int a, int b)
        {
            int rootA = FindRoot(a);
            int rootB = FindRoot(b);

            if (rootA != rootB)
            {
                parents[rootB] = rootA;
            }
        }
    }

        public static Dictionary<int, List<int>> FindDuplicates(List<Contact> contacts)
    {
        var phoneIndex = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase);
        var emailIndex = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase);
        
        var ds = new DuplicateSets(contacts.Count);

        for (int i = 0; i < contacts.Count; i++)
        {
            Contact c = contacts[i];
            string phone = c.Telefono ?? "";
            string email = c.Email ?? "";

            if (!string.IsNullOrWhiteSpace(phone))
            {
                if (phoneIndex.TryGetValue(phone, out int existingIdx))
                {
                    ds.Union(existingIdx, i);
                }
                else
                {
                    phoneIndex[phone] = i;
                }
            }

            if (!string.IsNullOrWhiteSpace(email))
            {
                if (emailIndex.TryGetValue(email, out int existingIdx))
                {
                    ds.Union(existingIdx, i);
                }
                else
                {
                    emailIndex[email] = i;
                }
            }
        }

    
        var groups = new Dictionary<int, List<int>>();

        for (int i = 0; i < contacts.Count; i++)
        {
            int root = ds.FindRoot(i);

            if (!groups.TryGetValue(root, out var list))
            {
                list = new List<int>();
                groups[root] = list;
            }
            list.Add(i);
        }

        return groups;
    }
}