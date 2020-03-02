using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace FastestResultProvider
{
    public class FastestResultProvider<TInput, TResult>
    {
        private readonly AutoResetEvent _autoResetEvent = new AutoResetEvent(false);
        private readonly IEnumerable<Func<TInput, TResult>> _functions;

        public FastestResultProvider(IEnumerable<Func<TInput, TResult>> functions)
        {
            _functions = functions;
        }

        public TResult Provide(TInput inputParam)
        {
            var tokenSource = new CancellationTokenSource();
            var token = tokenSource.Token;
            var eventSetter = new AutoResetEventSetter(_autoResetEvent);
            var resultSet = new ResultSet<TResult>();
            foreach (var function in _functions)
                Task.Factory.StartNew(DoJobAction,
                    new DoingJobParam<TInput, TResult>(function, inputParam, resultSet, eventSetter, token), token,
                    TaskCreationOptions.LongRunning, TaskScheduler.Default);

            _autoResetEvent.WaitOne();
            tokenSource.Cancel(false);
            return resultSet.Result;
        }

        private void DoJobAction(object doParamObj)
        {
            var doParam = (DoingJobParam<TInput, TResult>) doParamObj;

            doParam.ResultSet.SetResult(doParam.Func.Invoke(doParam.Input));
            if (!doParam.Token.IsCancellationRequested)
                doParam.EventSetter.Set();
        }
    }
}