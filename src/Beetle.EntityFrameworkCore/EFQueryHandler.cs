﻿using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
#if NET_STANDARD
using System.Reflection;
#endif

namespace Beetle.EntityFrameworkCore {
    using Server;

    public class EFQueryHandler: QueryableHandler {
        private static readonly MethodInfo _includeMethod;
        private static readonly Lazy<EFQueryHandler> _instance = new Lazy<EFQueryHandler>(() => new EFQueryHandler());

        static EFQueryHandler() {
            var queryExtensionsType = typeof(EntityFrameworkQueryableExtensions);
            _includeMethod = queryExtensionsType.GetMethods()
                .First(m => m.Name == "Include" && m.GetParameters().Last().GetType() == typeof(string));
        }

        public override IQueryable Include(IQueryable query, string expand) {
            if (string.IsNullOrWhiteSpace(expand)) return query;

            var genMethod = _includeMethod.MakeGenericMethod(query.ElementType);
            expand.Split(',').ToList()
                .ForEach(e => { query = (IQueryable)genMethod.Invoke(query, new object[] { e.Trim() }); });
            return query;
        }

        public new static QueryableHandler Instance => _instance.Value;
    }
}
