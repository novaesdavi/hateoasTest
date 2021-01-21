using HateoasLibrary.Repository;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using Microsoft.AspNetCore.Http;

namespace HateoasLibrary
{
    public sealed class PolicyBuilder<TResponse>
        where TResponse : class
    {

        public PolicyBuilder<TResponse> AddExternalUri(string linkKey, string method, Expression<Func<TResponse, object>> expression)
        {
            InMemoryPolicyRepository.InMemoryPolicies.Add(
                new InMemoryPolicyRepository.ExternalPolicy(typeof(TResponse), expression, linkKey)
                {

                    Method = method ?? HttpMethods.Get
                });

            return this;
        }

    }

    public sealed class PolicyBuilder<TResponse, TRequest>
        where TResponse : class 
        where TRequest : class
    {

        public PolicyBuilder<TResponse, TRequest> AddExternalUri(string linkKey, string method, Expression<Func<TResponse, object>> expression)
        {

            InMemoryPolicyRepository.InMemoryPolicies.Add(
                new InMemoryPolicyRepository.ExternalPolicy(typeof(TResponse), typeof(TRequest), expression, linkKey)
                {

                    Method = method ?? HttpMethods.Get
                });

            return this;
        }
    }

    public sealed class ConditionBuilder<TResponse>
    where TResponse : class, new()
    {

        public ConditionBuilder<TResponse> IncludeCondition(Func<ConditionModel<TResponse>, bool> expressionCondition)
        {
            var model = new ConditionModel<TResponse>();

            InMemoryConditionRepository.InMemoryCondition = new InMemoryConditionRepository.Condition(expression => expressionCondition(expression.Cast<TResponse>()));

            return this;
        }
    }
}
