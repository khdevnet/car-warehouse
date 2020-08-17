﻿using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CW.Core
{
    internal class CancelationTokenSourceProvider
    {
        private readonly ConcurrentDictionary<Guid, CancellationTokenSource> tokens = new ConcurrentDictionary<Guid, CancellationTokenSource>();

        public CancellationTokenSourceScope CreateScope(Guid id)
        {
            return new CancellationTokenSourceScope(id, this);
        }

        public CancellationTokenSource GetOrAdd(Guid id)
        {
            return tokens.GetOrAdd(id, new CancellationTokenSource());
        }

        public void Cancel(Guid id)
        {
            if (tokens.TryRemove(id, out var tokenSource))
            {
                tokenSource.Cancel();
                tokenSource.Dispose();
            }
        }

        public void Remove(Guid id)
        {
            if (tokens.TryRemove(id, out var tokenSource))
            {
                tokenSource.Dispose();
            }
        }

        public class CancellationTokenSourceScope : IDisposable
        {
            private readonly Guid id;
            private readonly CancelationTokenSourceProvider store;
            public CancellationToken Token { get; }

            public CancellationTokenSourceScope(Guid id, CancelationTokenSourceProvider store)
            {
                this.id = id;
                this.store = store;
                Token = store.GetOrAdd(id).Token;
            }

            public void Dispose()
            {
                store.Remove(id);
            }
        }
    }


}
