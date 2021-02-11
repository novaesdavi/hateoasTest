using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace HateoasLibrary
{
    /// <summary>
    /// Allows link policy configuration.
    /// </summary>
    public sealed class LinkBuilder
    {
        /// <inheritdoc/>
        public LinkBuilder AddPolicy<TResponse>(Action<PolicyBuilder<TResponse>> modelSetup, Action<ConditionBuilder<TResponse>> conditionSetup)
            where TResponse : class, new()
        {
            var policy = new PolicyBuilder<TResponse>();
            var condition = new ConditionBuilder<TResponse>();

            modelSetup?.Invoke(policy);
            conditionSetup?.Invoke(condition);

            return this;
        }

        public LinkBuilder AddPolicy<TResponse>(Action<PolicyBuilder<TResponse>> modelSetup)
    where TResponse : class, new()
        {
            var policy = new PolicyBuilder<TResponse>();

            modelSetup?.Invoke(policy);

            return this;
        }

        public LinkBuilder AddPolicy<TResponse, TRequest>(Action<PolicyBuilder<TResponse, TRequest>> modelSetup)
            where TResponse : class, new()
            where TRequest : class
        {
            var policy = new PolicyBuilder<TResponse, TRequest>();

            modelSetup?.Invoke(policy);

            return this;
        }
    }
}
