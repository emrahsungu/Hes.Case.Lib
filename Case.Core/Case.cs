namespace Case.Core
{
    public class Case
    {
        public string Id { get; }
        public string Title { get; }

        public Case(string id, string title)
        {
            Id = id;
            Title = title;
        }
    }
}