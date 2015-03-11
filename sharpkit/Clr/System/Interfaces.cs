namespace SharpKit.JavaScript.Private
{
    [JsType(Name = "System.IComparable$1", Filename = "~/Internal/Core.js")]
    public interface JsImplIComparable<T>
    {
        int CompareTo(T other);
    }

    [JsType(Name = "System.IEquatable$1", Filename = "~/Internal/Core.js")]
    public interface JsImplIEquatable<T>
    {
        bool Equals(T other);
    }

    [JsType(Name = "System.IComparar$1", Filename = "~/res/System.js")]
    public interface JsImplIComparar<T>
    {
        int Compare(T x, T y);
    }
}
