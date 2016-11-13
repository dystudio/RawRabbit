﻿using System;
using System.Threading.Tasks;
using RawRabbit.Context;
using RawRabbit.Pipe;
using RawRabbit.Pipe.Middleware;

namespace RawRabbit.Enrichers.MessageContext.Subscribe.Middleware
{
	public class AutoAckMessageHandlerMiddleware : AutoAckMessageHandlerMiddlewareBase
	{
		public AutoAckMessageHandlerMiddleware(AutoAckHandlerOptions options = null) : base(options)
		{ }

		protected override Task InvokeHandlerAsync(IPipeContext context)
		{
			var message = context.GetMessage();
			var messageContext = context.GetMessageContext();
			var handler = context.Get<Func<object, IMessageContext, Task>>(PipeKey.MessageHandler);

			return handler.Invoke(message, messageContext);
		}
	}
}