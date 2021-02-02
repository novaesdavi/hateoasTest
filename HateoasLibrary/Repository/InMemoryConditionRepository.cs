using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Text;

namespace HateoasLibrary.Repository
{
    public sealed class InMemoryConditionRepository
    {
        public static ConcurrentBag<Condition> InMemoryCondition { get; } = new ConcurrentBag<Condition>();
        public class Condition
        {


            public Condition(Func<ConditionModelResponse, bool> expression, string name = null, [CallerMemberName] string memberName = null)
            {

                this.Name = name;
                Expression = expression;
            }

            public Func<ConditionModelResponse, bool> Expression { get; }

            public string Name { get; }
        }

    }

    public class ConditionModelResponse
    {
        public object Response { get; set; }

    }

    public class ConditionModel<TResponse> : ConditionModelResponse
        where TResponse : class, new()
    {
        public new TResponse Response { get; set; }
        public ConditionModel()
        {
            base.Response = this.Response;

        }
    }

    public static class ConditionModelExtension
    {
        public static ConditionModel<TResponse> Cast<TResponse>(this ConditionModelResponse model)
                where TResponse : class, new()
        {

            var response = (TResponse)model.Response;

            return new ConditionModel<TResponse>() { Response = response };
        }
    }


}
