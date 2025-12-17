namespace Agency.API.Dtos
{
    public class EnumerableResult<T>
    {
        public IEnumerable<T> Result { get; set; } = new List<T>();
        public int Length
        {
            get
            {
                return Result.Count();
            }
        }
    }
}
