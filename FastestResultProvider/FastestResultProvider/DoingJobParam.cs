using System;
using System.Threading;

namespace FastestResultProvider
{
    internal class DoingJobParam<TIn, TOut>
    {
        public readonly AutoResetEventSetter EventSetter;
        public readonly CancellationToken Token;

        public DoingJobParam(Func<TIn, TOut> func, TIn input, ResultSet<TOut> resultSet,
            AutoResetEventSetter eventSetter, CancellationToken token)
        {
            EventSetter = eventSetter;
            Token = token;
            Func = func;
            Input = input;
            ResultSet = resultSet;
        }

        public Func<TIn, TOut> Func { get; }
        public TIn Input { get; }
        public ResultSet<TOut> ResultSet { get; }
    }
}