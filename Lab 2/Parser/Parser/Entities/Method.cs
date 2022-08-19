namespace Parser.Entities;

public class Method
{
    public List<Argument> Arguments = new List<Argument>();
    public string Name;
    public string Path;
    public string ReturnType;
    public string QueryType;
}