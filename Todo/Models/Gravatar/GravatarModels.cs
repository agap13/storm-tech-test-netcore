namespace Todo.Models.Gravatar
{
    public class Response
    {
        public Entry[] entry { get; set; }
    }

    public class Entry
    {
        public Name name { get; set; }

        public Photo[] photos { get; set; }

        public string displayName { get; set; }
    }

    public class Name
    {
        public string formatted { get; set; }
    }

    public class Photo
    {
        public string value { get; set; }

        public string type { get; set; }
    }

}
