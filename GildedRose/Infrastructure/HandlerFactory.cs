using System;
using System.Collections.Generic;
using System.Text;
using GildedRose.Api;
using GildedRose.Api.Data;

namespace GildedRose.Infrastructure
{
	/// <summary>
	/// Represents a producer of item quality handlers.
	/// </summary>
	public class HandlerFactory : IHandlerFactory
	{
		#region Private Members
		private Dictionary<ItemType, IItemHandler> handlerMap;
		#endregion

		#region Constructor
		/// <summary>
		/// Creates an instance of a producer of item quality handlers with the provided handlers.
		/// </summary>
		/// <param name="handlers">The handlers to be used.</param>
		public HandlerFactory(Dictionary<ItemType, IItemHandler> handlerMap)
		{
			if (handlerMap == null)
				throw new ArgumentNullException(nameof(handlerMap));

			this.handlerMap = handlerMap;
		}
		#endregion

		#region Public Methods
		/// <inheritdoc/>
		public IItemHandler Get(IItem item)
		{
			if (!this.handlerMap.TryGetValue(item.Type, out IItemHandler handler))
				throw new ArgumentException("No handler found for item type.", nameof(item));

			return handler;
		} 
		#endregion
	}
}
