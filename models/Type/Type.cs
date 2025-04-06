abstract class Type<t>
{
    public string Name { get; set; }
    public t Value { get; private set; }

    public Type(string name, t Value)
    {
        Validate(name);
        Name = name;
        Value = Value;
    }

    protected virtual void Validate(string name)
    {
        if (string.IsNullOrEmpty(name))
        {
            throw new ArgumentException("Name can't be null.");
        }
    }


}