namespace FastestResultProvider
{
    internal class ResultSet<TOut>
    {
        public TOut Result { get; private set; }

        public void SetResult(TOut result)
        {
            Result = result;
        }
    }
}