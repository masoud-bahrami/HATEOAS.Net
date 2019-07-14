/*
  HATEOAS.Net solution contains implementations of 
    Hypermedia as the engine of application state (HATEOAS)
    based on different specifications.

 HATEOAS.Net.HAL is an implementation of HAL's Specification, and it also contains some
 extra features such as Link httpVerb(GET, POST ...) and also action parameters.

 Masoud Bahrami
 http://refactor.ir
 https://twitter.com/masodbahrami
 */

namespace HATEOAS.Net.HAL
{
    public enum QueryParameterType
    {
        String,
        Boolean,
        Number,
        DateTime,
        Char,
        Enum,
        Object,
        Collection
    }

    public class ScalarQueryParameter
    {
        public static ScalarQueryParameter NewString(string title, short position)
        {
            return new ScalarQueryParameter(title, QueryParameterType.String, position);
        }
        public static ScalarQueryParameter NewBoolean(string title, short position)
        {
            return new ScalarQueryParameter(title, QueryParameterType.Boolean, position);
        }
        public static ScalarQueryParameter NewNumber(string title, short position)
        {
            return new ScalarQueryParameter(title, QueryParameterType.Number, position);
        }
        public static ScalarQueryParameter NewDateTime(string title, short position)
        {
            return new ScalarQueryParameter(title, QueryParameterType.DateTime, position);
        }
        public static ScalarQueryParameter NewChar(string title, short position)
        {
            return new ScalarQueryParameter(title, QueryParameterType.Char, position);
        }
        private ScalarQueryParameter(string title, QueryParameterType type, short position)
        {
            Title = title;
            Type = type;
            Position = position;
        }

        public ScalarQueryParameter WithTitle(string title)
        {
            return new ScalarQueryParameter(title, Type, Position);
        }
        public ScalarQueryParameter WithType(QueryParameterType type)
        {
            return new ScalarQueryParameter(Title, type, Position);
        }
        public ScalarQueryParameter WithPosition(short position)
        {
            return new ScalarQueryParameter(Title, Type, position);
        }
        public string Title { get; }
        public QueryParameterType Type { get; }
        public short Position { get; }
    }

    //public class EnumQueryParameter
    //{
    //    private EnumQueryParameter(string title, short position, Dictionary<int, string> enumValues = null)
    //    {
    //        Title = title;
    //        Type = QueryParameterType.Enum;
    //        Position = position;
    //        EnumValues = enumValues?? new Dictionary<int, string>();
    //    }
    //    public EnumQueryParameter WithTitle(string title)
    //    {
    //        return new EnumQueryParameter(title, Position, EnumValues);
    //    }
    //    public EnumQueryParameter WithPosition(short position)
    //    {
    //        return new EnumQueryParameter(Title, position, EnumValues);
    //    }

    //    public string Title { get; }
    //    public QueryParameterType Type { get; }
    //    public short Position { get; }
    //    public Dictionary<int, string> EnumValues { get; }
    //}

    //public class ObjectQueryParameter
    //{
    //    public ObjectQueryParameter(string title, short position)
    //    {
    //        Title = title;
    //        Position = position;
    //        Type = QueryParameterType.Object;
    //    }
    //    public string Title { get; }
    //    public QueryParameterType Type { get; }
    //    public short Position { get; }
    //}
}